using Domain;

//objeto da classe conta com upcasting
Conta objContaPF1 = new ContaPessoaFisica("Conta da Ellen", ETipoConta.Corrente) { ValorConta = 100 };
objContaPF1.VerSaldo();

//instancia do objeto pessoa juridica
//usar apenas a expressao new() para instanciar
ContaPessoaJuridica objContaPJ1 = new ContaPessoaJuridica("Conta Corrente 1", ETipoConta.Corrente) { RazaoSocial = "Empresa dos meus sonhos!", Agencia = "0001", NumeroConta = "35698", Digito = "0" };


//downcasting
Conta objPJ2 = objContaPJ1;
ContaPessoaJuridica objContaPJ3 = (ContaPessoaJuridica)objPJ2;

objPJ2.Sacar(20000);

Console.WriteLine($"O valor de R${objPJ2.ValorConta} foi sacado da empresa: {objContaPJ3.RazaoSocial} ");


//usando o "AS"
Conta objConta = new ContaPessoaFisica("Cofrinho para viagem", ETipoConta.Investimento);

if (objConta is ContaPessoaFisica)
{
    ContaPessoaFisica? objPF = objConta as ContaPessoaFisica;

    objPF.NomeCliente = "Ellen";
    objPF.Profissao = "Professora";
    objPF.ValorConta = (decimal)20000000.45;

    objPF.Depositar(100000);
    Console.WriteLine($"O valor de R${objPF.ValorConta} foi depositado no Investimento da {objPF.NomeCliente}");
}