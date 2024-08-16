using Domain;

//objeto da classe conta com upcasting
Conta objContaPF1 = new ContaPessoaFisica();

//instancia do objeto pessoa juridica
ContaPessoaJuridica objContaPJ1 = new ContaPessoaJuridica();
objContaPJ1.RazaoSocial = "Empresa dos meus sonhos!";

//downcasting
Conta objPJ2 = objContaPJ1;
ContaPessoaJuridica objContaPJ3 = (ContaPessoaJuridica)objPJ2;

var retorno = objPJ2.Sacar(20000);
Console.WriteLine($"{retorno} para o saque da {objContaPJ3.RazaoSocial} ");


//usando o "AS"
Conta objConta = new ContaPessoaFisica();

if (objConta is ContaPessoaFisica)
{
    ContaPessoaFisica? objPF = objConta as ContaPessoaFisica;

    objPF.NomeCliente = "Ellen";
    objPF.Profissao = "Professora";
    objPF.ValorConta = 200000000.00;
    objPF.TipoConta = ETipoConta.Investimento;

    var retornoInvestimento = objPF.Depositar(100000);
    Console.WriteLine($"{retornoInvestimento} do Investimento da {objPF.NomeCliente}");
}