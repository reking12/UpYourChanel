using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Controllers;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Post;
using Xunit;

namespace UpYourChannel.Tests.Controllers
{
    public class PostControllerTest
    {
        //[Fact]
        //public async Task CreatePostShouldCreatePost()
        //{
        //    //Arrange
        //    var post = new PostInputViewModel()
        //    {
        //        Title = "sdf",
        //        Content = "sdf",
        //        UserId = "u1",
        //        Category = 2
        //    };
        //    var user = new User()
        //    {
        //        Id = "u1",
        //        UserName = "Kris",
        //        Email = "kris@abv.com"
        //    };
        //    var mockPostService = new Mock<IPostService>();
        //    var controller = new PostController(null, mockPostService.Object, null, null, null);

        //    //Act
        //    var result = await controller.CreatePost(post) as RedirectResult;

        //    //Assert
        //    Assert.IsType<RedirectResult>(result);
        //}
        //[Fact]
        //public async Task CreatePostShouldWithNullTitle()
        //{
        //    //Arrange
        //    var post = new PostInputViewModel()
        //    {
        //        Title = "sd",
        //        Content = "sdf",
        //        Category = 1,
        //        UserId = "u1"
        //    };

        //    var mockTeamService = new Mock<IPostService>();
        //    var controller = new PostController(null, mockTeamService.Object, null, null, null);

        //    //Act
        //    var result = await controller.CreatePost(post) as ViewResult;

        //    //Assert
        //    Assert.IsType<ViewResult>(result);
        //}

        //[Fact]
        //public void AllVideosShouldReturnAllVideos()
        //{
            //var videoServiceMock = new Mock<IPostService>();
            //videoServiceMock.Setup(x => x.AllPosts()).Returns(new IQueryable<Post>
            //{
            //        new Post
            //        {
            //            Id=1,
            //            Title = "TE AMO1",
            //            Content = "az sum content 1",
            //            UserId = "u1",
            //        },
            //        new Post
            //        {
            //            Id = 2,
            //            Title = "TE AMO2",
            //            Content = "az sum content 2",
            //            UserId = "u2",
            //        }
            //});
            //var controller = new PostController(videoServiceMock.Object);

            //// act
            //// see why dont work with not null search string!!!
            //var result = controller.AllVideos("TE AMO", 1);

            //// Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<PaginatedList<VideoViewModel>>(
            //   viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
     //   }
    }
}
