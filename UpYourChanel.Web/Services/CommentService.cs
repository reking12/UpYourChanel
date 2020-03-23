using System;
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
        public async Task AddCommentToPost(int postId, string userId, string content)
        {
            var comment = new Comment()
            {
                Content = content,
                PostId = postId,
                UserId = userId
            };
            await db.Comments.AddAsync(comment);
            await db.SaveChangesAsync();
        }
    }
}
