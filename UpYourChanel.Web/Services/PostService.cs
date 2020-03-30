using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels.Comment;
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

        public PostIndexModel ById(int id)
        {
            var postFromDb = db.Posts.Include(x => x.User)
                .Include(x => x.Comments).FirstOrDefault(x => x.Id == id);
            var post = new PostIndexModel()
            {
                Post = mapper.Map<PostViewModel>(postFromDb)
            };
            return post;
        }

        public async Task CreatePost(PostInputViewModel input)
        {
            var post = mapper.Map<Post>(input);
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
        public AllPostsViewModel AllPosts()
        {
            var dbPosts = db.Posts.Include(x => x.User).Include(x => x.Comments);
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Comment, CommentViewModel>();
                cfg.CreateMap<Post, PostViewModel>()
                .ForMember(x => x.Comments, y => y.MapFrom(p => p.Comments));
            });
            var posts = new AllPostsViewModel()
            {
                Posts = dbPosts.ProjectTo<PostViewModel>(configuration).ToList()
            };
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
            return posts;
        }
        public async Task<PostInputViewModel> ReturnPostByIdAsync(int postId)
        {
            return mapper.Map<PostInputViewModel>(await db.Posts.FirstOrDefaultAsync(x => x.Id == postId));
        }

        public async Task<int> PostsCountAsync()
        => await db.Posts.CountAsync();

        
    }
}
