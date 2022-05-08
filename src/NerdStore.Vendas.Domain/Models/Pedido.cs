using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using NerdStore.Vendas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Domain.Models
{
    public class Pedido : Entity, IAggregateRoot
    {
        private readonly List<PedidoItem> _itens;

        protected Pedido()
        {
            _itens = new List<PedidoItem>();
        }

        public Pedido(Guid clienteId, bool voucherUtilizado, decimal desconto, decimal valorTotal) : this()
        {
            ClienteId = clienteId;
            VoucherUtilizado = voucherUtilizado;
            Desconto = desconto;
            ValorTotal = valorTotal;
        }

        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public EPedidoStatus Status { get; private set; }
        public IReadOnlyCollection<PedidoItem> Itens => _itens;

        public Voucher Voucher { get; private set; }

        public void AdicionarItem(PedidoItem item)
        {
            if (!item.Valido()) return;

            item.AssociarPedido(Id);

            if (PedidoItemExistente(item))
            {
                var itemExistente = _itens.FirstOrDefault(i => i.ProdutoId == item.ProdutoId);
                itemExistente.AdicionarUnidades(item.Quantidade);
                item = itemExistente;

                _itens.Remove(itemExistente);
            }

            item.CalcularValor();
            _itens.Add(item);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem item)
        {
            if (!item.Valido()) return;

            var itemExistente = Itens.FirstOrDefault(i => i.ProdutoId == item.ProdutoId);

            if (itemExistente == null) throw new DomainException("O item não pertence ao pedido");

            _itens.Remove(itemExistente);

            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem item)
        {
            if (!item.Valido()) return;

            item.AssociarPedido(Id);

            var itemExistente = Itens.FirstOrDefault(i => i.ProdutoId == item.ProdutoId);

            if (itemExistente == null) throw new DomainException("O item não pertence ao pedido");

            _itens.Remove(itemExistente);
            _itens.Add(item);

            CalcularValorPedido();
        }

        public void AtualizarUnidades(PedidoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }

        public ValidationResult AplicarVoucher(Voucher voucher)
        {
            var validationResult = voucher.ValidarSeAplicavel();

            if (!validationResult.IsValid) return validationResult;

            Voucher = voucher;
            VoucherUtilizado = true;
            CalcularValorPedido();

            return validationResult;
        }

        public void CalcularValorPedido()
        {
            ValorTotal = Itens.Sum(i => i.CalcularValor());
            CalcularValorTotalDesconto();
        }

        public bool PedidoItemExistente(PedidoItem item)
        {
            return _itens.Any(i => i.ProdutoId == item.ProdutoId);
        }

        public void TornarRascunho()
        {
            Status = EPedidoStatus.Rascunho;
        }

        public void IniciarPedido()
        {
            Status = EPedidoStatus.Iniciado;
        }

        public void FinalizarPedido()
        {
            Status = EPedidoStatus.Pago;
        }

        public void CancelarPedido()
        {
            Status = EPedidoStatus.Cancelado;
        }

        private void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            decimal desconto = 0;
            var valor = ValorTotal;

            if (Voucher.TipoDescontoVoucher == ETipoDescontoVoucher.Porcentagem)
            {
                if (Voucher.Percentual.HasValue)
                {
                    desconto = (valor * Voucher.Percentual.Value) / 100;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
            }

            ValorTotal = valor < 0 ? 0 : valor;
            Desconto = desconto;
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId
                };

                pedido.TornarRascunho();

                return pedido;
            }
        }
    }
}