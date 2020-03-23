using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.Services
{
    public class VideoService : IVideoService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public VideoService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public void AddVideo(VideoInputModel input)
        {
            // var video = mapper.Map<Video>(input);
            // TODO: ADD USER
            // TODO: MAKE IT WITH AUTOMAPPER AND TO WORK WITH ID

            //Make it ASYNC
            var video = new Video
            {
                Link = input.Link,
                Title = input.Title,
                Description = input.Description
            };
            db.Videos.Add(video);
            db.SaveChanges();
        }

        public async Task RemoveVideoByIdAsync(int id)
        {
            var video = db.Videos.FirstOrDefault(x => x.Id == id);
            db.Remove(video);
            await db.SaveChangesAsync();
        }

        public AllVideosViewModel AllVideos()
        {
            return new AllVideosViewModel()
            {
                AllVideos = db.Videos.Select(x => new VideoInputModel()
                {
                    Id= x.Id,
                    Title = x.Title,
                    Link = x.Link
                })
            };
        }

        public AllVideosViewModel VideosBySearch(string searchString)
        {
            return new AllVideosViewModel() { AllVideos = AllVideos().AllVideos.Where(x => x.Title.ToLower().Contains(searchString)) };
        }
    }
}
