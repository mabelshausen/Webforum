using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Entities
{
    public class Comment : BaseEntity
    {
        [Required]
        public Post Post { get; set; }

        public User User { get; set; }

        public string Content { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<LikedComments> LikedComments { get; set; }
    }
}
