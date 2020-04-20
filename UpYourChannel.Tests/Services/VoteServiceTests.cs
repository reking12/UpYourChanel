using Microsoft.EntityFrameworkCore;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Data.Models.Enums;
using UpYourChannel.Web.Services;
using Xunit;

namespace UpYourChannel.Tests.Services
{
    public class VoteServiceTests
    {
        [Fact]
        public async void AllVotesForPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "AllVotesForPost_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var voteService = new VoteService(dbContext);
            var postService = new PostService(dbContext);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "u1",1);
            await voteService.VoteAsync("u1", 1, true);
            await voteService.VoteAsync("u2", 1, true);
            for (int i = 0; i < 10; i++)
            {
                await voteService.VoteAsync("u3", 1, false);
            }
            for (int i = 0; i < 10; i++)
            {
                await voteService.VoteAsync("u3", 1, true);
            }

            var votesSum = voteService.AllVotesForPost(1);
            var vote = await dbContext.Votes.FirstAsync();
            var votesCount = await dbContext.Votes.CountAsync();

            Assert.Equal(1, vote.Id);
            Assert.Equal(VoteType.Up, vote.VoteType);
            Assert.Equal(1, vote.PostId);
            Assert.Equal("u1", vote.UserId);
            Assert.Equal(3, votesSum);
            Assert.Equal(3, votesCount);
        }
        [Fact]
        public async void AllVotesForComment()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "AllVotesForComment_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var voteService = new VoteService(dbContext);
            var postService = new PostService(dbContext);
            var commentService = new CommentService(dbContext);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "u1",1);
            await commentService.CreateCommentAsync(1,"u1","TweetsComment",null,false);
            await voteService.VoteForCommentAsync("u1", 1, true);
            await voteService.VoteForCommentAsync("u2", 1, true);
            for (int i = 0; i < 10; i++)
            {
                await voteService.VoteForCommentAsync("u3", 1, false);
            }
            for (int i = 0; i < 10; i++)
            {
                await voteService.VoteForCommentAsync("u3", 1, true);
            }

            var votesSum = voteService.AllVotesForComment(1);
            var vote = await dbContext.Votes.FirstAsync();
            var votesCount = await dbContext.Votes.CountAsync();

            Assert.Equal(1, vote.Id);
            Assert.Equal(VoteType.Up, vote.VoteType);
            Assert.Equal(1, vote.CommentId);
            Assert.Equal("u1", vote.UserId);
            Assert.Equal(3, votesSum);
            Assert.Equal(3, votesCount);
        }

        [Fact]
        public async void VoteAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "VoteAsync_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var voteService = new VoteService(dbContext);
            var postService = new PostService(dbContext);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "u1",1);
            await voteService.VoteAsync("u1", 1, true);
            await voteService.VoteAsync("u1", 1, false);
            for (int i = 0; i < 10; i++)
            {
                await voteService.VoteAsync("u1", 1, false);
            }
            for (int i = 0; i < 10; i++)
            {
                await voteService.VoteAsync("u1", 1, true);
            }
            await voteService.VoteAsync("u1", 1, false);

            var votesSum = voteService.AllVotesForPost(1);
            var vote = await dbContext.Votes.FirstAsync();
            var votesCount = await dbContext.Votes.CountAsync();

            Assert.Equal(1, vote.Id);
            Assert.Equal(VoteType.Neutral, vote.VoteType);
            Assert.Equal(1, vote.PostId);
            Assert.Equal("u1", vote.UserId);
            Assert.Equal(0, votesSum);
            Assert.Equal(1, votesCount);
        }

        [Fact]
        public async void VoteForCommentAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "VoteForCommentAsync_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var voteService = new VoteService(dbContext);
            var postService = new PostService(dbContext);
            var commentService = new CommentService(dbContext);

            await postService.CreatePostAsync("Tweets", "Hello i am tweet", "u1",1);
            await commentService.CreateCommentAsync(1, "u1", "TweetsComment", null,false);
            await voteService.VoteForCommentAsync("u1", 1, true);
            await voteService.VoteForCommentAsync("u1", 1, false);
            for (int i = 0; i < 10; i++)
            {
                await voteService.VoteForCommentAsync("u1", 1, false);
            }
            for (int i = 0; i < 10; i++)
            {
                await voteService.VoteForCommentAsync("u1", 1, true);
            }
            await voteService.VoteForCommentAsync("u1", 1, false);

            var votesSum = voteService.AllVotesForComment(1);
            var vote = await dbContext.Votes.FirstAsync();
            var votesCount = await dbContext.Votes.CountAsync();

            Assert.Equal(1, vote.Id);
            Assert.Equal(VoteType.Neutral, vote.VoteType);
            Assert.Equal(1, vote.CommentId);
            Assert.Equal("u1", vote.UserId);
            Assert.Equal(0, votesSum);
            Assert.Equal(1, votesCount);
        }

        [Fact]
        public void ReturnVoteTypeFromVote()
        { 
            var voteService = new VoteService(null);
            VoteType voteType;
            voteType = voteService.ReturnVoteTypeFromVote(VoteType.Down,false);
            Assert.Equal(VoteType.Down,voteType);
            voteType = voteService.ReturnVoteTypeFromVote(VoteType.Down, true);
            Assert.Equal(VoteType.Neutral, voteType);
            voteType = voteService.ReturnVoteTypeFromVote(VoteType.Neutral, false);
            Assert.Equal(VoteType.Down, voteType);
            voteType = voteService.ReturnVoteTypeFromVote(VoteType.Neutral, true);
            Assert.Equal(VoteType.Up, voteType);
            voteType = voteService.ReturnVoteTypeFromVote(VoteType.Up, false);
            Assert.Equal(VoteType.Neutral, voteType);
            voteType = voteService.ReturnVoteTypeFromVote(VoteType.Up, true);
            Assert.Equal(VoteType.Up, voteType);
        }
    }
}
