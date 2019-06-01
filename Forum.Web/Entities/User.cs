using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public ICollection<LikedPosts> LikedPosts { get; set; }

        public ICollection<LikedComments> LikedComments { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
