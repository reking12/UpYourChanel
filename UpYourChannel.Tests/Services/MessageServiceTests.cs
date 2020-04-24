using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Web.Services;
using Xunit;

namespace UpYourChannel.Tests.Services
{
    public class MessageServiceTests
    {
        [Fact]
        public async Task AddMessageToUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "CreateMessage_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var messageService = new MessageService(dbContext);

            await messageService.AddMessageToUserAsync("Message1", "u1");
            await messageService.AddMessageToUserAsync("Message2", "u2");

            var messagesCount = await dbContext.Messages.CountAsync();
            var message = await dbContext.Messages.FirstAsync();

            Assert.Equal(1, message.Id);
            Assert.Equal("Message1", message.Content);
            Assert.Equal("u1", message.UserId);
            Assert.True(message.IsNew);
            Assert.Equal(2, messagesCount);
        }

        [Fact]
        public async Task MakeAllMessagesOld()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "MakeAllMessagesOld_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var messageService = new MessageService(dbContext);

            await messageService.AddMessageToUserAsync("Message1", "u1");
            await messageService.AddMessageToUserAsync("Message2", "u2");
            await messageService.MakeAllMessagesOld("u1");

            var messagesCount = await dbContext.Messages.CountAsync();
            var message = await dbContext.Messages.FirstAsync();

            Assert.Equal(1, message.Id);
            Assert.Equal("Message1", message.Content);
            Assert.Equal("u1", message.UserId);
            Assert.False(message.IsNew);
            Assert.Equal(2, messagesCount);
        }

        [Fact]
        public async Task RemoveMessageFromUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "RemoveMessageFromUser_Database")
                    .Options;
            var dbContext = new ApplicationDbContext(options);
            var messageService = new MessageService(dbContext);

            await messageService.AddMessageToUserAsync("Message1", "u1");
            await messageService.AddMessageToUserAsync("Message2", "u2");
            await messageService.MakeAllMessagesOld("u1");
            await messageService.RemoveMessageFromUserAsync(1,"u1");

            var messagesCount = await dbContext.Messages.CountAsync();
            var message = await dbContext.Messages.FirstAsync();

            Assert.Equal(2, message.Id);
            Assert.Equal("Message2", message.Content);
            Assert.Equal("u2", message.UserId);
            Assert.True(message.IsNew);
            Assert.Equal(1, messagesCount);
        }
    }
}
