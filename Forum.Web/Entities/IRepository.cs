using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Entities
{
    public interface IRepository<T>
    {
        T GetById(Guid id);

        IQueryable<T> GetAll();

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(IQueryable<T> query);
    }
}
