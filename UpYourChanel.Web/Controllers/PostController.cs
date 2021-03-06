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
using Microsoft.AspNetCore.Authorization;
using System;
using UpYourChannel.Data.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace UpYourChannel.Web.Controllers
{

    public class PostController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPostService postService;
        private readonly IVoteService voteService;
        private readonly ICommentService commentService;
        private readonly IMessageService messageService;
        private readonly UserManager<User> userManager;

        public PostController(IMapper mapper, IPostService postService, IVoteService voteService, ICommentService commentService,IMessageService messageService, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.postService = postService;
            this.voteService = voteService;
            this.commentService = commentService;
            this.messageService = messageService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult CreatePost()
        {
            return this.View();
        }
        public async Task<int> OnGetAsync()
        {
            var user = await userManager.Users.Include(x => x.Messages).SingleOrDefaultAsync(x => x.Id == userManager.GetUserId(User));
            return user.Messages.Count();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostInputViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }
            TempData["newPost"] = "Successfully created Post!";
            // maybe remove UserId From input
            input.UserId = userManager.GetUserId(this.User);
            var postId = await postService.CreatePostAsync(input.Title, input.Content, input.UserId, input.Category);
            return Redirect($"/Post/ById/{postId}");
        }

        [Authorize]
        public async Task<IActionResult> EditPost(int postId, int pageNumber)
        {
            TempData["postId"] = postId;
            TempData["pageNumber"] = pageNumber;
            var post = mapper.Map<PostInputViewModel>(await postService.ReturnPostInputModelByIdAsync(postId));
            return View(post);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditPost(PostInputViewModel input, int postId)
        {
            var user = await userManager.GetUserAsync(this.User);
            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (await postService.EditPostAsync(postId, input.Content, input.Title, user.Id, isAdmin) == false)
            {
                return NotFound();
            }
            return Redirect($"/Post/ById/{postId}");
        }

        [Authorize]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var user = await userManager.GetUserAsync(this.User);
            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (await postService.DeletePostAsync(postId, user.Id, isAdmin) == false)
            {
                return NotFound();
            }
            return Redirect("/Post/AllPosts");
        }

        [HttpGet]
        public IActionResult AllPosts(int? pageNumber, string category, string sortBy)
        {
            var dbPosts = postService.AllPosts(category, sortBy);
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
            var pagination = PaginatedList<PostViewModel>.Create(allPosts.Posts, pageNumber ?? 1, GlobalConstants.PageSize);
            pagination.Category = category;
            pagination.SortBy = sortBy;
            return View(pagination);
        }

        public async Task<IActionResult> ById(int id)
        {
            var userId = userManager.GetUserId(this.User);
            var postViewModel = new PostIndexModel()
            {
                Post = mapper.Map<PostViewModel>(await postService.ByIdAsync(id))
            };
            if (postViewModel == null)
            {
                return this.NotFound();
            }
            if (postViewModel.Post.UserId == userId)
            {
                postViewModel.Post.IsThisUser = true;
            }
            postViewModel.Post.Comments.Where(x => x.UserId == userId).ToList().ForEach(x => x.IsThisUser = true);
            postViewModel.Post.VotesCount = voteService.AllVotesForPost(id);
            postViewModel.Top3Comments = mapper.Map<IEnumerable<CommentViewModel>>(commentService.Top3CommentsForPost(id));
            postViewModel.Top3Answers = mapper.Map<IEnumerable<CommentViewModel>>(commentService.Top3AnswersForPost(id));
            postViewModel.Post.Comments.ToList().ForEach(x => x.VotesCount = voteService.AllVotesForComment(x.Id));
            return this.View(postViewModel);
        }

        [Authorize]
        public IActionResult AddCommentToPost()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCommentToPost(PostIndexModel input, bool isAnswer)
        {
            var post = await postService.ByIdAsync(input.Comment.PostId);
            var user = await userManager.GetUserAsync(this.User);
            await commentService.CreateCommentAsync(input.Comment.PostId, user.Id, input.Comment.Content, input.Comment.ParentId, isAnswer);
            await messageService.AddMessageToUserAsync($"Your post was commented by {user.UserName}",post.UserId, post.Id);
            return Redirect($"/Post/ById/{input.Comment.PostId}");
        }

        public async Task<IActionResult> PostSubjects()
        {
            var postSubject = new PostSubjectViewModel()
            {
                TotalPosts = await postService.PostsCountAsync(null),
                PostForCategories = new Dictionary<string, int>()
            };
            foreach (Enum category in Enum.GetValues(typeof(CategoryType)))
            {
                postSubject.PostForCategories[category.ToString()] = await postService.PostsCountAsync(Convert.ToInt32(category));
            }
            return this.View(postSubject);
        }
    }
}
