﻿using System;
using System.Collections.Generic;
using UpYourChannel.Web.ViewModels;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.Services
{
    public interface IVideoService 
    {
        void AddVideo(VideoInputModel input);

        AllVideosViewModel AllVideos();

        AllVideosViewModel VideosBySearch(string word);
    }
}
