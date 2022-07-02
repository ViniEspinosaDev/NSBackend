namespace NerdStore.Pagamentos.Business.Models
{
    public class Produto
    {
        public Produto(int quantidade, decimal valorUnitario)
        {
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario{ get; private set; }
    }
}
