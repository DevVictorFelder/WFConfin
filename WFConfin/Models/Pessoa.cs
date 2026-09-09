using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WFConfin.Models
{
    public class Pessoa // uma anotacao que utilizando o entity framework, indica que a classe abaixo é uma entidade do banco de dados, e que as propriedades abaixo serao mapeadas para colunas da tabela Pessoa
    {
        [Key]//uma anotacao que utilizando o entity framework, indica que a propriedade abaixo é a chave primaria da tabela
        public Guid Id { get; set; }// Propriedade que representa a chave primária da tabela Pessoa, do tipo Guid

        [Required(ErrorMessage = "O nome da pessoa é obrigatório")]//uma anotacao que utilizando o entity framework, indica que a propriedade abaixo é obrigatoria e um errorMessage que sera exibido caso a validacao falhe
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome da pessoa deve ter entre 3 e 200 caracteres")]//uma anotacao que utilizando o entity framework, indica que a propriedade abaixo é obrigatoria e um errorMessage que sera exibido caso a validacao falhe, e que a propriedade abaixo tem tamanho maximo de 200 caracteres, e tamanho minimo de 3 caracteres e um errorMessage que sera exibido caso a validacao falhe
        public string Nome { get; set; }// Propriedade que representa o nome da pessoa, com validação de obrigatoriedade e tamanho mínimo e máximo

        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres")]//uma anotacao que utilizando o entity framework, indica que a propriedade abaixo tem tamanho maximo de 20 caracteres e um errorMessage que sera exibido caso a validacao falhe
        public string Telefone { get; set; }// Propriedade que representa o telefone da pessoa, com validação de tamanho máximo de 20 caracteres

        [EmailAddress(ErrorMessage = "O email informado não é válido")]//uma anotacao que utilizando o entity framework, indica que a propriedade abaixo é do tipo email e um errorMessage que sera exibido caso a validacao falhe
        public string Email { get; set; }// Propriedade que representa o email da pessoa, com validação de formato de email

        [DataType(DataType.Date)]//uma anotacao que utilizando o entity framework, indica que a propriedade abaixo é do tipo data
        public DateTime? DataNascimento { get; set; }// Propriedade que representa a data de nascimento da pessoa, com validação de tipo data

        [Column(TypeName = "decimal(18,2)")]//uma anotacao que utilizando o entity framework, indica que a propriedade abaixo é do tipo decimal, com 18 digitos no total e 2 digitos apos a virgula
        public decimal Salario { get; set; }// Propriedade que representa o salário da pessoa, com validação de tipo decimal e precisão de 18 dígitos no total e 2 dígitos após a vírgula

        [StringLength(20, ErrorMessage = "O gênero deve ter no máximo 20 caracteres")]
        public string Genero { get; set; }// Propriedade que representa o gênero da pessoa, com validação de tamanho máximo de 20 caracteres

        public Guid? CidadeId { get; set; }// Propriedade que representa a chave estrangeira para a cidade, indicando a qual cidade a pessoa pertence

        public Pessoa()// Construtor da classe Pessoa, que inicializa o Id com um novo Guid
        {
            Id = Guid.NewGuid();// Inicializa o Id com um novo Guid
        }

        //Relacionamento com entity framework, indicando que a pessoa pertence a uma cidade
        public Cidade Cidade { get; set; }

    }
}
