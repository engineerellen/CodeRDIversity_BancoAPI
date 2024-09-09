using Domain;

//Exemplo de Abstract Factory
Console.WriteLine("Insira se você é F para Pessoa Fisica ou J para Pessoa Jurídica");

string valor = Console.ReadLine();
Conta objConta = null;

switch (valor)
{
    case "F":
        objConta = new ContaPessoaFisicaDomain();
        break;

    case "J":
        objConta = new ContaPessoaJuridicaDomain();
        break;

    default:
        Console.WriteLine("Conta Inválida!");
        break;
}

objConta?.SetarNome($"Nome Conta {valor}");
objConta?.Depositar(100);

if (objConta is not null)
    Console.WriteLine($"O saldo da conta é {objConta?.VerSaldo()}");
//Fim do Exemplo de Abstract Factory


//objeto da classe conta com upcasting
Conta objContaPF1 = new ContaPessoaFisicaDomain("Conta da Ellen", ETipoConta.Corrente) { ValorConta = 100 };
objContaPF1.VerSaldo();

//instancia do objeto pessoa juridica
//usar apenas a expressao new() para instanciar
ContaPessoaJuridicaDomain objContaPJ1 = new ContaPessoaJuridicaDomain("Conta Corrente 1", ETipoConta.Corrente) { RazaoSocial = "Empresa dos meus sonhos!", Agencia = "0001", NumeroConta = "35698", Digito = "0" };


//downcasting
Conta objPJ2 = objContaPJ1;
ContaPessoaJuridicaDomain objContaPJ3 = (ContaPessoaJuridicaDomain)objPJ2;

objPJ2.Sacar(20000);

Console.WriteLine($"O valor de R${objPJ2.ValorConta} foi sacado da empresa: {objContaPJ3.RazaoSocial} ");


//usando o "AS"
Conta objConta1 = new ContaPessoaFisicaDomain("Cofrinho para viagem", ETipoConta.Investimento);

if (objConta1 is ContaPessoaFisicaDomain)
{
    ContaPessoaFisicaDomain? objPF = objConta1 as ContaPessoaFisicaDomain;

    objPF.NomeCliente = "Ellen";
    objPF.Profissao = "Professora";
    objPF.ValorConta = (decimal)20000000.45;

    objPF?.Depositar(100000);
    Console.WriteLine($"O valor de R${objPF.ValorConta} foi depositado no Investimento da {objPF.NomeCliente}");
}