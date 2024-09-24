using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Login
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "O campo UserName é obrigatório!")]
        [StringLength(100, ErrorMessage = "O tamanho máximo do campo UserName é de 100 caracteres!")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Password é obrigatório!")]
        [PasswordPropertyText]
        [StringLength(100, ErrorMessage = "O tamanho do campo Password deverá ser entre 6 e 100 caracteres!", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
    }
}