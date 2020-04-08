using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Data.Models.Enums;

namespace UpYourChannel.Web.Services
{
    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext db;

        public VoteService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int AllVotesForPost(int postId)
        {
            return db.Votes.Where(x => x.PostId == postId).Sum(x => (int)x.VoteType);
        }

        public int AllVotesForComment(int commentId)
        {
            return db.Votes.Where(x => x.CommentId == commentId).Sum(x => (int)x.VoteType);
        }

        public async Task VoteAsync(string userId, int postId, bool isUpVote)
        {
            var vote = db.Votes.FirstOrDefault(x => x.UserId == userId && x.PostId == postId);

            if (vote != null)
            {
                vote.VoteType = ReturnVoteTypeFromVote(vote.VoteType, isUpVote);
            }
            else
            {
                vote = new Vote()
                {
                    PostId = postId,
                    UserId = userId,
                    VoteType = isUpVote ? VoteType.Up : VoteType.Down
                };
                await db.Votes.AddAsync(vote);
            }
            await db.SaveChangesAsync();
        }
        public async Task VoteForCommentAsync(string userId, int commentId, bool isUpVote)
        {
            var vote = db.Votes.FirstOrDefault(x => x.UserId == userId && x.CommentId == commentId);

            if (vote != null)
            {
                vote.VoteType = ReturnVoteTypeFromVote(vote.VoteType, isUpVote);
            }
            else
            {
                vote = new Vote()
                {
                    CommentId = commentId,
                    UserId = userId,
                    VoteType = isUpVote ? VoteType.Up : VoteType.Down
                };
                await db.Votes.AddAsync(vote);
            }
            await db.SaveChangesAsync();
        }
        public VoteType ReturnVoteTypeFromVote(VoteType voteType, bool isUpVote)
        {
            // vote.VoteType = isUpVote ? VoteType.Up : VoteType.Down;
            // don't use this because i have neutral vote
            if (isUpVote && voteType == VoteType.Down)
            {
                voteType = VoteType.Neutral;
            }
            else if (isUpVote && voteType == VoteType.Neutral)
            {
                voteType = VoteType.Up;
            }
            else if (isUpVote == false && voteType == VoteType.Up)
            {
                voteType = VoteType.Neutral;
            }
            else if (isUpVote == false && voteType == VoteType.Neutral)
            {
                voteType = VoteType.Down;
            }

            return voteType;
        }

    }
}
