using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Web.Services;
using Xunit;

namespace UpYourChannel.Tests
{
    public class CommentServiceTests
    {
        [Fact]
        public async Task CreateComment()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "CreateComment_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var commentService = new CommentService(dbContext, null);

            await commentService.CreateCommentAsync(1,"Tweets", "Hello i am tweet",null);
            await commentService.CreateCommentAsync(1,"Tweets2", "Hello i am tweet2", 1);

            var comentsCount = await dbContext.Comments.CountAsync();
            var comment = await dbContext.Comments.FirstAsync();

            Assert.Equal(1, comment.Id);
            Assert.Equal("Tweets", comment.UserId);
            Assert.Equal("Hello i am tweet", comment.Content);
            Assert.Null(comment.ParentId);
            Assert.Equal(2, comentsCount);
        }

        [Fact]
        public async Task EditComment()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "EditComment_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var commentService = new CommentService(dbContext, null);

            await commentService.CreateCommentAsync(1, "Tweets", "Hello i am tweet", null);
            await commentService.CreateCommentAsync(1, "Tweets2", "Hello i am tweet2", 1);
            await commentService.EditCommentAsync(2, "Hell i am new tweet");

            var comentsCount = await dbContext.Comments.CountAsync();
            var comment = await dbContext.Comments.LastOrDefaultAsync();

            Assert.Equal(2, comment.Id);
            Assert.Equal("Tweets2", comment.UserId);
            Assert.Equal("Hell i am new tweet", comment.Content);
            Assert.Equal(1,comment.ParentId);
            Assert.Equal(2, comentsCount);
        }
    }
}
