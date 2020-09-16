using ApiTeste.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiTeste.Models.ObjectTypes
{
    public class Telefone
    { 
        public int TelefoneId { get; set; }

        [Required]
        public TipoTelefone Tipo { get; set; }

        [Required]
        [Display(Name = "Informe o seu número:")]
        [StringLength(12, MinimumLength = 8)]
        public string Numero { get; set; }

        public string DesenvolvedorId { get; set; }
        public Desenvolvedor Desenvolvedor { get; set; }

        public Telefone()
        {

        }
        public Telefone(TipoTelefone tipo, string numero)
        {
            Tipo = tipo;
            Numero = numero;
        }
    }
}
