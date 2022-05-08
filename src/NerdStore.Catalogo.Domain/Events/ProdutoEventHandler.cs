using MediatR;
using NerdStore.Catalogo.Domain.Interface;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Events
{
    public class ProdutoEventHandler :
        INotificationHandler<ProdutoAbaixoEstoqueEvent>,
        INotificationHandler<PedidoIniciadoEvent>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMediatorHandler _mediatorHandler;

        public ProdutoEventHandler(
            IProdutoRepository produtoRepository,
            IEstoqueService estoqueServices,
            IMediatorHandler mediatorHandler)
        {
            _produtoRepository = produtoRepository;
            _estoqueService = estoqueServices;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent notification, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorId(notification.AggregateId);

            // Enviar um e-mal para aquisição de mais produtos
            // Enviar notificação para administrador
        }

        public async Task Handle(PedidoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            var debitou = await _estoqueService.DebitarListaProdutosPedido(notification.ProdutosPedido);

            if (debitou)
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueConfirmadoEvent(
                    notification.PedidoId, 
                    notification.ClienteId,
                    notification.ValorTotal,
                    notification.ProdutosPedido,
                    notification.NomeCartao,
                    notification.NumeroCartao,
                    notification.ExpiracaoCartao,
                    notification.CvvCartao));
            }
            else
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueRejeitadoEvent(notification.PedidoId, notification.ClienteId));
            }
        }
    }
}
