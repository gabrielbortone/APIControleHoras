using ApiTeste.Models;
using ApiTeste.Models.Data;
using ApiTeste.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTeste.Repository
{
    public class PontoRepository : Repository<Ponto>, IPontoRepository
    {
        public PontoRepository(AppDbContext context) : base(context)
        {
        }

        public Ponto GetById(int id)
        {
            return _context.Pontos.FirstOrDefault(p => p.PontoId == id);
        }

        public IEnumerable<Ponto> GetPontosByDesenvolvedor(string id_Desenvolvedor)
        {
            return _context.Pontos.Where(ch => ch.Desenvolvedor.Id == id_Desenvolvedor).ToList();
        }

        public IEnumerable<Ponto> GetPontosByPeriodo(DateTime inicio, DateTime fim)
        {
            return _context.Pontos.Where(ch => ch.InicioJornada >= inicio && ch.FimJornada <= fim).ToList();
        }

        public IEnumerable<Ponto> GetPontosByProjeto(int id_Projeto)
        {
            return _context.Pontos.Where(ch => ch.Projeto.ProjetoId == id_Projeto).ToList();
        }
    }
}
