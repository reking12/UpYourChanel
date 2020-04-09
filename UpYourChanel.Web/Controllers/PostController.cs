﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.Paging;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Comment;
using UpYourChannel.Web.ViewModels.Post;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UpYourChannel.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPostService postService;
        private readonly IVoteService voteService;
        private readonly ICommentService commentService;
        private readonly UserManager<User> userManager;

        public PostController(IMapper mapper, IPostService postService, IVoteService voteService, ICommentService commentService, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.postService = postService;
            this.voteService = voteService;
            this.commentService = commentService;
            this.userManager = userManager;
        }

        public IActionResult CreatePost()
        {
            return this.View();
        }
        public async Task<IActionResult> EditPost(int postId, int pageNumber)
        {
            TempData["postId"] = postId;
            TempData["pageNumber"] = pageNumber;
            var post = await postService.ReturnPostByIdAsync(postId);
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> EditPost(PostInputViewModel input, int postId, int? pageNumber)
        {
            var userId = userManager.GetUserId(this.User);
            if (await postService.EditPostAsync(postId, input.Content, input.Title, userId) == false)
            {
                return NotFound();
            }
            if (pageNumber == null)
            {
                return Redirect("/Post/AllPosts");
            }
            return Redirect($"/Post/AllPosts?pagenumber={pageNumber}");
        }

        public async Task<IActionResult> DeletePost(int postId)
        {
            var userId = userManager.GetUserId(this.User);
            if (await postService.DeletePostAsync(postId, userId) == false)
            {
                return NotFound();
            }
            return Redirect("/Post/AllPosts");
        }
        
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostInputViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }
            // maybe remove UserId From input
            input.UserId = userManager.GetUserId(this.User);
            await postService.CreatePostAsync(input.Title, input.Content, input.UserId);
            return Redirect("/Post/AllPosts");
        }

        public IActionResult AllPosts(int? pageNumber)
        {
            var dbPosts = postService.AllPosts();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Comment, CommentViewModel>();
                cfg.CreateMap<Post, PostViewModel>()
                .ForMember(x => x.Comments, y => y.MapFrom(p => p.Comments));
            });
            var allPosts = new AllPostsViewModel()
            {
                Posts = dbPosts.ProjectTo<PostViewModel>(configuration).ToList()
            };
            var userId = userManager.GetUserId(this.User);
            allPosts.Posts.Where(x => x.UserId == userId).ToList().ForEach(x => x.IsThisUser = true);
            return View(PaginatedList<PostViewModel>.Create(allPosts.Posts, pageNumber ?? 1, GlobalConstants.PageSize));
        }

        public IActionResult ById(int id)
        {
            // maybe make comment from tinyMce like answers 
            var userId = userManager.GetUserId(this.User);
            var postViewModel = new PostIndexModel()
            {
                Post = mapper.Map<PostViewModel>(postService.ById(id))
            };
            if (postViewModel == null)
            {
                return this.NotFound();
            }
            
            postViewModel.Post.Comments.Where(x => x.UserId == userId).ToList().ForEach(x => x.IsThisUser = true);
            postViewModel.Post.VotesCount = voteService.AllVotesForPost(id);
            postViewModel.Top3Comments = mapper.Map<IEnumerable<CommentViewModel>>(commentService.Top3CommentsForPost(id));
            postViewModel.Post.Comments.ToList().ForEach(x => x.VotesCount = voteService.AllVotesForComment(x.Id));
            return this.View(postViewModel);
        }

        public IActionResult AddCommentToPost()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCommentToPost(PostIndexModel input)
        {
            // maybe remove PostId from comment ViewModel UserId
            input.Comment.UserId = userManager.GetUserId(this.User);
            await commentService.CreateCommentAsync(input.Comment.PostId, input.Comment.UserId, input.Comment.Content, input.Comment.ParentId);
            return Redirect($"/Post/ById/{input.Comment.PostId}");
        }

        public async Task<IActionResult> PostSubjects()
        {
            var postSubect = new PostSubjectViewModel()
            {
                TotalPosts = await postService.PostsCountAsync()
            };
            return this.View(postSubect);
        }
    }
}
