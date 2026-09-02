using System.ComponentModel.DataAnnotations;

namespace WFConfin.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 200 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O login do usuário é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O login deve ter entre 3 e 20 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha do usuário é obrigatória")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 20 caracteres")]  
        public string Password { get; set; }
        
        [Required(ErrorMessage = "A função do usuário é obrigatória")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "A função deve ter entre 3 e 20 caracteres")]
        public string Funcao { get; set; }






    }
}
