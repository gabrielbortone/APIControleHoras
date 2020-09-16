using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTeste.Models
{
    public class Ponto
    {
        public int PontoId { get; set; }
        
        [Required]
        public DateTime InicioJornada { get; set; }
        
        [Required]
        public DateTime FimJornada { get; set; }
        
        [NotMapped]
        public virtual int TempoTrabalho { get; set; }

        [Required]
        public Desenvolvedor Desenvolvedor { get; set; }

        [Required]
        public Projeto Projeto { get; set; }

        public Ponto()
        {
            TempoTrabalho = 0;
        }
        public Ponto(DateTime inicio, DateTime fim, Desenvolvedor desenvolvedor, Projeto projeto)
        {
            InicioJornada = inicio;
            FimJornada = fim;
            Desenvolvedor = desenvolvedor;
            Projeto = projeto;
            TempoTrabalho = (FimJornada - InicioJornada).Hours;
        }

        public Ponto(int id, DateTime inicio, DateTime fim, Desenvolvedor desenvolvedor, Projeto projeto)
        {
            PontoId = id;
            InicioJornada = inicio;
            FimJornada = fim;
            Desenvolvedor = desenvolvedor;
            Projeto = projeto;
            TempoTrabalho = (FimJornada - InicioJornada).Hours;
        }
    }
}
