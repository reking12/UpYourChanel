using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoService videoService;
        private readonly UserManager<User> userManager;

        public VideoController(IVideoService videoService, UserManager<User> userManager)
        {
            this.videoService = videoService;
            this.userManager = userManager;
        }

        public IActionResult AddVideo()
        {
            return this.View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddVideo(VideoInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }
            var userId = userManager.GetUserId(this.User);
            await videoService.AddVideoAsync(input.Link,input.Title,input.Description,userId);
            return Redirect("/Video/AllVideos");
        }

        public IActionResult AllVideos(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return this.View(videoService.AllVideos());
            }
            return this.View(videoService.VideosBySearch(search));
        }
    }
}
