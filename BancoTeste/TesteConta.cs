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
    }
}