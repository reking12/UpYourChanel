
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UpYourChannel.Web.Areas.Administration.Controllers;
using UpYourChannel.Web.Paging;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Video;
using Xunit;

namespace UpYourChannel.Tests.Controllers
{
    public class VideoControllerTests
    {
        //[Fact]
        //public void Pokazno()
        //{
        //    var mockService = new Mock<IVideoService>();
        //    mockService.Setup(x => x.AllVideos()).Returns(new List<AllVideosViewModel>
        //    {
        //       new AllVideosViewModel { AllVideos="2",Description="DEV",Title="df" },
        //    });
        //}
        [Fact]
        public void AllVideosShouldReturnAllVideos()
        {
            var videoServiceMock = new Mock<IVideoService>();
            videoServiceMock.Setup(x => x.AllVideos()).Returns(new AllVideosViewModel
            {
                AllVideos = new List<VideoViewModel>() {
                    new VideoViewModel
                    {
                        Id=1,
                        Title = "TE AMO1",
                        Link = "https://www.youtube.com/watch?v=mjrOA8Qe38k",
                        Description = "COVER BY GABBY G1",
                        UserId = "u1",
                    },
                    new VideoViewModel
                    {
                        Id = 2,
                        Title = "TE AMO2",
                        Link = "https://www.youtube.com/watch?v=mjrOA8Qe38k",
                        Description = "COVER BY GABBY G2",
                        UserId = "u2",
                    }
                }.AsQueryable()
            });
            var controller = new VideoController(videoServiceMock.Object);

            // act
            // see why dont work with not null search string!!!
            var result = controller.AllVideos(null, 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PaginatedList<VideoViewModel>>(
               viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

    }
}

