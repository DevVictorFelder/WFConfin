using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFConfin.Models
{
    public class Conta
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A descrição da conta é obrigatória")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 e 200 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O valor da conta é obrigatório")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "A data de vencimento da conta é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DataVencimento { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataPagamento { get; set; }

        [Required(ErrorMessage = "A situação da conta é obrigatória")]
        public Situacao Situacao { get; set; }

        [Required(ErrorMessage = "A pessoa associada à conta é obrigatória")]
        public Guid PessoaId { get; set; } // Propriedade que representa a chave estrangeira para a pessoa, indicando a qual pessoa a conta pertence


        public Conta()
        {
            Id = Guid.NewGuid();
        }


        public Pessoa Pessoa { get; set; } //Relacionamento com entity framework, indicando que a conta pertence a uma pessoa
    }
}
