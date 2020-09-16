using ApiTeste.Models.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Repository.Interfaces
{
    public interface ITelefoneRepository : IRepository<Telefone>
    {
        public Telefone GetById(int id);
        public Telefone GetTelefoneByDesenvolvedor(string Id_Desenvolvedor);
    }
}
