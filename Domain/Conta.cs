namespace Domain
{
    public abstract class Conta : IConta
    {
        private double _valorConta;
        private ETipoConta _tipoConta;

        protected int Codigo {get; set;}
        public double ValorConta { get; set; } = 0;
        public string NomeConta { get; set; } = "Conta1";
        public ETipoConta TipoConta { get; set; } = ETipoConta.Corrente;


        public string Sacar(double valor)
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

        public string Depositar(double valor)
        {
            _valorConta += valor;
            // ou fazer desta forma:
            // _valorConta = _valorConta + valor;

            return "Depósito efetuado com sucesso!";
        }

        public double VerSaldo()
        {
            return _valorConta;
        }

        public void SetarNome()
        { }

        public List<Conta> VerExtrato()
        {
            return new List<Conta>();
        }
    }
}