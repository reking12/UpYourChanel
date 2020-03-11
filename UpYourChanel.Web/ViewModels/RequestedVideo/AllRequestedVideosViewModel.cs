using System.Collections.Generic;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.ViewModels.RequestedVideo
{
    public class AllRequestedVideosViewModel
    {
        public IEnumerable<VideoInputModel> AllVideos { get; set; }
    }
}
