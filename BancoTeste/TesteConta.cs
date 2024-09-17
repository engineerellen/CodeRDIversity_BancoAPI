using Domain;

namespace BancoTeste
{
    public class TesteConta
    {
        Conta objConta;
        public TesteConta()
        {
            objConta = new Conta();
        }

        [Fact]
        public void Sacar_TestSuccess()
        {
            objConta.ValorConta = 100000;
            var valorContaInicial = objConta.ValorConta;

            var valorAserRetirado = 1000;
            objConta.Sacar(valorAserRetirado);

            Assert.True(valorContaInicial > valorAserRetirado);
        }

        [Theory]
        [InlineData(100000, 1000)]
        [InlineData(2000, 1000)]
        [InlineData(1000, 1000)]
        public void Sacar_Test_Param_Success(decimal valorConta, decimal valorSaque)
        {
            objConta.ValorConta = valorConta;

            objConta.Sacar(valorSaque);

            Assert.True(valorConta >= valorSaque);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 2)]
        [InlineData(1000, 3000)]
        public void Sacar_Test_Param_Exception(decimal valorConta, decimal valorSaque)
        {
            objConta.ValorConta = valorConta;
            Assert.Throws<Exception>(() => objConta.Sacar(valorSaque));
        }

        [Fact]
        public void Sacar_Test_Exception()
        {
            objConta.ValorConta = 2000;

            var valorAserRetirado = 3000;

            Assert.Throws<Exception>(() => objConta.Sacar(valorAserRetirado));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(3000)]
        public void SetarValorConta_Sucess(decimal valor)
        {
            objConta.ValorConta = valor;

            Assert.True(objConta.ValorConta >= 0);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-1000)]
        [InlineData(-3000)]
        public void SetarValorConta_Fail(decimal valor) =>
            Assert.Throws<Exception>(() => objConta.ValorConta = valor);


        [Theory]
        [InlineData(100000, 1000)]
        [InlineData(2000, 1000)]
        [InlineData(1000, 1000)]
        [InlineData(0, 0)]
        public void Depositar_Sucess(decimal valorConta, decimal valorDeposito)
        {
            objConta.ValorConta = valorConta;
            objConta.Depositar(valorDeposito);

            Assert.True(valorDeposito >= 0);
        }

        [Theory]
        [InlineData(1, -2)]
        [InlineData(1000, -3000)]
        public void Depositar_Fail(decimal valorConta, decimal valorDeposito)
        {
            objConta.ValorConta = valorConta;
            Assert.Throws<Exception>(() => objConta.Depositar(valorDeposito));
        }

        [Theory]
        [InlineData("ContaCorrente1")]
        [InlineData("ConvaInvestimento")]
        [InlineData("Intercambio das Crianças")]
        [InlineData("Cesta de viagem")]
        public void SetarNome_Success(string nomeConta)
        {
            objConta.SetarNome(nomeConta);
            Assert.NotNull(objConta.NomeConta);
        }

        [Fact]
        public void Transferir_Success()
        {
            objConta.ValorConta = 100000;

            var contaDestino = new Conta()
            {
                ValorConta = 5000,
                Agencia = "0001",
                NumeroConta = "3256478",
                Digito = "0",
                Pix = "99999999999"
            };

            objConta.Transferir(contaDestino);
            Assert.NotNull(contaDestino);
            Assert.True(contaDestino.ValorConta > 0);
            Assert.NotNull(contaDestino.Agencia);
            Assert.NotNull(contaDestino.NumeroConta);
            Assert.NotNull(contaDestino.Pix);
        }

        [Fact]
        public void Transferir_ContaPara_Fail()
        {
            objConta.ValorConta = 100000;

            Conta contaDestino = null;

            Assert.Throws<Exception>(() => objConta.Transferir(contaDestino));
        }

        [Fact]
        public void Transferir_ValorConta_Fail()
        {
            objConta.ValorConta = 100000;

            var contaDestino = new Conta()
            {
                ValorConta = 0,
                Agencia = "0001",
                NumeroConta = "3256478",
                Digito = "0",
                Pix = "99999999999"
            };

            Assert.Throws<Exception>(() => objConta.Transferir(contaDestino));
        }

        [Fact]
        public void Transferir_DadosConta_Agencia_Fail()
        {
            objConta.ValorConta = 100000;

            var contaDestino = new Conta()
            {
                ValorConta = 5000,
                Agencia = "",
                NumeroConta = "3256478",
                Digito = "0",
                Pix = "99999999999"
            };

            Assert.Throws<Exception>(() => objConta.Transferir(contaDestino));
        }

        [Fact]
        public void Transferir_DadosConta_Conta_Fail()
        {
            objConta.ValorConta = 100000;

            var contaDestino = new Conta()
            {
                ValorConta = 5000,
                Agencia = "0001",
                NumeroConta = "",
                Digito = "0",
                Pix = "99999999999"
            };

            Assert.Throws<Exception>(() => objConta.Transferir(contaDestino));
        }

        [Fact]
        public void Transferir_DadosConta_Pix_Fail()
        {
            objConta.ValorConta = 100000;

            var contaDestino = new Conta()
            {
                ValorConta = 5000,
                Agencia = "0001",
                NumeroConta = "3256478",
                Digito = "0",
                Pix = ""
            };

            Assert.Throws<Exception>(() => objConta.Transferir(contaDestino));
        }

        [Fact]
        public void EncerrarConta_Success()
        {
            objConta.EncerrarConta();
            Assert.False(objConta.EstaAtiva);
        }
    }
}