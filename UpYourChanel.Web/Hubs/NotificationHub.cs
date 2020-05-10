using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels.Userr;

namespace UpYourChannel.Web.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly UserManager<User> userManager;

        public NotificationHub(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<UserViewModel> GetNotifications()
        {
            var user = await userManager.Users.Include(x => x.Messages).SingleOrDefaultAsync(x => x.Id == Context.ConnectionId);
            var userViewModel = new UserViewModel
            {
                ProfilePictureUrl = user.ProfilePictureUrl,
                NotificationsCount = user.Messages.Where(x => x.IsNew == true).Count()
            };
            return userViewModel;
        }
    }
}
