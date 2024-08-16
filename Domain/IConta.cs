namespace Domain
{
    internal interface IConta
    {
        //properties
        string Agencia { get; set; }
        string NumeroConta { get; set; }
        string Digito { get; set; }
        string? Pix { get; set; }
        double ValorConta { get; set; }
        ETipoConta TipoConta { get; set; }
        string NomeConta { get; set; }

        //métodos
        string Sacar(double valor);
        string Depositar(double valor);
        double VerSaldo();
        void SetarNome(string nome);
    }
}