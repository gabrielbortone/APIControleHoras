using APIDesafio.DTOs;
using ApiTeste.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiTeste.DTOs
{
    public class DesenvolvedorDTO
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Informe o seu nome")]
        [StringLength(30, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Informe o seu sobrenome")]
        [StringLength(30, MinimumLength = 3)]
        public string Sobrenome { get; set; }

        [Required]
        [Display(Name = "Informe o seu número de CPF")]
        [StringLength(11, MinimumLength = 11)]
        public string CPF { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Informe a url de seu linkedin")]
        [StringLength(60, MinimumLength = 11)]
        public string LinkedinUrl { get; set; }

        [Required]
        [Display(Name = "Informe a url de seu github")]
        [StringLength(60, MinimumLength = 11)]
        public string GithubUrl { get; set; }

        [Required]
        [Display(Name = "Informe a data de contratação")]
        public DataResumoDTO DataContratacao { get; set; }


        [Required]
        [Display(Name = "Informe o seu logradouro")]
        [StringLength(30, MinimumLength = 3)]
        public string Logradouro { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        [Display(Name = "Informe o CEP:")]
        [StringLength(8, MinimumLength = 8)]
        public string CEP { get; set; }

        [Required]
        [Display(Name = "Informe o bairro:")]
        [StringLength(30, MinimumLength = 4)]
        public string Bairro { get; set; }

        [Required]
        [Display(Name = "Informe a cidade:")]
        [StringLength(35, MinimumLength = 4)]
        public string Cidade { get; set; }

        [Required]
        [Display(Name = "Informe o Estado:")]
        [StringLength(35, MinimumLength = 4)]
        public string Estado { get; set; }

        [Required]
        [Display(Name = "Informe o País:")]
        [StringLength(35, MinimumLength = 3)]
        public string Pais { get; set; }

        [Required]
        public TipoTelefone Tipo { get; set; }

        [Required]
        [Display(Name = "Informe o seu número:")]
        [StringLength(12, MinimumLength = 8)]
        public string NumeroTelefone { get; set; }
    }
}
