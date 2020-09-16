using ApiTeste.Models;
using System.Collections.Generic;

namespace ApiTeste.Repository.Interfaces
{
    public interface IDesenvolvedorRepository : IRepository<Desenvolvedor>
    {
        public Desenvolvedor GetDesenvolvedorById(string id_Desenvolvedor);
        public Desenvolvedor GetDesenvolvedorByCPF(string cpf);
    }
}
