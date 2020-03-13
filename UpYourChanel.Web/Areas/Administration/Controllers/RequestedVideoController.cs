using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public class RequestedVideoController : Controller
    {
        private readonly IRequestedVideoService requestedVideoService;
        private readonly IVideoService videoService;

        public RequestedVideoController(IRequestedVideoService requestedVideoService, IVideoService videoService)
        {
            this.requestedVideoService = requestedVideoService;
            this.videoService = videoService;
        }    


        public async Task<IActionResult> AddVideoAndRemoveItFromRequested(VideoInputModel input)
        {
            videoService.AddVideo(input);
            await requestedVideoService.RemoveRequestedVideoAsync(input.Id);
            return Redirect("/Administration/RequestedVideo/AllRequestedVideos");
        }

        public IActionResult AllRequestedVideos()
        {
            var allRequestedVideos = requestedVideoService.AllRequestedVideos();
            return this.View(allRequestedVideos);
        }
    }
}
