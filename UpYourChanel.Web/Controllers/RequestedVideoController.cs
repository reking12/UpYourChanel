using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels;

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
        public async Task<IActionResult> AddRequestedVideo(AddVideoInputViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }
            await requestedVideoService.AddRequestedVideoAsync(input);
            return Redirect("/Video/AllVideos");
        }
    }
}
