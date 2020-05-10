using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Paging;
using UpYourChannel.Web.Services;
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
        private readonly IEmailSender emailSender;
        private readonly UserManager<User> userManager;

        public RequestedVideoController(IRequestedVideoService requestedVideoService, IVideoService videoService,IMessageService messageService, IEmailSender emailSender, UserManager<User> userManager)
        {
            this.requestedVideoService = requestedVideoService;
            this.videoService = videoService;
            this.messageService = messageService;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }    

        [HttpPost]
        public async Task<IActionResult> AddVideoAndRemoveItFromRequested(VideoViewModel input, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            await videoService.AddVideoAsync(input.Link,input.Title,input.Description, userId);
            await requestedVideoService.RemoveRequestedVideoAsync(input.Id);
            await emailSender.SendEmailAsync(user.Email, "Your Video:" , GlobalConstants.MessageForGoodVideo);
            await messageService.AddMessageToUserAsync(GlobalConstants.MessageForGoodVideo,userId, null);
            return Redirect("/Administration/RequestedVideo/AllRequestedVideos");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRequestedVideo(int id, string message, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            await requestedVideoService.RemoveRequestedVideoAsync(id);
            await emailSender.SendEmailAsync(user.Email, "Your Video:", message);
            await messageService.AddMessageToUserAsync(message, userId, null);
            return Redirect("/Administration/RequestedVideo/AllRequestedVideos");
        }

        public IActionResult AllRequestedVideos(int? pageNumber)
        {
            var allRequestedVideos = requestedVideoService.AllRequestedVideos();
            return View(PaginatedList<VideoViewModel>.Create(allRequestedVideos.AllVideos, pageNumber ?? 1, GlobalConstants.PageSize));
        }
    }
}
