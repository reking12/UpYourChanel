using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels;
using UpYourChannel.Web.ViewModels.RequestedVideo;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.Services
{
    public class RequestedVideoService : IRequestedVideoService
    {
        private readonly ApplicationDbContext db;

        public RequestedVideoService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddRequestedVideoAsync(string title, string link, string description, string userId)
        {
            var requestedVideo = new RequestedVideo()
            {
                Title = title,
                Link = link,
                Description = description,
                UserId = userId
            };
            await db.RequestedVideos.AddAsync(requestedVideo);
            await db.SaveChangesAsync();
        }

        public AllRequestedVideosViewModel AllRequestedVideos()
        {
            return new AllRequestedVideosViewModel()
            {
                AllVideos = db.RequestedVideos.Select(x => new VideoViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Link = x.Link,
                    Description = x.Description,
                    UserId = x.UserId
                })
            };
            // do it in controller
        }

        public async Task RemoveRequestedVideoAsync(int id)
        {
            var requestedVideoForRemove = db.RequestedVideos.FirstOrDefault(x => x.Id == id);
            db.RequestedVideos.Remove(requestedVideoForRemove);
            await db.SaveChangesAsync();
        }
    }
}
