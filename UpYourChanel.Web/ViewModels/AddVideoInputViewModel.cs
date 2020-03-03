using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChanel.Web.ViewModels
{
    public class AddVideoInputViewModel
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        [Url(ErrorMessage = "Invalid Url")]
        public string Link { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
