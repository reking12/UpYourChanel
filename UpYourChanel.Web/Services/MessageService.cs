using System;
using System.Collections.Generic;
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

        public Task RemoveMessageFromUserAsync(int messageId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
