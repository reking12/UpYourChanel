using AngleSharp.Dom;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Paging;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Post;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IVoteService voteService;
        private readonly ICommentService commentService;
        private readonly UserManager<User> userManager;

        public PostController(IPostService postService,IVoteService voteService,ICommentService commentService,UserManager<User> userManager)
        {
            this.postService = postService;
            this.voteService = voteService;
            this.commentService = commentService;
            this.userManager = userManager;
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
            input.UserId = userManager.GetUserId(this.User);
            await postService.CreatePost(input);
            return Redirect("/Post/AllPosts");
        }

        public async Task<IActionResult> AllPosts(int? pageNumber)
        {
            var allPosts = postService.AllPosts();
            return View(await PaginatedList<PostViewModel>.CreateAsync(allPosts.Posts, pageNumber ?? 1, GlobalConstants.PageSize));
        }

        public IActionResult ById(int id)
        {
            var postViewModel = this.postService.ById(id);
            if (postViewModel == null)
            {
                return this.NotFound();
            }
            postViewModel.Post.VotesCount = voteService.AllVotesForPost(id);
            return this.View(postViewModel);
        }

        public IActionResult AddCommentToPost()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCommentToPost(PostIndexModel input)
        {
            // maybe remove PostId from comment ViewModel
            input.Comment.UserId = userManager.GetUserId(this.User);
            await commentService.AddCommentToPostAsync(input.Comment.PostId, input.Comment.UserId, input.Comment.Content);
            return Redirect($"/Post/ById/{input.Comment.PostId}");
        }

        public IActionResult PostSubjects()
        {
            var postSubect = new PostSubjectViewModel()
            {
                TotalPosts = postService.PostsCount()
            };
            return this.View(postSubect);
        }
    }
}
