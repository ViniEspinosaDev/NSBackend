using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Pagamentos.Business.Models;
using System.Threading.Tasks;

namespace NerdStore.Pagamentos.Business.Interface
{
    public interface IPagamentoService
    {
        Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
    }
}
