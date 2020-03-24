using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Comment;
using UpYourChannel.Web.ViewModels.Post;

namespace UpYourChannel.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
    }
}
