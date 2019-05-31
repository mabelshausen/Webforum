using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Entities
{
    public class LikedPosts
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
    }
}
