namespace Domain
{
    internal interface IConta
    {
        double ValorConta { get; set; }
        ETipoConta TipoConta { get; set; }
        string NomeConta { get; set; }

        string Sacar(double valor);
        string Depositar(double valor);
        double VerSaldo();
        void SetarNome();
    }
}