using APIDesafio.DTOs;
using ApiTeste.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiTeste.DTOs
{
    public class ProjetoDTO
    {
        public int ProjetoId { get; set; }

        [Required]
        [Display(Name = "Informe uma descrição curta:")]
        [StringLength(60, MinimumLength = 3)]
        public string DescricaoCurta { get; set; }

        [Required]
        [Display(Name = "Informe uma descrição detalhada sobre o projeto:")]
        [StringLength(800, MinimumLength = 3)]
        public string DescricaoDetalhada { get; set; }

        [Required]
        [Display(Name = "Informe os objetivos que pretende se alcançar com o projeto:")]
        [StringLength(150, MinimumLength = 3)]
        public string Objetivo { get; set; }

        [Required]
        [Display(Name = "Informe os principais recursos serão utilizados para com o projeto:")]
        [StringLength(300, MinimumLength = 3)]
        public string Recursos { get; set; }

        [Required]
        [Display(Name = "Informe o público alvo do projeto:")]
        [StringLength(60, MinimumLength = 3)]
        public string PublicoAlvo { get; set; }

        [Required]
        [Display(Name = "Informe o setor que se encontra o projeto:")]
        [StringLength(60, MinimumLength = 3)]
        public string Setor { get; set; }

        [Required]
        public Fase Fase { get; set; }

        [Required]
        public DataResumoDTO DataInicio { get; set; }
        public DataResumoDTO DataPrazo { get; set; }
    }
}
