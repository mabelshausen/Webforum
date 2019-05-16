using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Entities
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
    }
}
