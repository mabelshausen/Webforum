using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Entities
{
    public class Theme : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
