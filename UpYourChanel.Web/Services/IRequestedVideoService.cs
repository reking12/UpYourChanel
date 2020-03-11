using System.Threading.Tasks;
using UpYourChannel.Web.ViewModels;
using UpYourChannel.Web.ViewModels.RequestedVideo;

namespace UpYourChannel.Web.Services
{
    public interface IRequestedVideoService 
    {
        Task AddRequestedVideoAsync(AddVideoInputViewModel input);

        Task RemoveRequestedVideoAsync(int id);

        AllRequestedVideosViewModel AllRequestedVideos();
    }
}
