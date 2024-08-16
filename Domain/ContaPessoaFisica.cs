using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ContaPessoaFisica:Conta
    {
        public string NomeCliente { get; set; }
        public string CPF { get; set; }
        public string Genero { get; set; }
        public string Endereco { get; set; }
        public string Profissao { get; set; }

        public double RendaFamiliar { get; set; }
        //construtor da classe Conta
        public ContaPessoaFisica()
        {
        

        }

        public ContaPessoaFisica(string nomeConta)
        {
           // _nomeConta = nomeConta;
        }

        public ContaPessoaFisica(ETipoConta tipoConta)
        {
          //this._tipoConta = tipoConta;
        }

        public virtual string Sacar(double value)
        {
            return base.Sacar(value);
        }

        public virtual string Depositar(double value)
        {
            return base.Depositar(value);
        }
        public virtual double VerSaldo()
        {
           return base.VerSaldo();
        }
      }
}
