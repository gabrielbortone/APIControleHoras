using ApiTeste.Models.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Repository.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        public Endereco GetEnderecoByDesenvolvedor(string Id_Desenvolvedor);
        public Endereco GetById(int id);
    }
}
