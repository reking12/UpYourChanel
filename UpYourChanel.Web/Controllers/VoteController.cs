using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Vote;

namespace UpYourChannel.Web.Controllers
{
    public class VoteController : Controller
    {
        private readonly IVoteService voteService;
        private readonly UserManager<User> userManager;

        public VoteController(IVoteService voteService, UserManager<User> userManager)
        {
            this.voteService = voteService;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult Hello(VoteInputViewModel input)
        {
            var user = userManager.GetUserAsync(this.User);
            voteService.VoteAsync(user.Id.ToString(), input.PostId, input.IsUpVote);
            var voteResponseModel = new VoteResponseModel()
            {
                VotesCount = voteService.AllVotesForPost(input.PostId)
            };
            return this.Json(voteResponseModel);
            // return this.Json(new { d = "d"});
        }

    }
}
