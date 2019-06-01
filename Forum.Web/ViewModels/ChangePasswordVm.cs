using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.ViewModels
{
    public class ChangePasswordVm
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "You must enter your password in order to log in.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Please enter your password. ")]
        [Required(ErrorMessage = "A password is required. ")]
        [MinLength(6, ErrorMessage = "Your password must be at least {1} characters long. ")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Please enter your password again. ")]
        [Compare(nameof(NewPassword), ErrorMessage = "The passwords do not match. ")]
        public string RepeatNewPassword { get; set; }
    }
}
