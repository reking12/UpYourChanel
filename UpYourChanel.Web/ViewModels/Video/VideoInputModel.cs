using System.ComponentModel.DataAnnotations;

namespace UpYourChannel.Web.ViewModels.Video
{
    public class VideoInputModel
    {
        [Required]
        public string Title { get; set; }

        [Url]
        [Required]
        public string Link { get; set; }

        [Required]
        public string Description { get; set; }
    }
}