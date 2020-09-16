using ApiTeste.Models.Data;
using ApiTeste.Models.ObjectTypes;
using ApiTeste.Repository.Interfaces;
using System.Linq;

namespace ApiTeste.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(AppDbContext context) : base(context)
        {

        }

        public Endereco GetById(int id)
        {
            return _context.Enderecos.FirstOrDefault(e => e.EnderecoId == id);
        }

        public Endereco GetEnderecoByDesenvolvedor(string Id_Desenvolvedor)
        {
            return _context.Enderecos.FirstOrDefault(e => e.DesenvolvedorId == Id_Desenvolvedor);
        }
    }
}
