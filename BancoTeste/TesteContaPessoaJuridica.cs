using Domain;

namespace BancoTeste;

public class TesteContaPessoaJuridica
{
    ContaPessoaJuridicaDomain objContaPJ;

    public TesteContaPessoaJuridica() =>
        objContaPJ = new ContaPessoaJuridicaDomain("Conta Fluxo Caixa", ETipoConta.Corrente);


    [Theory]
    [InlineData(1000)]
    [InlineData(2000)]
    [InlineData(3000)]
    public void SacarTest_Base_Sucess(decimal valorSaque)
    {
        objContaPJ.ValorConta = 300000;
        decimal valorConta = objContaPJ.ValorConta;

        objContaPJ.Sacar(valorSaque);

        Assert.Equal(objContaPJ.ValorConta, valorConta - valorSaque);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(200)]
    [InlineData(300)]
    public void DepositarTest_Sucess(decimal valorDeposito)
    {
        objContaPJ.ValorConta = 3000;
        decimal valorConta = objContaPJ.ValorConta;

        objContaPJ.Depositar(valorDeposito);

        Assert.Equal(objContaPJ.ValorConta, valorConta + valorDeposito);
    }

    [Fact]
    public void VerSaldoTest()
    {
        objContaPJ.ValorConta = 3000;

        Assert.Equal(objContaPJ.VerSaldo(), objContaPJ.ValorConta);
    }

    [Fact]
    public void TransferirTest()
    {
        objContaPJ.ValorConta = 100000;

        var contaDestino = new Conta()
        {
            ValorConta = 5000,
            Agencia = "0001",
            NumeroConta = "3256478",
            Digito = "0",
            Pix = "99999999999"
        };

        objContaPJ.Transferir(contaDestino);
        Assert.NotNull(contaDestino);
        Assert.True(contaDestino.ValorConta > 0);
        Assert.NotNull(contaDestino.Agencia);
        Assert.NotNull(contaDestino.NumeroConta);
        Assert.NotNull(contaDestino.Pix);
    }
}