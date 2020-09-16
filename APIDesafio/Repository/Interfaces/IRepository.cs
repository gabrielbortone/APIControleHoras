using System;
using System.Linq;
using System.Linq.Expressions;

namespace ApiTeste.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
