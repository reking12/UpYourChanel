using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Data.Models.Enums;

namespace UpYourChannel.Web.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext db;

        public PostService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Post> ByIdAsync(int id)
        {
            // then include e da se hodi navutre
            return await db.Posts.Include(x => x.User)
                .Include(x => x.Comments).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> CreatePostAsync(string title, string content, string userId, int category)
        {
            var post = new Post
            {
                Title = title,
                Content = content,
                UserId = userId,
                Category = (CategoryType)category
            };
            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();
            return post.Id;
        }
        //TODO: make it async
        public IQueryable<Post> AllPosts(string category, string sortBy)
        {
            IQueryable<Post> posts;
            if (category != null)
            {
                Enum.TryParse(category, out CategoryType categoryType);
                posts = db.Posts.Include(x => x.User).Include(x => x.Comments).Where(x => x.Category == categoryType);
            }
            else
            {
                posts = db.Posts.Include(x => x.User).Include(x => x.Comments);
            }
            if (sortBy == "Recent")
            {
                posts = posts.OrderByDescending(x => x.CreatedOn);
            }
            else if (sortBy == "Popular")
            {
                posts = posts.OrderByDescending(x => x.Votes.Sum(x => (int)x.VoteType));
            }
            return posts;
        }
        public async Task<Post> ReturnPostInputModelByIdAsync(int postId)
        {
            return await db.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<int> PostsCountAsync(int? category)
        => category != null ? await db.Posts.Where(x => x.Category == (CategoryType)category).CountAsync() 
            : await db.Posts.CountAsync();

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
