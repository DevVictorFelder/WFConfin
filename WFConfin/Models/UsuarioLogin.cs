using System.ComponentModel.DataAnnotations;

namespace WFConfin.Models
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "O login do usuário é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O login deve ter entre 3 e 20 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha do usuário é obrigatória")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 20 caracteres")]
        public string Password { get; set; }
    }
}
