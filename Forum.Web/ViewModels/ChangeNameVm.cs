using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.ViewModels
{
    public class ChangeNameVm
    {
        [Display(Name = "Please enter your desired username.")]
        [Required(ErrorMessage = "A username is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Username must be at least {2} characters long and cannot be longer than {1} characters.")]
        [RegularExpression(@"^[\w\d.]{5,}$", ErrorMessage = "Username cannot contain spaces or special characters.")]
        public string Username { get; set; }
    }
}
