using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Pagamentos.Business.Enums;
using NerdStore.Pagamentos.Business.Interface;
using NerdStore.Pagamentos.Business.Models;
using System.Threading.Tasks;

namespace NerdStore.Pagamentos.Business.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PagamentoService(
            IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
            IPagamentoRepository pagamentoRepository,
            IMediatorHandler mediatorHandler)
        {
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            _pagamentoRepository = pagamentoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
        {
            var pedido = new Pedido(pagamentoPedido.PedidoId, pagamentoPedido.ValorTotal);

            var pagamento = new Pagamento(
                pagamentoPedido.PedidoId,
                pagamentoPedido.ValorTotal,
                pagamentoPedido.NomeCartao,
                pagamentoPedido.NumeroCartao,
                pagamentoPedido.ExpiracaoCartao,
                pagamentoPedido.CvvCartao);

            var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

            if (transacao.Status == EStatusTransacao.Pago)
            {
                //pagamento.AdicionarEvento(new PagamentoRealizadoEvent(pedido.Id, pagamentoPedido.ClienteId, transacao.PagamentoId));

                //_pagamentoRepository.Adicionar(pagamento);
                //_pagamentoRepository.AdicionarTransacao(transacao);

                //await _pagamentoRepository.UnitOfWork.Commit();
                //return transacao;
            }

            //await _mediatorHandler.PublicarNotificacao(new DomainNotification("Pagamento", "A"));
            //await _mediatorHandler.PublicarEvento(new PagamentoRecusadoEvent(pedido.Id, pagamentoPedido.ClienteId, transacao.PagamentoId));

            return transacao;
        }
    }
}
