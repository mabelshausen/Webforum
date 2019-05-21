using Forum.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.ViewModels
{
    public class CategoriesIndexVm
    {
        public Theme Theme { get; set; }

        public IEnumerable<Category> CategoriesBytheme { get; set; }
    }
}
