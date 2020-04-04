using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels.Post;

namespace UpYourChannel.Web.Services
{
    public class PostService : IPostService
    {
        private readonly ICommentService commentService;
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public PostService(ICommentService commentService, ApplicationDbContext db, IMapper mapper)
        {
            this.commentService = commentService;
            this.db = db;
            this.mapper = mapper;
        }

        public Post ById(int id)
        {
            // then include e da se hodi navutre
            return db.Posts.Include(x => x.User)
                .Include(x => x.Comments).ThenInclude(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        public async Task CreatePostAsync(string title, string content, string userId)
        {
            var post = new Post
            { 
                Title = title,
                Content = content,
                UserId = userId
            };
            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();
        }

        public async Task EditPostAsync(int postId, string newContent, string newTitle)
        {
            var post = await db.Posts.FirstOrDefaultAsync(x => x.Id == postId);
            post.Content = newContent;
            post.Title = newTitle;
            await db.SaveChangesAsync();
        }

        //TODO: make it async
        public IQueryable<Post> AllPosts()
        {
            return db.Posts.Include(x => x.User).Include(x => x.Comments);  
            //----- Old Way
            //var posts = new AllPostsViewModel()
            //{
            //    Posts = dbPosts.Select(x => new PostViewModel
            //    {
            //        Id = x.Id,
            //        Comments = commentService.AllCommentsForPost(x.Id),
            //        CommentsCount = x.CommentsCount,
            //        VotesCount = x.Votes.Count(),
            //        Content = x.Content,
            //        CreatedOn = x.CreatedOn,
            //        Title = x.Title,
            //        UserUserName = x.User.UserName
            //    })
            //};
        }
        public async Task<PostInputViewModel> ReturnPostByIdAsync(int postId)
        {
            return mapper.Map<PostInputViewModel>(await db.Posts.FirstOrDefaultAsync(x => x.Id == postId));
        }

        public async Task<int> PostsCountAsync()
        => await db.Posts.CountAsync();

        
    }
}
