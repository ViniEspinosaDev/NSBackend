using NerdStore.Core.DomainObjects;
using NerdStore.Pagamentos.Business.Enums;
using System;

namespace NerdStore.Pagamentos.Business.Models
{
    public class Transacao : Entity
    {
        public Guid PedidoId { get; private set; }
        public Guid PagamentoId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public EStatusTransacao Status { get; private set; }

        public Pagamento Pagamento { get; set; }
    }
}
