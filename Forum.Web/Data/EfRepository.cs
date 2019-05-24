using Forum.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ForumContext _fc;

        public EfRepository(ForumContext context)
        {
            _fc = context;
        }

        public T GetById(Guid id)
        {
            return _fc.Set<T>()
                .Where(e => e.Id == id)
                .First();
        }

        public IQueryable<T> GetAll()
        {
            return _fc.Set<T>();
        }

        public T Add(T entity)
        {
            _fc.Set<T>().Add(entity);
            _fc.SaveChanges();
            return entity;
        }

        public void Update(T entity)
        {
            _fc.Set<T>().Update(entity);
            _fc.SaveChanges();
        }

        public void Delete(T entity)
        {
            _fc.Set<T>().Remove(entity);
            _fc.SaveChanges();
        }

        public void DeleteRange(IQueryable<T> query)
        {
            _fc.Set<T>().RemoveRange(query);
            _fc.SaveChanges();
        }
    }
}
