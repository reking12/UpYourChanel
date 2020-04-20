using System.ComponentModel.DataAnnotations;

namespace UpYourChannel.Web.ViewModels.Post
{
    public class PostInputViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Category { get; set; }

        public string UserId { get; set; }
    }
}
