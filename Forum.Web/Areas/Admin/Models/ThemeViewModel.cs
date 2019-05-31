using Forum.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Forum.Web.Areas.Admin.Models
{
    public class ThemeViewModel
    {
        public IEnumerable<Theme> Themes { get; set; }

        [Display(Name = "Enter the title of the theme you want to add. ")]
        public string Theme { get; set; }

        [Display(Name = "Enter the description of the object you want to add. ")]
        public string Description { get; set; }

        [Display(Name = "Enter the category you want to add. ")]
        public string Category { get; set; }

        [Display(Name = "Select the theme of the category you want to add. ")]
        public string CategoryTheme { get; set; }

    }
}
