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
            var postService = new PostService(dbContext);

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
            var postService = new PostService(dbContext);

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
            var postService = new PostService(dbContext);

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
            var postService = new PostService(dbContext);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "u1",1);
            var post = await postService.ByIdAsync(1);

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
            var postService = new PostService(dbContext);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "u1",1);
            await postService.CreatePostAsync("Tweets2", "Hello i am tweet2", "u1",1);

            var allPosts = postService.AllPosts(null,null);
            var firstPost = await allPosts.FirstAsync();

            Assert.Equal("Tweets", firstPost.Title);
            Assert.Equal("Hello i am tweet", firstPost.Content);
            Assert.Equal("u1", firstPost.UserId);
            Assert.Equal(2, await allPosts.CountAsync());
        }

        [Fact]
        public async Task DeletePostShouldDeletePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "DeletePost_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var postService = new PostService(dbContext);
            var voteService = new VoteService(dbContext);

            await postService.CreatePostAsync("newPost1", "Hello i am tweet", "u1", 1);
            await postService.CreatePostAsync("newPost2", "Hello i am tweet2", "u2", 1);
            await voteService.VoteAsync("u1", 1, true);
            await postService.EditPostAsync(1, "Hello i am new tweet2 new","new title", "u1");
            await postService.DeletePostAsync(1, "u1");

            var postsCount = await dbContext.Posts.CountAsync();
            var post = await dbContext.Posts.FirstOrDefaultAsync();

            Assert.Equal(2, post.Id);
            Assert.Equal("u2", post.UserId);
            Assert.Equal("Hello i am tweet2", post.Content);
            Assert.Equal("newPost2",post.Title);
            Assert.Equal(1, postsCount);
            Assert.Equal(0, await dbContext.Votes.CountAsync());
        }

        [Fact]
        public async Task ReturnPostInputModelById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "ReturnPostInputModelById_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var postService = new PostService(dbContext);

            await postService.CreatePostAsync("newPost1", "Hello i am tweet", "u1", 1);
            await postService.CreatePostAsync("newPost2", "Hello i am tweet2", "u2", 1);
            await postService.CreatePostAsync("newPost3", "Hello i am tweet3", "u3", 2);
            await postService.EditPostAsync(1, "Hello i am new tweet2 new", "new title", "u1");
            await postService.DeletePostAsync(1, "u1");

            var postsCount = await dbContext.Posts.CountAsync();
            var post = await postService.ReturnPostInputModelByIdAsync(2);

            Assert.Equal(2, post.Id);
            Assert.Equal("u2", post.UserId);
            Assert.Equal("Hello i am tweet2", post.Content);
            Assert.Equal("newPost2", post.Title);
            Assert.Equal(2, postsCount);
        }
    }
}
