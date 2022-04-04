using NerdStore.Core.DomainObjects;
using Xunit;

namespace NerdStore.Tests.Core
{
    public class ValidationsTest
    {
        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Igual()
        {
            string mensagem = "Dados devem ser iguais";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeIgual(1, 2, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Igual_Corretamente()
        {
            string mensagem = "Dados devem ser iguais";

            Validations.ValidarSeIgual(2, 2, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Diferente()
        {
            string mensagem = "Dados devem ser diferentes";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeDiferente(2, 2, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Diferente_Corretamente()
        {
            string mensagem = "Dados devem ser iguais";

            Validations.ValidarSeDiferente(1, 2, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Caracteres()
        {
            string valor = "1234567890";
            string mensagem = "Palavra deve ter no máximo 9 caracteres";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarCaracteres(valor, 9, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Caracteres_Corretamente()
        {
            string valor = "1234567890";
            string mensagem = "Palavra deve ter no máximo 9 caracteres";

            Validations.ValidarCaracteres(valor, 10, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Caracteres_Max()
        {
            string valor = "1234567890";
            string mensagem = "Palavra deve ter no mínimo 7 e no máximo 9 caracteres";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarCaracteres(valor, 7, 9, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Caracteres_Min()
        {
            string valor = "123456";
            string mensagem = "Palavra deve ter no mínimo 7 e no máximo 9 caracteres";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarCaracteres(valor, 7, 9, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Caracteres_Corretamente_Min_Max()
        {
            string valor = "123456789";
            string mensagem = "Palavra deve ter no mínimo 7 e no máximo 9 caracteres";

            Validations.ValidarCaracteres(valor, 7, 9, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Expressao()
        {
            // TODO: Fazer teste de expressão
        }

        [Fact]
        public void Deve_Validar_Expressao_Corretamente()
        {
            // TODO: Fazer teste de expressão
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Vazio()
        {
            string valor = "1234567890";
            string mensagem = "Valor deve ser vazio";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeVazio(valor, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Vazio_Corretamente()
        {
            string valor = "";
            string mensagem = "Valor deve ser vazio";

            Validations.ValidarSeVazio(valor, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Nulo()
        {
            string valor = "1234567890";
            string mensagem = "Valor deve ser nulo";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeNulo(valor, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Nulo_Corretamente()
        {
            string valor = null;
            string mensagem = "Valor deve ser vazio";

            Validations.ValidarSeNulo(valor, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Nao_Vazio()
        {
            string valor = "";
            string mensagem = "Valor não deve ser vazio";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeNaoVazio(valor, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Nao_Vazio_Corretamente()
        {
            string valor = "1234567890";
            string mensagem = "Valor não deve ser vazio";

            Validations.ValidarSeNaoVazio(valor, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Nao_Nulo()
        {
            string valor = null;
            string mensagem = "Valor não deve ser nulo";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeNaoNulo(valor, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Nao_Nulo_Corretamente()
        {
            string valor = "1234567890";
            string mensagem = "Valor não deve ser nulo";

            Validations.ValidarSeNaoNulo(valor, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Minimo_Maximo_Double_Min()
        {
            double valor = 2;
            double minimo = 3;
            double maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Minimo_Maximo_Double_Max()
        {
            double valor = 6;
            double minimo = 3;
            double maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Minimo_Maximo_Double_Corretamente()
        {
            double valor = 4;
            double minimo = 3;
            double maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Minimo_Maximo_Float_Min()
        {
            float valor = 2;
            float minimo = 3;
            float maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Minimo_Maximo_Float_Max()
        {
            float valor = 6;
            float minimo = 3;
            float maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Minimo_Maximo_Float_Corretamente()
        {
            float valor = 4;
            float minimo = 3;
            float maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Minimo_Maximo_Int_Min()
        {
            int valor = 2;
            int minimo = 3;
            int maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Minimo_Maximo_Int_Max()
        {
            int valor = 6;
            int minimo = 3;
            int maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Minimo_Maximo_Int_Corretamente()
        {
            int valor = 4;
            int minimo = 3;
            int maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Minimo_Maximo_Long_Min()
        {
            long valor = 2;
            long minimo = 3;
            long maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Minimo_Maximo_Long_Max()
        {
            long valor = 6;
            long minimo = 3;
            long maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Minimo_Maximo_Long_Corretamente()
        {
            long valor = 4;
            long minimo = 3;
            long maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Minimo_Maximo_Decimal_Min()
        {
            decimal valor = 2;
            decimal minimo = 3;
            decimal maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Minimo_Maximo_Decimal_Max()
        {
            decimal valor = 6;
            decimal minimo = 3;
            decimal maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Minimo_Maximo_Decimal_Corretamente()
        {
            decimal valor = 4;
            decimal minimo = 3;
            decimal maximo = 5;

            string mensagem = $"Valor deve ser maior que {minimo} e menor que {maximo}";

            Validations.ValidarMinimoMaximo(valor, minimo, maximo, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Nao_Menor_Igual_Minimo_Double()
        {
            double valor = 2;
            double minimo = 3;

            string mensagem = $"Valor não deve ser menor ou igual {minimo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeNaoMenorIgualMinimo(valor, minimo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Nao_Menor_Igual_Minimo_Double_Corretamente()
        {
            double valor = 4;
            double minimo = 3;

            string mensagem = $"Valor não deve ser menor ou igual {minimo}";

            Validations.ValidarSeNaoMenorIgualMinimo(valor, minimo, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Nao_Menor_Igual_Minimo_Float()
        {
            float valor = 2;
            float minimo = 3;

            string mensagem = $"Valor não deve ser menor ou igual {minimo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeNaoMenorIgualMinimo(valor, minimo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Nao_Menor_Igual_Minimo_Float_Corretamente()
        {
            float valor = 4;
            float minimo = 3;

            string mensagem = $"Valor não deve ser menor ou igual {minimo}";

            Validations.ValidarSeNaoMenorIgualMinimo(valor, minimo, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Nao_Menor_Igual_Minimo_Int()
        {
            int valor = 2;
            int minimo = 3;

            string mensagem = $"Valor não deve ser menor ou igual {minimo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeNaoMenorIgualMinimo(valor, minimo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Nao_Menor_Igual_Minimo_Int_Corretamente()
        {
            int valor = 4;
            int minimo = 3;

            string mensagem = $"Valor não deve ser menor ou igual {minimo}";

            Validations.ValidarSeNaoMenorIgualMinimo(valor, minimo, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Nao_Menor_Igual_Minimo_Long()
        {
            long valor = 2;
            long minimo = 3;

            string mensagem = $"Valor não deve ser menor ou igual {minimo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeNaoMenorIgualMinimo(valor, minimo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Nao_Menor_Igual_Minimo_Long_Corretamente()
        {
            long valor = 4;
            long minimo = 3;

            string mensagem = $"Valor não deve ser menor ou igual {minimo}";

            Validations.ValidarSeNaoMenorIgualMinimo(valor, minimo, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Nao_Menor_Igual_Minimo_Decimal()
        {
            decimal valor = 2;
            decimal minimo = 3;

            string mensagem = $"Valor não deve ser menor ou igual {minimo}";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeNaoMenorIgualMinimo(valor, minimo, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Nao_Menor_Igual_Minimo_Decimal_Corretamente()
        {
            decimal valor = 4;
            decimal minimo = 3;

            string mensagem = $"Valor não deve ser menor ou igual {minimo}";

            Validations.ValidarSeNaoMenorIgualMinimo(valor, minimo, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Falso()
        {
            string mensagem = "O valor deve ser falso";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeFalso(true, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Falso_Corretamente()
        {
            string mensagem = "O valor deve ser falso";

            Validations.ValidarSeFalso(false, mensagem);

            Assert.True(true);
        }

        [Fact]
        public void Deve_Dar_Erro_Ao_Validar_Se_Verdadeiro()
        {
            string mensagem = "O valor deve ser verdadeiro";

            var excecao = Assert.Throws<DomainException>(() => Validations.ValidarSeVerdadeiro(false, mensagem));

            Assert.Contains(mensagem, excecao.Message);
        }

        [Fact]
        public void Deve_Validar_Se_Verdadeiro_Corretamente()
        {
            string mensagem = "O valor deve ser verdadeiro";

            Validations.ValidarSeVerdadeiro(true, mensagem);

            Assert.True(true);
        }
    }
}
