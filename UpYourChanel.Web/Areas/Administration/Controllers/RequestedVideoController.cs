using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Paging;
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
        private readonly IMessageService messageService;
        private readonly UserManager<User> userManager;

        public RequestedVideoController(IRequestedVideoService requestedVideoService, IVideoService videoService,IMessageService messageService, UserManager<User> userManager)
        {
            this.requestedVideoService = requestedVideoService;
            this.videoService = videoService;
            this.messageService = messageService;
            this.userManager = userManager;
        }    

        [HttpPost]
        public async Task<IActionResult> AddVideoAndRemoveItFromRequested(VideoViewModel input, string userId)
        {
            await videoService.AddVideoAsync(input.Link,input.Title,input.Description, userId);
            await requestedVideoService.RemoveRequestedVideoAsync(input.Id);
            await messageService.AddMessageToUserAsync("Your video was approved. We appreciate that",userId);
            return Redirect("/Administration/RequestedVideo/AllRequestedVideos");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRequestedVideo(int id)
        {
            await requestedVideoService.RemoveRequestedVideoAsync(id);
            return Redirect("/Administration/RequestedVideo/AllRequestedVideos");
        }

        public IActionResult AllRequestedVideos(int? pageNumber)
        {
            var allRequestedVideos = requestedVideoService.AllRequestedVideos();
            return View(PaginatedList<VideoViewModel>.Create(allRequestedVideos.AllVideos, pageNumber ?? 1, GlobalConstants.PageSize));
        }
    }
}
