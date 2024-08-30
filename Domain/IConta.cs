namespace Domain
{
    internal interface IConta
    {
        //properties
        string Agencia { get; set; }
        string NumeroConta { get; set; }
        string Digito { get; set; }
        string? Pix { get; set; }
        decimal ValorConta { get; set; }
        ETipoConta TipoConta { get; set; }
        string NomeConta { get; set; }

        //métodos
        void Sacar(decimal valor);
        void Depositar(decimal valor);
        decimal VerSaldo();
        void SetarNome(string nome);
        void Transferir(Conta conta);

        void EncerrarConta();
    }
}