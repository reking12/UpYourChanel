using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels.Post;

namespace UpYourChannel.Web.Services
{
     public interface IPostService
    {
        PostIndexModel ById(int id);

        Task CreatePost(PostInputViewModel input);

        Task EditPostAsync(int postId, string newContent, string newTitle);

        AllPostsViewModel AllPosts();

        Task<int> PostsCountAsync();

        Task<PostInputViewModel> ReturnPostByIdAsync(int postId);
    }
}
