using System.Collections.Generic;
using System.Threading.Tasks;
using UpYourChannel.Web.ViewModels.Comment;

namespace UpYourChannel.Web.Services
{
    public interface ICommentService
    {
        Task CreateCommentAsync(int postId, string UserId, string content, int? parentId);

        IEnumerable<CommentViewModel> AllCommentsForPost(int postId);

        IEnumerable<CommentViewModel> Top3CommentsForPost(int postId);

        Task EditComment(int commentId, string newContent);
    }
}
