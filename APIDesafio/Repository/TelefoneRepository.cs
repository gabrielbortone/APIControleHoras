using ApiTeste.Models.Data;
using ApiTeste.Models.ObjectTypes;
using ApiTeste.Repository.Interfaces;
using System.Linq;

namespace ApiTeste.Repository
{
    public class TelefoneRepository : Repository<Telefone>, ITelefoneRepository
    {
        public TelefoneRepository(AppDbContext context) : base(context)
        {
        }

        public Telefone GetById(int id)
        {
            return _context.Telefones.FirstOrDefault(t => t.TelefoneId == id);
        }

        public Telefone GetTelefoneByDesenvolvedor(string Id_Desenvolvedor)
        {
            return _context.Telefones.FirstOrDefault(t => t.DesenvolvedorId == Id_Desenvolvedor);
        }
    }
}
