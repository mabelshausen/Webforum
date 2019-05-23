using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Models
{
    public class TCPState
    {
        public Guid ThemeId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid PostId { get; set; }
    }
}
