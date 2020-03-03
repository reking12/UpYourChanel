using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChanel.Web.Data;
using UpYourChanel.Web.Models;
using UpYourChanel.Web.ViewModels;
using UpYourChanel.Web.ViewModels.Video;

namespace UpYourChanel.Web.Services
{
    public class VideoService : IVideoService
    {
        private readonly ApplicationDbContext db;

        public VideoService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddVideo(AddVideoInputViewModel input)
        {
            var video = new Video()
            {
                Title = input.Title,
                Link = input.Link,
                Description = input.Description,
            };
            db.Add(video);
            db.SaveChanges();
        }

        public AllVideosViewModel AllVideos()
        {
            return new AllVideosViewModel()
            {
                AllVideos = db.Videos.Select(x => new VideoInputModel()
                {
                    Title = x.Title,
                    Link = x.Link
                })
            };
        }

        public AllVideosViewModel VideosBySearch(string word)
        {
            return new AllVideosViewModel() { AllVideos = AllVideos().AllVideos.Where(x => x.Title.ToLower().Contains(word)) };
        }
    }
}
