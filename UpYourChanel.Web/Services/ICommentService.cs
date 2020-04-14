using System.Collections.Generic;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Web.Services
{
    public interface ICommentService
    {
        Task CreateCommentAsync(int postId, string UserId, string content, int? parentId, bool isAnswer);

        IEnumerable<Comment> AllCommentsForPost(int postId);

        IEnumerable<Comment> Top3CommentsForPost(int postId);

        Task<bool> EditCommentAsync(int commentId, string newContent, string userId);

        Task<bool> DeleteCommentByIdAsync(int id, int postId, string userId);
    }
}
