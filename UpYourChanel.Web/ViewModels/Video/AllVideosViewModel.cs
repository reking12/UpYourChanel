using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChannel.Web.ViewModels.Video
{
    public class AllVideosViewModel
    {
        public IEnumerable<VideoInputModel> AllVideos { get; set; }

        [Required]
        [MinLength(1)]
        public string Search { get; set; }
    }
}
