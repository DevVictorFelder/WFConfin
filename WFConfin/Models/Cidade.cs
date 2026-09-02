using System.ComponentModel.DataAnnotations;

namespace WFConfin.Models
{
    public class Cidade
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da cidade é obrigatório")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome da cidade deve ter entre 3 e 200 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A sigla do estado é obrigatória")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A sigla do estado deve ter exatamente 2 caracteres")]
        public string EstadoSigla { get; set; }

        public Cidade()
        {
            Id = Guid.NewGuid();
        }

        //Relacionamento com entity framework, indicando que a cidade pertence a um estado
        public Estado Estado { get; set; }



    }
}
