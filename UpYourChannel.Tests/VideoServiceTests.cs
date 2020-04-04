using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Video;
using Xunit;

namespace UpYourChannel.Tests
{
    public class VideoServiceTests
    {
        [Fact]
        public async Task AddVideoShouldAddVideo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "Videos_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var videoService = new VideoService(dbContext);

            await videoService.AddVideoAsync("https://www.youtube.com/watch?v=mjrOA8Qe38k", "TE AMO1", "COVER BY GABBY G1", "asdf1");
            await videoService.AddVideoAsync("https://www.youtube.com/watch?v=mjrOA8Qe38k", "TE AMO2", "COVER BY GABBY G2", "asdf2");

            var videoCount = await dbContext.Videos.CountAsync();
            var video = await dbContext.Videos.FirstAsync();

            Assert.Equal("https://www.youtube.com/watch?v=mjrOA8Qe38k", video.Link);
            Assert.Equal("TE AMO1", video.Title);
            Assert.Equal("COVER BY GABBY G1", video.Description);
            Assert.Equal("asdf1", video.UserId);
            Assert.Equal(2, videoCount);
        }

        [Fact]
        public async Task RemoveVideoById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "VideoRemoveById_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var videoService = new VideoService(dbContext);

            await videoService.AddVideoAsync("https://www.youtube.com/watch?v=mjrOA8Qe38k", "TE AMO1", "COVER BY GABBY G1", "asdf1");
            await videoService.AddVideoAsync("https://www.youtube.com/watch?v=mjrOA8Qe38k", "TE AMO2", "COVER BY GABBY G2", "asdf2");
            await videoService.AddVideoAsync("https://www.youtube.com/watch?v=mjrOA8Qe38k", "TE AMO3", "COVER BY GABBY G3", "asdf3");
            await videoService.RemoveVideoByIdAsync(1);

            var videoCount = await dbContext.Videos.CountAsync();

            Assert.Equal(2, videoCount);
        }

        [Fact]
        public async Task AllVideos()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "AllVideos_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var videoService = new VideoService(dbContext);

            await videoService.AddVideoAsync("https://www.youtube.com/watch?v=mjrOA8Qe38k", "TE AMO1", "COVER BY GABBY G1", "asdf1");
            await videoService.AddVideoAsync("https://www.youtube.com/watch?v=mjrOA8Qe38k", "TE AMO2", "COVER BY GABBY G2", "asdf2");

            var videoCount = await videoService.AllVideos().AllVideos.CountAsync();
            var video = videoService.AllVideos().AllVideos.ToList().First();

            Assert.Equal("https://www.youtube.com/watch?v=mjrOA8Qe38k", video.Link);
            Assert.Equal("TE AMO1", video.Title);
            Assert.Equal("COVER BY GABBY G1", video.Description);
            Assert.Equal(2, videoCount);

        }

        [Fact]
        public async Task AllVideosBySearch()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "AllVideosBySearch_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var videoService = new VideoService(dbContext);

            await videoService.AddVideoAsync("https://www.youtube.com/watch?v=mjrOA8Qe38k", "TE AMO1", "COVER BY GABBY G1", "asdf1");
            await videoService.AddVideoAsync("https://www.youtube.com/watch?v=mjrOA8Qe38k", "TE AMO2", "COVER BY GABBY G2", "asdf2");

            var videoCount = await videoService.VideosBySearch("TE AMO1").AllVideos.CountAsync();
            var video = videoService.VideosBySearch("TE AMO1").AllVideos.ToList().First();

            Assert.Equal("https://www.youtube.com/watch?v=mjrOA8Qe38k", video.Link);
            Assert.Equal("TE AMO1", video.Title);
            Assert.Equal("COVER BY GABBY G1", video.Description);
            Assert.Equal(1, videoCount);

        }
    }
}
