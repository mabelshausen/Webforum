using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Entities
{
    public class LikedComments
    {
        public Guid UserId { get; set; }
        public Guid CommentId { get; set; }

        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}
