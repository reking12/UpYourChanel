using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Web.Services;
using Xunit;

namespace UpYourChannel.Tests.Services
{
    public class RequestedVideoServiceTests
    {
        [Fact]
        public async Task AddRequestedVideo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "AddRequestedVideo_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var requestedVideoService = new RequestedVideoService(dbContext);

            await requestedVideoService.AddRequestedVideoAsync("TE AMO1", "https://www.youtube.com/watch?v=mjrOA8Qe38k", "COVER BY GABBY G1", "u1");
            await requestedVideoService.AddRequestedVideoAsync("TE AMO2", "https://www.youtube.com/watch?v=mjrOA8Qe38k", "COVER BY GABBY G2", "u2");
            await requestedVideoService.AddRequestedVideoAsync("TE AMO3", "https://www.youtube.com/watch?v=mjrOA8Qe38k", "COVER BY GABBY G3", "u3");

            var requestedVideosCount = await dbContext.RequestedVideos.CountAsync();
            var requestedVideo = await dbContext.RequestedVideos.FirstAsync();

            Assert.Equal(1, requestedVideo.Id);
            Assert.Equal("TE AMO1", requestedVideo.Title);
            Assert.Equal("https://www.youtube.com/watch?v=mjrOA8Qe38k", requestedVideo.Link);
            Assert.Equal("COVER BY GABBY G1", requestedVideo.Description);
            Assert.Equal("u1", requestedVideo.UserId);
            Assert.Equal(3, requestedVideosCount);
        }

        [Fact]
        public async Task RemoveRequestedVideo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "RemoveRequestedVideo_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var requestedVideoService = new RequestedVideoService(dbContext);

            await requestedVideoService.AddRequestedVideoAsync("TE AMO1", "https://www.youtube.com/watch?v=mjrOA8Qe38k", "COVER BY GABBY G1", "u1");
            await requestedVideoService.AddRequestedVideoAsync("TE AMO2", "https://www.youtube.com/watch?v=mjrOA8Qe38k", "COVER BY GABBY G2", "u2");
            await requestedVideoService.AddRequestedVideoAsync("TE AMO3", "https://www.youtube.com/watch?v=mjrOA8Qe38k", "COVER BY GABBY G3", "u3");
            await requestedVideoService.RemoveRequestedVideoAsync(2);
            await requestedVideoService.RemoveRequestedVideoAsync(3);

            var requestedVideosCount = await dbContext.RequestedVideos.CountAsync();
            var requestedVideo = await dbContext.RequestedVideos.FirstAsync();

            Assert.Equal(1, requestedVideo.Id);
            Assert.Equal("TE AMO1", requestedVideo.Title);
            Assert.Equal("https://www.youtube.com/watch?v=mjrOA8Qe38k", requestedVideo.Link);
            Assert.Equal("COVER BY GABBY G1", requestedVideo.Description);
            Assert.Equal("u1", requestedVideo.UserId);
            Assert.Equal(1, requestedVideosCount);
        }

        [Fact]
        public async Task AllRequestedVideos()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "AllRequestedVideos_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var requestedVideoService = new RequestedVideoService(dbContext);

            await requestedVideoService.AddRequestedVideoAsync("TE AMO1", "https://www.youtube.com/watch?v=mjrOA8Qe38k", "COVER BY GABBY G1", "u1");
            await requestedVideoService.AddRequestedVideoAsync("TE AMO2", "https://www.youtube.com/watch?v=mjrOA8Qe38k", "COVER BY GABBY G2", "u2");
            await requestedVideoService.AddRequestedVideoAsync("TE AMO3", "https://www.youtube.com/watch?v=mjrOA8Qe38k", "COVER BY GABBY G3", "u3");
            await requestedVideoService.AddRequestedVideoAsync("TE AMO4", "https://www.youtube.com/watch?v=mjrOA8Qe38k", "COVER BY GABBY G4", "u4");
            await requestedVideoService.RemoveRequestedVideoAsync(4);

            var allRequestedVideos = requestedVideoService.AllRequestedVideos();
            var requestedVideo = allRequestedVideos.AllVideos.FirstOrDefault();
            var allRequestedVideoCount = allRequestedVideos.AllVideos.Count();

            Assert.Equal(1, requestedVideo.Id);
            Assert.Equal("TE AMO1", requestedVideo.Title);
            Assert.Equal("https://www.youtube.com/watch?v=mjrOA8Qe38k", requestedVideo.Link);
            Assert.Equal("COVER BY GABBY G1", requestedVideo.Description);
            Assert.Equal("u1", requestedVideo.UserId);
            Assert.Equal(3, allRequestedVideoCount);
        }
    }
}
