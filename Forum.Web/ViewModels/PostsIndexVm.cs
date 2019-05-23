using Forum.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.ViewModels
{
    public class PostsIndexVm
    {
        public Theme Theme { get; set; }

        public Category Category { get; set; }

        public IEnumerable<Post> PostsByCategory { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
