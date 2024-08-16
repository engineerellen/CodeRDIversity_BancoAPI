using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ContaPessoaJuridica : Conta
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public double ValorInicial { get; set; }
        public double FaturamentoMedio { get; set; }


        public ContaPessoaJuridica()
        { }

        public ContaPessoaJuridica(string nomeConta, ETipoConta tipoConta)
        {
            TipoConta = tipoConta;
            NomeConta = nomeConta;
        }

        public override string Sacar(double value)
        {
            Console.WriteLine("Temos Ofertas especiais para sua empresa!");

            return base.Sacar(value);
        }

        public override string Depositar(double value)
        {
            Console.WriteLine("Temos Ofertas especiais para sua empresa! Verifique com seu gerente!");

            return base.Depositar(value);
        }

        public override double VerSaldo() => base.VerSaldo();//é o mesmo que
        //public override double VerSaldo()
        //{
        //    base.VerSaldo();

        //}
    }
}