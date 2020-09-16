using APIDesafio.DTOs;
using ApiTeste.Models;
using ApiTeste.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTeste.Services
{
    public class DesenvolvedorService
    {
        private IUnitOfWork _unitOfWork;
        private List<DesenvolvedorResumoDTO> Desenvolvedores { get; set; }
        private List<Ponto> Pontos { get; set; }
        public DesenvolvedorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Desenvolvedores = new List<DesenvolvedorResumoDTO>();
            Pontos = new List<Ponto>();
        }

        public List<DesenvolvedorResumoDTO> GetDesenvolvedoresByRacking()
        {
            try
            {
                int total = 0;
                foreach (DesenvolvedorResumoDTO d in Desenvolvedores)
                {
                    total += d.TotalHoras;
                }
                int media = total / Desenvolvedores.Count;
                return Desenvolvedores.OrderBy(d => d.TotalHoras).Where(d => d.TotalHoras > media).Take(5).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }  
        }

        public void ObterTotalSemanal(DateTime inicioSemana)
        {
            if(inicioSemana == null || inicioSemana >= DateTime.Now)
            {
                throw new Exception("Data inválida!");
            }
            DateTime fimDeSemana = inicioSemana.AddDays(6);
            Pontos = (List<Ponto>)_unitOfWork.PontoRepository.GetPontosByPeriodo(inicioSemana, fimDeSemana);
            List<Desenvolvedor> ListaDesenvolvedoresParticipantes = new List<Desenvolvedor>();
            foreach (Ponto p in Pontos)
            {
                var dev = p.Desenvolvedor;
                dev.TotalSemanal += p.TempoTrabalho;
                ListaDesenvolvedoresParticipantes.Add(dev);
            }
            foreach(Desenvolvedor d in ListaDesenvolvedoresParticipantes)
            {
                var dev = new DesenvolvedorResumoDTO(d.Nome, d.Sobrenome, d.LinkedinUrl, d.GithubUrl);
                dev.TotalHoras = d.TotalSemanal;
                Desenvolvedores.Add(dev);
            }
        }
    }
}
