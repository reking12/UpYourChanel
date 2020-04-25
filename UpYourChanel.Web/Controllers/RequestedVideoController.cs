using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.Controllers
{
    public class RequestedVideoController : Controller
    {
        private readonly IRequestedVideoService requestedVideoService;
        private readonly UserManager<User> userManager;

        public RequestedVideoController(IRequestedVideoService requestedVideoService, UserManager<User> userManager)
        {
            this.requestedVideoService = requestedVideoService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult AddRequestedVideo()
        {
            return this.View("~/Views/Video/AddVideo.cshtml");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRequestedVideo(VideoInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View("~/Views/Video/AddVideo.cshtml", input);
            }
            var userId = userManager.GetUserId(User);
            await requestedVideoService.AddRequestedVideoAsync(input.Title, input.Link, input.Description, userId);
            TempData["SuccessOnAddVideo"] = "Thanks for adding your video.";
            return Redirect("/Video/AllVideos");
        }
    }
}
