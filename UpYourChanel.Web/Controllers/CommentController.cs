using Microsoft.AspNetCore.Authorization;
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
        private readonly IPostService postService;
        private readonly IMessageService messageService;
        private readonly UserManager<User> userManager;

        public CommentController(ICommentService commentService, IPostService postService, IMessageService messageService, UserManager<User> userManager)
        {
            this.commentService = commentService;
            this.postService = postService;
            this.messageService = messageService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentInputModel input, bool isAnswer)
        {
            var post = await postService.ByIdAsync(input.PostId);
            var user = await userManager.GetUserAsync(this.User);
            await commentService.CreateCommentAsync(input.PostId, user.Id, input.Content, input.ParentId, isAnswer);
            await messageService.AddMessageToUserAsync($"Your post was commented by {user.UserName}", post.UserId, post.Id);
            if (input.ParentId != 0)
            {
                var comment = await commentService.GetCommentByIdAsync(input.ParentId);
                await messageService.AddMessageToUserAsync($"Your comment was commented by {user.UserName}", comment.UserId, post.Id);
            }
            return Redirect($"/Post/ById/{input.PostId}");
        }

        [Authorize]
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

        [Authorize]
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
