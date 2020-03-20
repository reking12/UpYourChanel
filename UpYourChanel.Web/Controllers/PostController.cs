using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Post;

namespace UpYourChannel.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IVoteService voteService;

        public PostController(IPostService postService,IVoteService voteService)
        {
            this.postService = postService;
            this.voteService = voteService;
        }

        public IActionResult CreatePost()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostInputViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }
            await postService.CreatePost(input);
            return Redirect("/Video/AllVideos");
        }

        public IActionResult AllPosts()
        {
            return this.View(postService.AllPosts());
        }

        public IActionResult PostSubjects()
        {
            return this.View();
        }
        public IActionResult ById(int id)
        {
            var postViewModel = this.postService.ById(id);
            if (postViewModel == null)
            {
                return this.NotFound();
            }
            postViewModel.VotesCount = voteService.AllVotesForPost(id);
            return this.View(postViewModel);
        }
    }
}
