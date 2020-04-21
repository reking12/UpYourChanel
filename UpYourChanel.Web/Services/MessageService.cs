using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task AddMessageToUserAsync(string content, string userId)
        {
            var message = new Message
            {
                Content = content,
                UserId = userId
            };
            await db.Messages.AddAsync(message);
            await db.SaveChangesAsync();
        }

        public IEnumerable<Message> AllMessagesForUser(string userId)
        {
            throw new NotImplementedException();
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
