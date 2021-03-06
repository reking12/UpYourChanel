﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Web.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext db;

        public MessageService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddMessageToUserAsync(string content, string userId, int? postId)
        {
            var message = new Message
            {
                Content = content,
                UserId = userId,
                PostId = postId
            };
            await db.Messages.AddAsync(message);
            await db.SaveChangesAsync();
        }

        public IEnumerable<Message> AllMessagesForUser(string userId)
        {
            return db.Messages.Where(x => x.UserId == userId);
        }

        public async Task MakeAllMessagesOld(string userId)
        {
            await db.Messages.Where(x => x.UserId == userId).ForEachAsync(x => x.IsNew = false);
            await db.SaveChangesAsync();
        }

        public async Task<bool> RemoveMessageFromUserAsync(int messageId, string userId)
        {
            var message = await db.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
            if (message.UserId != userId)
            {
                return false;
            }
            db.Messages.Remove(message);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
