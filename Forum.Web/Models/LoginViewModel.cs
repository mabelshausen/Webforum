using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Please enter your username")]
        [Required(ErrorMessage = "You must enter your username in order to log in. ")]
        public string Username { get; set; }

        [Display(Name = "Please enter your password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "You must enter your password in order to log in. ")]
        public string Password { get; set; }
    }
}
