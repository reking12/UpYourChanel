using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Vote;

namespace UpYourChannel.Web.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VoteApiController : ControllerBase
    {
        private readonly IVoteService votesService;
        private readonly UserManager<User> userManager;

        public VoteApiController(
            IVoteService votesService,
            UserManager<User> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        // POST /api/votes
        // Request body: {"postId":1,"isUpVote":true}
        // Response body: {"votesCount":16}
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> VoteForPost(VoteInputViewModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.votesService.VoteAsync(userId, input.PostId, input.IsUpVote);
            var votes = this.votesService.AllVotesForPost(input.PostId);
            return new VoteResponseModel { VotesCount = votes };
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> VoteForComment(VoteInputViewModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.votesService.VoteForCommentAsync(userId, input.PostId, input.IsUpVote);
            var votes = this.votesService.AllVotesForComment(input.PostId);
            return new VoteResponseModel { VotesCount = votes };
        }
    }
}
