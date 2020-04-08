using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
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
        public async Task AddVideoAsync(string link,string title,string description, string userId)
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
                    Id= x.Id,
                    Title = x.Title,
                    Link = x.Link,
                    Description = x.Description
                })
            };
        }

        public AllVideosViewModel VideosBySearch(string searchString)
        {
            string searchStringToLower = searchString.ToLower();
            return new AllVideosViewModel() { AllVideos = AllVideos().AllVideos.Where(x => x.Title.ToLower().Contains(searchStringToLower)) };
        }
    }
}
