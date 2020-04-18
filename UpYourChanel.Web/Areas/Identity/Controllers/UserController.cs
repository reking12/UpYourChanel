using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Services;

namespace UpYourChannel.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class UserController : Controller
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext db;

        public UserController(ICloudinaryService cloudinaryService, UserManager<User> userManager, ApplicationDbContext db)
        {
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
            this.db = db;
        }
        [HttpPost]
        public async Task<IActionResult> AddPhoto(IFormFile file)
        {
            // think a little how can make this method better
            if (file == null)
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
    }
}
