using AutoMapper;
//using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Paging;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Message;

namespace UpYourChannel.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class UserController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IMessageService messageService;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext db;

        public UserController(IMapper mapper, ICloudinaryService cloudinaryService, IMessageService messageService, UserManager<User> userManager, ApplicationDbContext db)
        {
            this.mapper = mapper;
            this.cloudinaryService = cloudinaryService;
            this.messageService = messageService;
            this.userManager = userManager;
            this.db = db;
        }
        [HttpPost]
        public async Task<IActionResult> AddPhoto(IFormFile file)
        {
            // think a little how can make this method better
            if (file.FileName == null)
            {
                return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
            }
            var user = await userManager.GetUserAsync(User);
            var imageParameters = await cloudinaryService.UploadProfilePictureAsync(file.Name, file.OpenReadStream(), user.ProfilePicturePublicId);
            user.ProfilePictureUrl = imageParameters.ProfilePictureUrl;
            user.ProfilePicturePublicId = imageParameters.ProfilePicturePublicId;
            await db.SaveChangesAsync();
            return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
        }

        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            var userId = userManager.GetUserId(User);
            if (await messageService.RemoveMessageFromUserAsync(messageId, userId) == false)
            {
                return NotFound();
            }
            return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
        }

        public IActionResult AllMessages(int? pageNumber)
        {
            var userId = userManager.GetUserId(User);
            var messages = mapper.Map<IEnumerable<MessageViewModel>>(messageService.AllMessagesForUser(userId).OrderByDescending(x => x.CreatedOn));
            var pagination = PaginatedList<MessageViewModel>.Create(messages, pageNumber ?? 1, GlobalConstants.PageSize);
           // return View("/Areas/Identity/Pages/Account/Manage/AllMessages.cshtml", pagination);
            return RedirectToPage("/Account/Manage/AllMessages", pagination);
        }
    }
}
