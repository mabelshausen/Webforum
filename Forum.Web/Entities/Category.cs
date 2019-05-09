using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public Theme Theme { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
