using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChannel.Web.Services
{
    public interface IVoteService
    {
        Task VoteAsync(string userId, int postId, bool isUpVote);

        Task VoteForCommentAsync(string userId, int commentId, bool isUpVote);

        int AllVotesForPost(int postId);

        int AllVotesForComment(int commentId);
    }
}
