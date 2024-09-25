using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class SenhaHash
    {
        /* é um método recursivo usado para gerar um hash. O algoritmo de hash usado é SHA256.
        O processo de combinação de senha, salt e pepper ocorre nesta linha de código:
         var passwordSaltPepper = $"{senha}{salt}{pepper}";

        Semelhante ao cozimento, o processo de adição de sal e pimenta pode ser ajustado de acordo com o gosto até atingir a complexidade desejada.
        O resultado de hash na matriz de bytes é convertido em uma string de base 64, que é reinserida como um parâmetro de senha na função ComputeHash() até que o processo de iteração seja concluído.
        */
        public static string ComputeHash(string senha, string salt, string? pepper, int iteration)
        {
            if (iteration <= 0) return senha;

            using var sha256 = SHA256.Create();
            var passwordSaltPepper = $"{senha}{salt}{pepper}";
            var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
            var byteHash = sha256.ComputeHash(byteValue);
            var hash = Convert.ToBase64String(byteHash);
            return ComputeHash(hash, salt, pepper, iteration - 1);
        }

        //é usado para gerar um salt de bytes aleatórios e convertê-lo como uma string de base 64.
        public static string GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            var byteSalt = new byte[16];
            rng.GetBytes(byteSalt);
            var salt = Convert.ToBase64String(byteSalt);
            return salt;
        }
    }
}