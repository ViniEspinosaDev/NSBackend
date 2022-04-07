using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain.Models
{
    public class Dimensoes
    {
        // TODO: Criar teste de unidade para essa classe
        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Validations.ValidarSeNaoMenorIgualMinimo(altura, 1, "O campo Altura não pode ser menor ou igual a 1cm");
            Validations.ValidarSeNaoMenorIgualMinimo(largura, 1, "O campo Largura não pode ser menor ou igual a 1cm");
            Validations.ValidarSeNaoMenorIgualMinimo(profundidade, 1, "O campo Profundidade não pode ser menor ou igual a 1cm");

            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }

        public decimal Altura { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Profundidade { get; private set; }

        public override string ToString()
        {
            return $"L x A x P: {Largura} x {Altura} x {Profundidade}";
        }
    }
}
