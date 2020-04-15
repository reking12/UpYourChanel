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
        public async Task CreateCommentAsync(int postId, string userId, string content,int? parentId, bool isAnswer)
        {
            var comment = new Comment()
            {
                Content = content,
                PostId = postId,
                UserId = userId,
                ParentId = parentId,
                IsAnswer = isAnswer
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
        public IEnumerable<Comment> Top3AnswersForPost(int postId)
        {
            return db.Comments.Where(x => x.PostId == postId && x.IsAnswer == true).OrderByDescending(x => x.Votes.Sum(y => (int)y.VoteType)).Take(3);
        }

        public async Task<bool> EditCommentAsync(int commentId, string newContent, string userId)
        {
            var comment = await db.Comments.FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment.UserId != userId)
            {
                return false;
            }
            comment.Content = newContent;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCommentByIdAsync(int id, int postId, string userId)
        {
            // make it in better way
            // dont like this 
            // make it in controller
            var comment = await db.Comments.FirstOrDefaultAsync(x => x.Id == id && x.PostId == postId);
            if (comment.UserId != userId)
            {
                return false;
            }
            var votesToRemove = db.Votes.Where(x => x.CommentId == id);
            await db.Comments.Where(x => x.ParentId == id).ForEachAsync(x => x.ParentId = null);
            db.Comments.Remove(comment);
            db.Votes.RemoveRange(votesToRemove);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
