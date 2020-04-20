using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels.Post;

namespace UpYourChannel.Web.Services
{
     public interface IPostService
    {
        Task<Post> ByIdAsync(int id);

        Task<int> CreatePostAsync(string title, string content, string userId, int category);

        Task<bool> EditPostAsync(int postId, string newContent, string newTitle, string userId);

        Task<bool> DeletePostAsync(int postId, string userId);

        IQueryable<Post> AllPosts(string category, string sortBy);

        Task<int> PostsCountAsync(int? category);

        Task<Post> ReturnPostInputModelByIdAsync(int postId);
    }
}
