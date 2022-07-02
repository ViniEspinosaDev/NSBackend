using NerdStore.Pagamentos.Business.Models;

namespace NerdStore.Pagamentos.Business.Interface
{
    public interface IPagamentoCartaoCreditoFacade
    {
        Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento);
    }
}
