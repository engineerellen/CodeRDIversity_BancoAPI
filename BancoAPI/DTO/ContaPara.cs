using Microsoft.AspNetCore.SignalR;

namespace BancoAPI.DTO
{
    public class ContaPara
    {
        public string Pix { get; set; } = string.Empty;

        public string Agencia { get; } = string.Empty;

        public string NumeroConta { get; } = string.Empty;

        public string NomeCorrentista { get; } = string.Empty;

        public double Valor { get; set; }

        public string TipoContaPFPJ { get; set; } = string.Empty;
    }
}