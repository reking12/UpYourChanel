using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Web.Services;
using Xunit;

namespace UpYourChannel.Tests
{
    public class PostServiceTests
    {
        [Fact]
        public async Task CreatePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "Posts_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var postService = new PostService(null, dbContext, null);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "h1");
            await postService.CreatePostAsync("Tweets2", "Hello i am tweet2", "h2");

            var postCount = await dbContext.Posts.CountAsync();
            var post = await dbContext.Posts.FirstAsync();

            Assert.Equal(1, post.Id);
            Assert.Equal("Tweets", post.Title);
            Assert.Equal("Hello i am tweet", post.Content);
            Assert.Equal("h1", post.UserId);
            Assert.Equal(2, postCount);
        }

        [Fact]
        public async Task PostsCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "PostsCount_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var postService = new PostService(null, dbContext, null);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "h1");
            await postService.CreatePostAsync("Tweets2", "Hello i am tweet2", "h2");

            Assert.Equal(2, await postService.PostsCountAsync());
        }

        [Fact]
        public async Task EditPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "EditPost_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var postService = new PostService(null, dbContext, null);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "h1");
            await postService.EditPostAsync(1,"Hello i am new content","Hello i am new title");

            var post = await dbContext.Posts.FirstAsync();

            Assert.Equal(1, post.Id);
            Assert.Equal("Hello i am new content", post.Content);
            Assert.Equal("Hello i am new title", post.Title);
            Assert.Equal("h1", post.UserId);
            Assert.Equal(1, await postService.PostsCountAsync());
        }

        [Fact]
        public async Task ById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "B_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var postService = new PostService(null, dbContext, null);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "h1");
            var post = postService.ById(1);

            Assert.Equal(1, post.Id);
            Assert.Equal("Tweets", post.Title);
            Assert.Equal("Hello i am tweet", post.Content);
            Assert.Equal("h1", post.UserId);
        }
    }
}
