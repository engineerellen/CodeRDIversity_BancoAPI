using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class Conta : IConta
    {
        private double _valorConta = 0;

        [Required]
        public int Codigo { get; set; }

        [Required]
        public string Agencia { get; set; } = "0001";

        [Required]
        public string NumeroConta { get; set; } = string.Empty;

        [Required]
        public string Digito { get; set; } = "0";
        public string? Pix { get; set; }

        public bool EstaAtiva { get; set; } = true;

        [Required]
        public double ValorConta
        {
            get => _valorConta;
            set
            {
                if (_valorConta >= 0)
                    _valorConta = value;

                else
                    throw new Exception("O valor da conta informado deverá ser maior que 0!");
            }
        }

        public string NomeConta { get; set; } = "Conta1";

        [Required]
        public ETipoConta TipoConta { get; set; } = ETipoConta.Corrente;

        public virtual void Sacar(double valor)
        {
            if (_valorConta > 0 && valor <= _valorConta)
                _valorConta -= valor;
            // ou fazer desta forma:
            // _valorConta = _valorConta - valor;
            else
                throw new Exception("Valor de saque inválido!");
        }

        public virtual void Depositar(double valor) =>
            _valorConta += valor;
        // ou fazer desta forma:
        // _valorConta = _valorConta + valor;

        public virtual double VerSaldo() => _valorConta;


        public virtual void SetarNome(string nome) =>
            NomeConta = nome;

        public virtual List<Conta> VerExtrato(DateTime data)
        {
            var lstConta = new List<Conta>();
            lstConta.Add(this);
            return lstConta;
        }

        public virtual void Transferir(Conta contaPara)
        {
            contaPara.ValorConta += contaPara.ValorConta;
            _valorConta -= contaPara.ValorConta;
        }

        public void EncerrarConta(int codigoConta)
        {
            if (ValorConta < 0)
                throw new Exception("Não é possivel encerrar a conta, Verifique seu saldo!");

            Codigo = codigoConta;
            EstaAtiva = false;
        }
    }
}