using Forum.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.ViewModels
{
    public class HomeIndexVm
    {
        public IEnumerable<Theme> Themes { get; set; }
    }
}
