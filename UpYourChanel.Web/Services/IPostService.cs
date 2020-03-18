using System.Threading.Tasks;
using UpYourChannel.Web.ViewModels.Post;

namespace UpYourChannel.Web.Services
{
     public interface IPostService
    {
        Task CreatePost(PostInputViewModel input);

        AllPostsViewModel AllPosts();
    }
}
