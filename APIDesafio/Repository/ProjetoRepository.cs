using ApiTeste.Models;
using ApiTeste.Models.Data;
using ApiTeste.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Repository
{
    public class ProjetoRepository : Repository<Projeto>, IProjetoRepository
    {
        public ProjetoRepository(AppDbContext context) : base(context)
        {
        }

        public Projeto GetById(int id)
        {
            return _context.Projeto.FirstOrDefault(p => p.ProjetoId == id);
        }
    }
}
