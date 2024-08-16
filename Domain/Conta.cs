namespace Domain
{
    public abstract class Conta : IConta
    {
        private double _valorConta;

        protected int Codigo { get; set; }
        public string Agencia { get; set; } = "0001";
        public string NumeroConta { get; set; } = string.Empty;
        public string Digito { get; set; } = "0";
        public string? Pix { get; set; }
        public double ValorConta { get; set; } = 0;
        public string NomeConta { get; set; } = "Conta1";
        public ETipoConta TipoConta { get; set; } = ETipoConta.Corrente;


        public virtual string Sacar(double valor)
        {
            if (_valorConta > 0 && valor <= _valorConta)
            {
                _valorConta -= valor;

                // ou fazer desta forma:
                // _valorConta = _valorConta - valor;

                return "Saque efetuado com sucesso!";
            }
            else
                return "Não foi possível realizar o saque!";
        }

        public virtual string Depositar(double valor)
        {
            _valorConta += valor;
            // ou fazer desta forma:
            // _valorConta = _valorConta + valor;

            return "Depósito efetuado com sucesso!";
        }

        public virtual double VerSaldo()
        {
            return _valorConta;
        }

        public virtual void SetarNome(string nome)
        {
            NomeConta = nome;
        }

        public static List<Conta> VerExtrato()
        {
            return new List<Conta>();
        }
    }
}