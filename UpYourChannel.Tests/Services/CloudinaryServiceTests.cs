using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Web.Services;
using Xunit;
using Microsoft.Extensions.Configuration;
using AngleSharp.Common;
using System.IO;
using System.Text;

namespace UpYourChannel.Tests.Services
{
    public class CloudinaryServiceTests
    {
        private readonly IConfiguration configuration;

        public CloudinaryServiceTests(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        //[Fact]
        //public async Task UploadProfilePictureShouldReturnCorrectImage()
        //{
        //    //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //    //        .UseInMemoryDatabase(databaseName: "UploadProfilePicture_Database")
        //    //        .Options;
        //    //var dbContext = new ApplicationDbContext(options);
        //    //var cloudinaryService = new CloudinaryService(
        //    //configuration.GetConnectionString("CLOUD_NAME"),
        //    //configuration.GetConnectionString("API_KEY"),
        //    //configuration.GetConnectionString("API_SECRET"));


        //    //using (var test_Stream = new MemoryStream(Encoding.UTF8.GetBytes("whatever")))
        //    //{
        //    //    var result = await cloudinaryService.UploadProfilePictureAsync("new",test_Stream,null);

        //    //    // Assert    
        //    //    Assert.is(result);
        //    //}
        //    //await cloudinaryService.UploadProfilePictureAsync("Tweets", "Hello i am tweet", null);
        //    //Assert.Equal(1, post.Id);
        //}

    }
}
