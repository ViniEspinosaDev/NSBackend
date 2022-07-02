using System;
using System.Collections.Generic;

namespace NerdStore.Pagamentos.Business.Models
{
    public class Pedido
    {
        public Pedido(Guid id, decimal valorTotal, List<Produto> produtos = null)
        {
            Id = id;
            ValorTotal = valorTotal;
            Produtos = produtos ?? new List<Produto>();
        }

        public Guid Id { get; private set; }
        public decimal ValorTotal { get; private set; }
        public List<Produto> Produtos { get; private set; }
    }
}
