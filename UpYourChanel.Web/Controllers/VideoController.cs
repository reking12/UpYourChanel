using Microsoft.AspNetCore.Mvc;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoService videoService;

        public VideoController(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        public IActionResult AddVideo()
        {
            return this.View();
        }
        
        [HttpPost]
        public IActionResult AddVideo(VideoInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }
            videoService.AddVideo(input);
            return Redirect("/Video/AllVideos");
        }

        public IActionResult AllVideos()
        {
            return this.View(videoService.AllVideos());
        }

        [HttpPost]
        public IActionResult AllVideos(string searchString)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Video/AllVideos");
            }
            return this.View(videoService.VideosBySearch(searchString));
        }
    }
}
