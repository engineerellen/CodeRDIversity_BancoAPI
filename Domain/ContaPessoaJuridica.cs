using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ContaPessoaJuridica : Conta
    {
        [Required]
        public string CNPJ { get; set; } = string.Empty;

        [Required]
        public string RazaoSocial { get; set; } = string.Empty;

        public string NomeFantasia { get; set; } = string.Empty;

        [Required]
        public double ValorInicial { get; set; }
        public double FaturamentoMedio { get; set; }


        public ContaPessoaJuridica()
        {
        }

        public ContaPessoaJuridica(string nomeConta, ETipoConta tipoConta)
        {
            TipoConta = tipoConta;
            NomeConta = nomeConta;
        }

        public override void Sacar(double value) => base.Sacar(value);


        public override void Depositar(double value) => base.Depositar(value);

        public override double VerSaldo() => base.VerSaldo();//é o mesmo que
        //public override double VerSaldo()
        //{
        //    base.VerSaldo();

        //}
        public override void Transferir(Conta contaPara) => base.Transferir(contaPara);
    }
}