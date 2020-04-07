using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Web.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext db;

        public CommentService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task CreateCommentAsync(int postId, string userId, string content,int? parentId)
        {
            var comment = new Comment()
            {
                Content = content,
                PostId = postId,
                UserId = userId,
                ParentId = parentId
            };
            await db.Comments.AddAsync(comment);
            await db.SaveChangesAsync();
        }

        public IEnumerable<Comment> AllCommentsForPost(int postId)
        {
            return db.Comments.Where(x => x.PostId == postId);
        }

        public IEnumerable<Comment> Top3CommentsForPost(int postId)
        {
            return db.Comments.Where(x => x.PostId == postId).OrderByDescending(x => x.Votes.Sum(y => (int)y.VoteType)).Take(3);
        }

        public async Task EditCommentAsync(int commentId, string newContent)
        {
            var comment = await db.Comments.FirstOrDefaultAsync(x => x.Id == commentId);
            comment.Content = newContent;
            await db.SaveChangesAsync();
        }

        public async Task DeleteCommentByIdAsync(int id)
        {
            var comment = await db.Comments.FirstOrDefaultAsync(x => x.Id == id);
            var votes = db.Votes.Where(x => x.CommentId == id);
            await db.Comments.Where(x => x.ParentId == id).ForEachAsync(x => x.ParentId = null);
            db.Comments.Remove(comment);
            db.Votes.RemoveRange(votes);
            await db.SaveChangesAsync();
        }
    }
}
