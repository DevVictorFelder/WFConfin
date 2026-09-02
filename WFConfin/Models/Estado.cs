using System.ComponentModel.DataAnnotations;

namespace WFConfin.Models
{
    public class Estado
    {
        [Key] //uma anotacion que utilizando o entity framework, indica que a propriedade abaixo é a chave primaria da tabela
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A sigla deve ter no minimo 2 caracteres")] //uma anotacao que utilizando o entity framework, indica que a propriedade abaixo tem tamanho maximo de 2 caracteres, e tamanho minimo de 2 caracteres e um errorMessage que sera exibido caso a validacao falhe
        public String Sigla { get; set; }

        [Required(ErrorMessage = "O nome do estado é obrigatorio")] //uma anotacao que utilizando o entity framework, indica que a propriedade abaixo é obrigatoria e um errorMessage que sera exibido caso a validacao falhe
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 a 200 caracteres")] //uma anotacao que utilizando o entity framework, indica que a propriedade abaixo tem tamanho maximo de 200 caracteres, e tamanho minimo de 3 caracteres e um errorMessage que sera exibido caso a validacao falhe
        public String Nome { get; set; }

    }
}
