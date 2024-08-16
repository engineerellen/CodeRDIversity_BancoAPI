using Domain;

//objeto da classe conta com upcasting
Conta objContaPF1 = new ContaPessoaFisica() { ValorConta = 100 };
objContaPF1.VerSaldo();

//instancia do objeto pessoa juridica
//usar apenas a expressao new() para instanciar
ContaPessoaJuridica objContaPJ1 = new() { RazaoSocial = "Empresa dos meus sonhos!", Agencia="0001", NumeroConta="35698", Digito="0"};


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