using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ContaPessoaJuridicaDomain : Conta
    {
        [Required]
        public string CNPJ { get; set; } = string.Empty;

        [Required]
        public string RazaoSocial { get; set; } = string.Empty;

        public string NomeFantasia { get; set; } = string.Empty;

        [Required]
        public double ValorInicial { get; set; }
        public double FaturamentoMedio { get; set; }


        public ContaPessoaJuridicaDomain()
        {
        }

        public ContaPessoaJuridicaDomain(string nomeConta, ETipoConta tipoConta)
        {
            TipoConta = tipoConta;
            NomeConta = nomeConta;
        }

        public override void Sacar(decimal value) => base.Sacar(value);


        public override void Depositar(decimal value) => base.Depositar(value);

        public override decimal VerSaldo() => base.VerSaldo();//é o mesmo que
        //public override double VerSaldo()
        //{
        //    base.VerSaldo();

        //}
        public override void Transferir(Conta contaPara) => base.Transferir(contaPara);
    }
}