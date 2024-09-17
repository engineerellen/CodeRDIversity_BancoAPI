using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Conta : IConta
    {
        public decimal juros = (decimal)0.12;

        private decimal _valorConta = 0;

        public int IDConta { get; set; }

        [Required]
        public string Agencia { get; set; } = "0001";

        [Required]
        public string NumeroConta { get; set; } = string.Empty;

        [Required]
        public string Digito { get; set; } = "0";
        public string? Pix { get; set; }

        public bool EstaAtiva { get; set; } = true;

        [Required]
        public decimal ValorConta
        {
            get => _valorConta;
            set
            {
                if (value >= 0)
                    _valorConta = value;

                else
                    throw new Exception("O valor da conta informado deverá ser maior que 0!");
            }
        }

        public string NomeConta { get; set; } = "Conta1";

        [Required]
        public ETipoConta TipoConta { get; set; } = ETipoConta.Corrente;

        public string DescricaoTipoConta { get; set; } = string.Empty;

        public virtual void Sacar(decimal valorSaque)
        {
            if (_valorConta > 0 && valorSaque <= _valorConta)
                _valorConta -= valorSaque;
            // ou fazer desta forma:
            // _valorConta = _valorConta - valorSaque;
            else
                throw new Exception("Valor de saque inválido!");
        }

        public virtual void Depositar(decimal valorADepositar)
        {
            if (valorADepositar >= 0)
                _valorConta += valorADepositar;
            // ou fazer desta forma:
            // _valorConta = _valorConta + valorSaque;

            else
                throw new Exception("valor para depósito não pode ser negativo!");
        }


        public virtual decimal VerSaldo() => _valorConta;


        public virtual void SetarNome(string nome) =>
            NomeConta = nome;

        public virtual void Transferir(Conta contaPara)
        {
            if (contaPara is null)
                throw new Exception("Favor, informar conta destino!");

            if (contaPara.ValorConta <= 0)
                throw new Exception("Favor, informar um valor válido para transferência!");

            if (string.IsNullOrEmpty(contaPara.Agencia) ||
                string.IsNullOrEmpty(contaPara.NumeroConta) ||
                string.IsNullOrEmpty(contaPara.Pix))
                throw new Exception("favor, informar dados para transferência!");

            contaPara.ValorConta += contaPara.ValorConta;
            _valorConta -= contaPara.ValorConta;
        }

        public void EncerrarConta()
        {
            if (ValorConta < 0)
                throw new Exception("Não é possivel encerrar a conta, Verifique seu saldo!");

            EstaAtiva = false;
        }
    }
}