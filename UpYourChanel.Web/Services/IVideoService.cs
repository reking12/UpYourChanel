using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChanel.Web.Models;

namespace UpYourChanel.Web.Services
{
    public interface IVideoService 
    {
        void AddVideo();

        IEnumerable<Video> AllVideos();


    }
}
