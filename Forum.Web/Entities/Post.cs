using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Entities
{
    public class Post : BaseEntity
    {
        public User User { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<LikedPosts> LikedPosts { get; set; }
    }
}
