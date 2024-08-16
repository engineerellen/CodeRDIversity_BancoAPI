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
    }
}