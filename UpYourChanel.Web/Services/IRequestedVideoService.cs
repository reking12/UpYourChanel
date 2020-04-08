using System.Threading.Tasks;
using UpYourChannel.Web.ViewModels;
using UpYourChannel.Web.ViewModels.RequestedVideo;

namespace UpYourChannel.Web.Services
{
    public interface IRequestedVideoService 
    {
        Task AddRequestedVideoAsync(string link, string title, string description);

        Task RemoveRequestedVideoAsync(int id);

        AllRequestedVideosViewModel AllRequestedVideos();
    }
}
