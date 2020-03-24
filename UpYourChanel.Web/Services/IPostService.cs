using System.Threading.Tasks;
using UpYourChannel.Web.ViewModels.Post;

namespace UpYourChannel.Web.Services
{
     public interface IPostService
    {
        PostIndexModel ById(int id);

        Task CreatePost(PostInputViewModel input);

        AllPostsViewModel AllPosts();
    }
}
