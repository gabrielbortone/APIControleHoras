using ApiTeste.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiTeste.Models
{
    public class Projeto
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
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataPrazo { get; set; }

        public virtual IEnumerable<Ponto> ControleHoras { get; set; }
        public Projeto()
        {

        }

        public Projeto(string descricaoCurta, string descricaoDetalhada, string objetivo, string recursos, string publicoAlvo, string setor, Fase fase, DateTime dataInicio)
        {
            DescricaoCurta = descricaoCurta;
            DescricaoDetalhada = descricaoDetalhada;
            Objetivo = objetivo;
            Recursos = recursos;
            PublicoAlvo = publicoAlvo;
            Setor = setor;
            Fase = fase;
            DataInicio = dataInicio;
        }
        public Projeto(int projetoId, string descricaoCurta, string descricaoDetalhada, string objetivo, string recursos, string publicoAlvo, string setor, Fase fase, DateTime dataInicio)
        {
            ProjetoId = projetoId;
            DescricaoCurta = descricaoCurta;
            DescricaoDetalhada = descricaoDetalhada;
            Objetivo = objetivo;
            Recursos = recursos;
            PublicoAlvo = publicoAlvo;
            Setor = setor;
            Fase = fase;
            DataInicio = dataInicio;
        }
    }
}
