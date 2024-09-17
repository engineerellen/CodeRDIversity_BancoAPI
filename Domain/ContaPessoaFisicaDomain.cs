using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ContaPessoaFisicaDomain : Conta
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

        public bool Emprestimo { get; set; }


        //construtor da classe ContaPF
        public ContaPessoaFisicaDomain()
        {

        }
        public ContaPessoaFisicaDomain(string nomeConta, ETipoConta tipoConta)
        {
            NomeConta = nomeConta;
            this.TipoConta = tipoConta;
        }

        public override void Sacar(decimal value)
        {
            if (Emprestimo)
                ValorConta = ValorConta - value - (ValorConta * juros);
            else
                base.Sacar(value);
        }

        public override void Depositar(decimal value)
        {
            //if(DevendoPensao())
            //    Transferir()
            //        else

            base.Depositar(value);
        }


        public override decimal VerSaldo() =>
            base.VerSaldo();

        public override sealed void SetarNome(string nome) => base.SetarNome(nome);

        public override void Transferir(Conta contaPara) =>
            base.Transferir(contaPara);
    }
}