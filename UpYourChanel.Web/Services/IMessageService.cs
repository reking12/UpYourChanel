using System.Collections.Generic;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Web.Services
{
    public interface IMessageService
    {
        Task AddMessageToUserAsync(string content, string userId);

        Task<bool> RemoveMessageFromUserAsync(int messageId, string userId);

        Task MakeAllMessagesOld(string userId);

        public IEnumerable<Message> AllMessagesForUser(string userId);
    }
}
