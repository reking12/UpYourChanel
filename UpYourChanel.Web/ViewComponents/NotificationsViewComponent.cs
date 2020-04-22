using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Web.ViewComponents
{
    public class NotificationsViewComponent : ViewComponent
    {
        private readonly UserManager<User> userManager;

        public NotificationsViewComponent(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.Users.Include(x => x.Messages).SingleOrDefaultAsync(x => x.Id == userManager.GetUserId(HttpContext.User));
            var messagesCount = user.Messages.Where(x => x.IsNew == true).Count();
            return View(messagesCount);
        }
    }
}