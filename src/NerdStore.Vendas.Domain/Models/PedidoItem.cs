using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Vendas.Domain.Models
{
    public class PedidoItem : Entity
    {
        protected PedidoItem() { }

        public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
        {
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public Pedido Pedido { get; private set; }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }

        internal void AssociarPedido(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }

        public override bool Valido()
        {
            return true;
        }
    }
}
