using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Web.ViewModels.Comment;

namespace UpYourChannel.Web.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult AddCommentToPost()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCommentToPost(CommentInputModel input)
        {
            return this.View();
        }
    }
}
