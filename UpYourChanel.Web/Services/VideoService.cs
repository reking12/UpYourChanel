using Microsoft.EntityFrameworkCore;
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

        public VideoService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddVideoAsync(string link, string title, string description, string userId)
        {
            var video = new Video
            {
                Link = link,
                Title = title,
                Description = description,
                UserId = userId
            };
            await db.Videos.AddAsync(video);
            await db.SaveChangesAsync();
        }

        public async Task RemoveVideoByIdAsync(int id)
        {
            // maybe check if id exists
            var video = db.Videos.FirstOrDefault(x => x.Id == id);
            db.Remove(video);
            await db.SaveChangesAsync();
        }

        public AllVideosViewModel AllVideos()
        {
            // return just all videos fro db
            // map it in controller
            return new AllVideosViewModel()
            {
                AllVideos = db.Videos.Select(x => new VideoViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Link = x.Link,
                    Description = x.Description
                })
            };
        }

        public AllVideosViewModel VideosBySearch(string searchString)
        {
            //IQueryable<Video> query = db.Videos;
            //var words = searchString?.Split(' ').Select(x => x.Trim())
            //    .Where(x => !string.IsNullOrWhiteSpace(x) && x.Length >= 2).ToList();
            //if (words != null)
            //{
            //    foreach (var word in words)
            //    {
            //        query = query.Where(c => EF.Functions.FreeText(c.Title, word));
            //    }
            //}
            //return new AllVideosViewModel()
            //{
            //    AllVideos = query.Select(x => new VideoViewModel()
            //    {
            //        Id = x.Id,
            //        Title = x.Title,
            //        Link = x.Link,
            //        Description = x.Description
            //    })
            //};
            string searchStringToLower = searchString.ToLower();
            return new AllVideosViewModel() { AllVideos = AllVideos().AllVideos.Where(x => x.Title.ToLower().Contains(searchStringToLower)) };
        }

        public async Task EditVideoTitleAsync(int videoId, string newTitle)
        {
            var video = await db.Videos.FirstOrDefaultAsync(x => x.Id == videoId);
            video.Title = newTitle;
            await db.SaveChangesAsync();
        }
    }
}
