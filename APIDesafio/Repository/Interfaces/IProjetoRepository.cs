using ApiTeste.Models;

namespace ApiTeste.Repository.Interfaces
{
    public interface IProjetoRepository : IRepository<Projeto>
    {
        public Projeto GetById(int id);
    }
}
