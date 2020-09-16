using ApiTeste.Models;
using ApiTeste.Models.Data;
using ApiTeste.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ApiTeste.Repository
{
    public class DesenvolvedorRepository : Repository<Desenvolvedor>, IDesenvolvedorRepository 
    {
        public DesenvolvedorRepository(AppDbContext context) : base(context)
        {
        }

        public Desenvolvedor GetDesenvolvedorByCPF(string cpf)
        {
            return _context.Desenvolvedores.FirstOrDefault(d => d.CPF == cpf);
        }

        public Desenvolvedor GetDesenvolvedorById(string id_Desenvolvedor)
        {
            return _context.Desenvolvedores.Include(d=>d.Endereco).Include(d=>d.Telefone).FirstOrDefault(d => d.Id == id_Desenvolvedor);
        }
    }
}
