using System.Threading.Tasks;

namespace UpYourChannel.Web.Services
{
    public interface ICommentService
    {
        Task AddCommentToPost(int postId, string UserId, string content);
    }
}
