using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Entities
{
    public class Post
    {
        public Guid Id { get; set; }

        public User User { get; set; }

        public Category Category { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateTime { get; set; }
    }
}
