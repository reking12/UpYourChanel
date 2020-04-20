using System.Collections.Generic;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Web.Services
{
    public interface IMessageService
    {
        IEnumerable<Message> AllMessagesForUser(string userId);

        Task AddMessageToUserAsync(string content, string userId);

        Task RemoveMessageFromUserAsync(int messageId, string userId);
    }
}
