using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Comment;

namespace UpYourChannel.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly UserManager<User> userManager;

        public CommentController(ICommentService commentService, UserManager<User> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentInputModel input)
        {
            //remove userId FROM input Model
            var userId = this.userManager.GetUserId(this.User);
            input.UserId = userId;
            await commentService.CreateCommentAsync(input.PostId, input.UserId, input.Content, input.ParentId);
            return Redirect($"/Post/ById/{input.PostId}");
        }

        [HttpPost]
        public async Task<IActionResult> EditComment(int commentId, string newContent, int postId)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (await commentService.EditCommentAsync(commentId, newContent, userId) == false)
            {
                return NotFound();
            }
            return Redirect($"/Post/ById/{postId}");
        }

        public async Task<IActionResult> DeleteComment(int id, int postId)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (await commentService.DeleteCommentByIdAsync(id,postId, userId) == false)
            {
                return NotFound();
            }
            return Redirect($"/Post/ById/{postId}");
        }
    }
}
