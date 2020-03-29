using System.Collections.Generic;
using System.Threading.Tasks;
using UpYourChannel.Web.ViewModels.Comment;

namespace UpYourChannel.Web.Services
{
    public interface ICommentService
    {
        Task AddCommentToPostAsync(int postId, string UserId, string content);

        IEnumerable<CommentViewModel> AllCommentsForPost(int postId);

        IEnumerable<CommentViewModel> Top3CommentsForPost(int postId);
    }
}
