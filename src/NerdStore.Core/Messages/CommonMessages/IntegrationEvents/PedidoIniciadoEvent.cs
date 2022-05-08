using NerdStore.Core.DomainObjects.DTO;
using System;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoIniciadoEvent : IntegrationEvent
    {
        public PedidoIniciadoEvent(
            Guid pedidoId,
            Guid clienteId,
            decimal valorTotal,
            ListaProdutosPedido produtosPedido,
            string nomeCartao,
            string numerocartao,
            string expiracaoCartao,
            string cvvCartao)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            ValorTotal = valorTotal;
            ProdutosPedido = produtosPedido;
            NomeCartao = nomeCartao;
            NumeroCartao = numerocartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }

        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public ListaProdutosPedido ProdutosPedido { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }
    }
}
