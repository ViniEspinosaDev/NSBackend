using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Extensions;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Domain.Interface;
using NerdStore.Vendas.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Commands
{
    public class PedidoCommandHandler :
        IRequestHandler<AdicionarItemPedidoCommand, bool>,
        IRequestHandler<AtualizarItemPedidoCommand, bool>,
        IRequestHandler<RemoverItemPedidoCommand, bool>,
        IRequestHandler<AplicarVoucherPedidoCommand, bool>,
        IRequestHandler<IniciarPedidoCommand, bool>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository, IMediatorHandler mediatorHandler)
        {
            _pedidoRepository = pedidoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            var pedidoItem = new PedidoItem(message.ProdutoId, message.Nome, message.Quantidade, message.ValorUnitario);

            if (pedido == null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.ClienteId);
                pedido.AdicionarItem(pedidoItem);

                _pedidoRepository.Adicionar(pedido);

                pedido.AdicionarEvento(new PedidoRascunhoIniciadoEvent(message.ClienteId, pedido.Id));
            }
            else
            {
                var pedidoItemExistente = pedido.PedidoItemExistente(pedidoItem);
                pedido.AdicionarItem(pedidoItem);

                if (pedidoItemExistente)
                    _pedidoRepository.AtualizarItem(pedido.Itens.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
                else
                    _pedidoRepository.AdicionarItem(pedidoItem);

                pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            }

            pedido.AdicionarEvento(
                new PedidoItemAdicionadoEvent(
                    pedido.ClienteId,
                    pedido.Id,
                    message.ProdutoId,
                    message.ValorUnitario,
                    message.Quantidade,
                    pedidoItem.ProdutoNome));

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AtualizarItemPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);

            if (pedido == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Pedido não encontrado"));
                return false;
            }

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, request.ProdutoId);

            if (!pedido.PedidoItemExistente(pedidoItem))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Item do pedido não encontrado"));
                return false;
            }

            pedido.AtualizarUnidades(pedidoItem, request.Quantidade);

            _pedidoRepository.AtualizarItem(pedidoItem);
            _pedidoRepository.Atualizar(pedido);

            pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            pedido.AdicionarEvento(new PedidoItemAtualizadoEvent(pedido.ClienteId, pedido.Id, request.ProdutoId, request.Quantidade));

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoverItemPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);

            if (pedido == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Pedido não encontrado"));
                return false;
            }

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, request.ProdutoId);

            if (pedidoItem != null && !pedido.PedidoItemExistente(pedidoItem))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Item do pedido não encontrado"));
                return false;
            }

            pedido.RemoverItem(pedidoItem);

            _pedidoRepository.Atualizar(pedido);

            pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            pedido.AdicionarEvento(new PedidoItemRemovidoEvent(request.ClienteId, pedido.Id, request.ProdutoId));

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AplicarVoucherPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);

            if (pedido == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Pedido não encontrado"));
                return false;
            }

            var voucher = await _pedidoRepository.ObterVoucherPorCodigo(request.CodigoVoucher);

            if (voucher == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Voucher não encontrado"));
                return false;
            }

            var voucherAplicacaoValidation = pedido.AplicarVoucher(voucher);

            if (!voucherAplicacaoValidation.IsValid)
            {
                foreach (var erro in voucherAplicacaoValidation.Errors)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(erro.ErrorCode, erro.ErrorMessage));
                }

                return false;
            }

            _pedidoRepository.Atualizar(pedido);

            pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            pedido.AdicionarEvento(new VoucherAplicadoPedidoEvent(request.ClienteId, pedido.Id, voucher.Id));

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(IniciarPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);

            pedido.IniciarPedido();

            var listaItens = new List<Item>();

            pedido.Itens.ForEach(i => listaItens.Add(new Item { Id = i.ProdutoId, Quantidade = i.Quantidade }));

            var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = listaItens };

            pedido.AdicionarEvento(new PedidoIniciadoEvent(
                pedido.Id,
                pedido.ClienteId,
                pedido.ValorTotal,
                listaProdutosPedido,
                request.NomeCartao,
                request.NumeroCartao,
                request.ExpiracaoCartao,
                request.CvvCartao));

            _pedidoRepository.Atualizar(pedido);

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command message)
        {
            if (message.Valido()) return true;

            foreach (var erro in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, erro.ErrorMessage));
            }

            return false;
        }
    }
}
