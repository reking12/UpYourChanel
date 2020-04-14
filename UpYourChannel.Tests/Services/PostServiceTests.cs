using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Web.Services;
using Xunit;

namespace UpYourChannel.Tests.Services
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
            var postService = new PostService(dbContext, null);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "u1",1);
            await postService.CreatePostAsync("Tweets2", "Hello i am tweet2", "u2",1);

            var postCount = await dbContext.Posts.CountAsync();
            var post = await dbContext.Posts.FirstAsync();

            Assert.Equal(1, post.Id);
            Assert.Equal("Tweets", post.Title);
            Assert.Equal("Hello i am tweet", post.Content);
            Assert.Equal("u1", post.UserId);
            Assert.Equal(2, postCount);
        }

        [Fact]
        public async Task PostsCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "PostsCount_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var postService = new PostService(dbContext, null);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "u1",1);
            await postService.CreatePostAsync("Tweets2", "Hello i am tweet2", "u2",1);
        
            Assert.Equal(2, await postService.PostsCountAsync(null));
        }

        [Fact]
        public async Task EditPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "EditPost_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var postService = new PostService(dbContext, null);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "u1",1);
            await postService.EditPostAsync(1,"Hello i am new content","Hello i am new title","u1");

            var post = await dbContext.Posts.FirstAsync();

            Assert.Equal(1, post.Id);
            Assert.Equal("Hello i am new content", post.Content);
            Assert.Equal("Hello i am new title", post.Title);
            Assert.Equal("u1", post.UserId);
            Assert.Equal(1, await postService.PostsCountAsync(null));
        }

        [Fact]
        public async Task ById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "B_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var postService = new PostService(dbContext, null);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "u1",1);
            var post = postService.ById(1);

            Assert.Equal(1, post.Id);
            Assert.Equal("Tweets", post.Title);
            Assert.Equal("Hello i am tweet", post.Content);
            Assert.Equal("u1", post.UserId);
        }

        [Fact]
        public async Task AllPosts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "AllPosts_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var postService = new PostService(dbContext, null);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "u1",1);
            await postService.CreatePostAsync("Tweets2", "Hello i am tweet2", "u1",1);

            var allPosts = postService.AllPosts(null);
            var firstPost = await allPosts.FirstAsync();

            Assert.Equal("Tweets", firstPost.Title);
            Assert.Equal("Hello i am tweet", firstPost.Content);
            Assert.Equal("u1", firstPost.UserId);
            Assert.Equal(2, await allPosts.CountAsync());
        }
    }
}
