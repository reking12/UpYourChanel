using System.Threading.Tasks;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.Services
{
    public interface IVideoService 
    {
        Task AddVideoAsync(string link, string title, string description, string userId);

        AllVideosViewModel AllVideos();

        AllVideosViewModel VideosBySearch(string searchString);

        Task RemoveVideoByIdAsync(int id);

        Task EditVideoTitleAsync(int videoId, string newTitle);
    }
}
