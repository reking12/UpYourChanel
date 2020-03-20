using System.Threading.Tasks;
using UpYourChannel.Web.ViewModels.Post;

namespace UpYourChannel.Web.Services
{
     public interface IPostService
    {
        PostViewModel ById(int id);

        Task CreatePost(PostInputViewModel input);

        AllPostsViewModel AllPosts();
    }
}
