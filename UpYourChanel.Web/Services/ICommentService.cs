using System.Collections.Generic;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Web.Services
{
    public interface ICommentService
    {
        Task CreateCommentAsync(int postId, string UserId, string content, int? parentId);

        IEnumerable<Comment> AllCommentsForPost(int postId);

        IEnumerable<Comment> Top3CommentsForPost(int postId);

        Task EditCommentAsync(int commentId, string newContent);

        Task DeleteCommentByIdAsync(int id);
    }
}
