using System.ComponentModel.DataAnnotations;

namespace ApiTeste.Models.ObjectTypes
{
    public class Endereco
    {
        public int EnderecoId { get; set; }

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

        public string DesenvolvedorId { get; set; }
        public Desenvolvedor Desenvolvedor { get; set; }

        public Endereco()
        {

        }
        public Endereco(string logradouro, int numero, string cEP, string bairro, string cidade, string estado, string pais)
        {
            Logradouro = logradouro;
            Numero = numero;
            CEP = cEP;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
        }

        public Endereco(string logradouro, int numero, string cEP, 
            string bairro, string cidade, string estado, string pais, string desenvolvedorId)
        {
            Logradouro = logradouro;
            Numero = numero;
            CEP = cEP;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            DesenvolvedorId = desenvolvedorId;
        }
    }
}
