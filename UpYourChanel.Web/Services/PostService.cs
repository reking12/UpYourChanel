using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels.Post;

namespace UpYourChannel.Web.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public PostService(ApplicationDbContext db, IMapper mapper)
        {
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

        public async Task<bool> EditPostAsync(int postId, string newContent, string newTitle, string userId)
        {
            var post = await db.Posts.FirstOrDefaultAsync(x => x.Id == postId);
            if (post.UserId != userId)
            {
                return false;
            }
            post.Content = newContent;
            post.Title = newTitle;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePostAsync(int postId, string userId)
        {
            var commentService = new CommentService(db);
            var postToRemove = await db.Posts.FirstOrDefaultAsync(x => x.Id == postId);
            if (postToRemove.UserId != userId)
            {
                return false;
            }
            var commentsToRemove = db.Comments.Where(x => x.PostId == postId);
            var votesForCommentsToRemove = db.Votes.Where(x => commentsToRemove.Any(y => y.Id == x.CommentId));
            var votesForPostToRemove = db.Votes.Where(x => x.PostId == postId);
            db.Comments.RemoveRange(commentsToRemove);
            db.Votes.RemoveRange(votesForCommentsToRemove);
            db.Votes.RemoveRange(votesForPostToRemove);
            db.Posts.Remove(postToRemove);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
