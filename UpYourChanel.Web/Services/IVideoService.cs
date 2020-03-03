using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChanel.Web.Models;
using UpYourChanel.Web.ViewModels;
using UpYourChanel.Web.ViewModels.Video;

namespace UpYourChanel.Web.Services
{
    public interface IVideoService 
    {
        void AddVideo(AddVideoInputViewModel input);

        AllVideosViewModel AllVideos();

        AllVideosViewModel VideosBySearch(string word);
    }
}
