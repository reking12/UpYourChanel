using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UpYourChannel.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public class ForumController : Controller
    {
        public IActionResult Forum()
        {
            return View();
        }
    }
}
