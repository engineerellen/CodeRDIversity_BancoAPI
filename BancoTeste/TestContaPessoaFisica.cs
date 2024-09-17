namespace Domain.Tests
{
    public class TestContaPessoaFisica
    {
        ContaPessoaFisicaDomain objContaPF;
        public TestContaPessoaFisica() =>
            objContaPF = new ContaPessoaFisicaDomain("Compra imovel", ETipoConta.Investimento);

        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        public void SacarTest_Emprestimo_Sucess(decimal valorSaque)
        {
            objContaPF.ValorConta = 3000;
            decimal valorConta = objContaPF.ValorConta;
            objContaPF.Emprestimo = true;
            var juros = new Conta().juros;

            objContaPF.Sacar(valorSaque);

            Assert.Equal(objContaPF.ValorConta, valorConta - valorSaque - (valorConta * juros));
        }


        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        public void SacarTest_Base_Sucess(decimal valorSaque)
        {
            objContaPF.ValorConta = 3000;
            decimal valorConta = objContaPF.ValorConta;
            objContaPF.Emprestimo = false;

            objContaPF.Sacar(valorSaque);

            Assert.Equal(objContaPF.ValorConta, valorConta - valorSaque);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        public void DepositarTest_Sucess(decimal valorDeposito)
        {
            objContaPF.ValorConta = 3000;
            decimal valorConta = objContaPF.ValorConta;

            objContaPF.Depositar(valorDeposito);

            Assert.Equal(objContaPF.ValorConta, valorConta + valorDeposito);
        }

        [Fact]
        public void VerSaldoTest()
        {
            objContaPF.ValorConta = 3000;

            Assert.Equal(objContaPF.VerSaldo(), objContaPF.ValorConta);
        }

        [Fact]
        public void SetarNomeTest()
        {
            objContaPF.SetarNome("Mudança de país");

            Assert.NotEmpty(objContaPF.NomeConta);
        }

        [Fact]
        public void TransferirTest()
        {
            objContaPF.ValorConta = 100000;

            var contaDestino = new Conta()
            {
                ValorConta = 5000,
                Agencia = "0001",
                NumeroConta = "3256478",
                Digito = "0",
                Pix = "99999999999"
            };

            objContaPF.Transferir(contaDestino);
            Assert.NotNull(contaDestino);
            Assert.True(contaDestino.ValorConta > 0);
            Assert.NotNull(contaDestino.Agencia);
            Assert.NotNull(contaDestino.NumeroConta);
            Assert.NotNull(contaDestino.Pix);
        }
    }
}