using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChannel.Web.Services
{
    public interface IVoteService
    {
        Task VoteAsync(string userId, int postId, bool isUpVote);

        int AllVotesForPost(int postId);
    }
}
