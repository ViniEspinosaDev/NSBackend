using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    public class Categoria : Entity
    {
        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;

            Validar();
        }

        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        public override string ToString()
        {
            return $"{Nome} - {Codigo}";
        }

        public void Validar()
        {
            Validations.ValidarSeVazio(Nome, "O campo Nome da categoria não pode estar vazio");
            Validations.ValidarSeIgual(Codigo, 0, "O campo Codigo da categoria não pode ser zero");
        }
    }
}
