using System;
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

        public async Task VoteAsync(string userId, int postId, bool isUpVote)
        {
            var vote = db.Votes.FirstOrDefault(x => x.UserId == userId && x.PostId == postId);

            if (vote != null)
            {
                if (isUpVote && vote.VoteType == VoteType.Down)
                {
                    vote.VoteType = VoteType.Neutral;
                }
                else if (isUpVote && vote.VoteType == VoteType.Neutral)
                {
                    vote.VoteType = VoteType.Up;
                }
                else if (isUpVote == false && vote.VoteType == VoteType.Up)
                {
                    vote.VoteType = VoteType.Neutral;
                }
                else if (isUpVote == false && vote.VoteType == VoteType.Neutral)
                {
                    vote.VoteType = VoteType.Down;
                }
                //vote.VoteType = isUpVote ? VoteType.Up : VoteType.Down;
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
    }
}
