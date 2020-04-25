using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Services;
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

        [Authorize]
        public IActionResult AddVideo()
        {
            return this.View();
        }
        
        //[HttpPost]
        //public async Task<IActionResult> AddVideo(VideoInputModel input, string userId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return this.View(input);
        //    }
        //    await videoService.AddVideoAsync(input.Link,input.Title,input.Description,userId);
        //    return Redirect("/Video/AllVideos");
        //}

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
