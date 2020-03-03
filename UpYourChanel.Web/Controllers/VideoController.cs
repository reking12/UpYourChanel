using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChanel.Web.Services;
using UpYourChanel.Web.ViewModels;
using UpYourChanel.Web.ViewModels.Video;

namespace UpYourChanel.Web.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoService videoService;

        public VideoController(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        [HttpGet]
        public IActionResult AddVideo()
        {
            return this.View();
        }
        
        [HttpPost]
        public IActionResult AddVideo(AddVideoInputViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }
            videoService.AddVideo(input);
            return Redirect("/Video/AllVideos");
        }
        [HttpGet]
        public IActionResult AllVideos()
        {
            return this.View(videoService.AllVideos());
        }
        [HttpPost]
        public IActionResult AllVideos(AllVideosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Video/AllVideos");
            }
            return this.View(videoService.VideosBySearch(model.Search));
        }
    }
}
