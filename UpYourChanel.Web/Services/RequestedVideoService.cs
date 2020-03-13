using AutoMapper;
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
        private readonly IMapper mapper;

        public RequestedVideoService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task AddRequestedVideoAsync(AddVideoInputViewModel input)
        {
            var requestedVideo = mapper.Map<RequestedVideo>(input);
            db.RequestedVideos.Add(requestedVideo);
            await db.SaveChangesAsync();
        }

        public AllRequestedVideosViewModel AllRequestedVideos()
        {
            return new AllRequestedVideosViewModel()
            {
                AllVideos = db.RequestedVideos.Select(x => new VideoInputModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Link = x.Link,
                    Description = x.Description
                })
            };
        }

        public async Task RemoveRequestedVideoAsync(int id)
        {
            var requestedVideoForRemove = db.RequestedVideos.FirstOrDefault(x => x.Id == id);
            db.RequestedVideos.Remove(requestedVideoForRemove);
            await db.SaveChangesAsync();
        }
    }
}
