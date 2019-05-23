using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Models
{
    public class UserState
    {
        public string Username { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
    }
}
