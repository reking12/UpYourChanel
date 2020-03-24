using System.Threading.Tasks;

namespace UpYourChannel.Web.Services
{
    public interface ICommentService
    {
        Task AddCommentToPostAsync(int postId, string UserId, string content);
    }
}
