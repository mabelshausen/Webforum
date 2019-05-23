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
                .Where(e => e.Id == id && !e.IsDeleted)
                .First();
        }

        public IQueryable<T> GetAll()
        {
            return _fc.Set<T>()
                .Where(e => !e.IsDeleted);
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
            entity.IsDeleted = true;
            Update(entity);
        }
    }
}
