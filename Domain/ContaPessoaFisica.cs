using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ContaPessoaFisica : Conta
    {
        [Required]
        public int ID_ContaPF { get; set; }

        [Required]
        public string NomeCliente { get; set; } = string.Empty;

        [Required]
        public string CPF { get; set; } = string.Empty;

        public string Genero { get; set; } = string.Empty;

        [Required]
        public string Endereco { get; set; } = string.Empty;
        public string Profissao { get; set; } = string.Empty;

        public decimal RendaFamiliar { get; set; }


        //construtor da classe ContaPF
        public ContaPessoaFisica()
        {

        }
        public ContaPessoaFisica(string nomeConta, ETipoConta tipoConta)
        {
            NomeConta = nomeConta;
            this.TipoConta = tipoConta;
        }

        public override void Sacar(decimal value) => base.Sacar(value);


        public override void Depositar(decimal value) => base.Depositar(value);

        public override decimal VerSaldo() =>
            base.VerSaldo();


        public override sealed void SetarNome(string nome) => base.SetarNome(nome);

        public override void Transferir(Conta contaPara) =>
            base.Transferir(contaPara);
    }
}