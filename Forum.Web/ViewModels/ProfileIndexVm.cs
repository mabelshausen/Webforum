using Forum.Web.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.ViewModels
{
    public class ProfileIndexVm
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

    }
}
