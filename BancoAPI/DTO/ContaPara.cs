using Domain;

namespace BancoAPI.DTO
{
    public class ContaPara
    {
        public string Pix { get; set; } = string.Empty;

        public string Agencia { get; } = string.Empty;

        public string NumeroConta { get; } = string.Empty;

        public string Digito { get; } = string.Empty;

        public string NomeCorrentista { get; } = string.Empty;

        public double Valor { get; set; }

        public string TipoContaPFPJ { get; set; } = string.Empty;

        public ETipoConta TipoConta { get; set; }

        public ContaPessoaFisica TransferirConta(ContaPara contaPara)
        {
            Conta contaPF = new ContaPessoaFisica();

            contaPF.Pix = contaPara.Pix;
            contaPF.Agencia = contaPara.Agencia;
            contaPF.NumeroConta = contaPara.NumeroConta;
            contaPF.Digito = contaPara.Digito;
            contaPF.ValorConta = contaPF.ValorConta;
            contaPF.TipoConta = contaPara.TipoConta; 

            contaPF.Transferir(contaPF);

            return (ContaPessoaFisica)contaPF;
        }
    }
}