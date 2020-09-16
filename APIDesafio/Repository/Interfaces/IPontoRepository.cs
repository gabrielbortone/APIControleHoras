using ApiTeste.Models;
using System;
using System.Collections.Generic;

namespace ApiTeste.Repository.Interfaces
{
    public interface IPontoRepository : IRepository<Ponto>
    {
        public Ponto GetById(int id);
        public IEnumerable<Ponto> GetPontosByPeriodo(DateTime inicio, DateTime fim);
        public IEnumerable<Ponto> GetPontosByDesenvolvedor(string id_Desenvolvedor);
        public IEnumerable<Ponto> GetPontosByProjeto(int id_Projeto);
    }
}
