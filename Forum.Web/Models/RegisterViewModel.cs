using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Please enter your desired username. ")]
        [Required(ErrorMessage = "A username is required. ")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Username must be at least {2} characters long and cannot be longer than {1} characters. ")]
        [RegularExpression(@"^[\w\d.]{5,}$", ErrorMessage = "Username cannot contain spaces or special characters. ")]
        public string Username { get; set; }

        [Display(Name = "Please enter your password. ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A password is required. ")]
        [MinLength(6, ErrorMessage = "Your password must be at least {2} characters long. ")]
        public string Password { get; set; }

        [Display(Name = "Please enter your password again. ")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The passwords do not match. ")]
        public string RepeatPassword { get; set; }

        [Display(Name = "I agree with the terms of service. ")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree with out terms of service in order to create an account. ")]
        public bool TermsOfService { get; set; }
    }
}
