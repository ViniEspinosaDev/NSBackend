using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Pagamentos.Business.Models
{
    public class Pagamento : Entity, IAggregateRoot
    {
        public Pagamento(Guid pedidoId, decimal valorTotal, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
        {
            PedidoId = pedidoId;
            ValorTotal = valorTotal;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }

        public Guid PedidoId { get; private set; }
        public string Status { get; private set; }
        public decimal ValorTotal { get; private set; }

        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }

        public Transacao Transacao { get; set; }
    }
}
