using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.Controllers
{
    public class RequestedVideoController : Controller
    {
        private readonly IRequestedVideoService requestedVideoService;

        public RequestedVideoController(IRequestedVideoService requestedVideoService)
        {
            this.requestedVideoService = requestedVideoService;
        }

        public IActionResult AddRequestedVideo()
        {
            return this.View("~/Views/Video/AddVideo.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> AddRequestedVideo(VideoInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View("~/Views/Video/AddVideo.cshtml", input);
            }
            await requestedVideoService.AddRequestedVideoAsync(input.Title, input.Link, input.Description);
            TempData["SuccessOnAddVideo"] = "Thanks for adding your video.";
            return Redirect("/Video/AllVideos");
        }
    }
}
