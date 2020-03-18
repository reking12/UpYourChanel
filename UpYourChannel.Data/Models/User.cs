using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChannel.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Videos = new HashSet<Video>();
            Posts = new HashSet<Post>();
        }

        public IEnumerable<Post> Posts { get; set; }

        public IEnumerable<Video> Videos { get; set; }

        public int PostsCount => Posts.Count();

        public int VideosCount => Videos.Count();


    }
}
