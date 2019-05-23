using Forum.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.ViewModels
{
    public class CommentsIndexVm
    {
        public Theme Theme { get; set; }

        public Category Category { get; set; }

        public Post Post { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public string Content { get; set; }
    }
}
