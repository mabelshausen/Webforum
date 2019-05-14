using Forum.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.ViewModels
{
    public class CategoriesIndexVm
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
