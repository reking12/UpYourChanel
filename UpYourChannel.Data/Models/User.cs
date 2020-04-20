using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace UpYourChannel.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Videos = new HashSet<Video>();
            Posts = new HashSet<Post>();
            Messages = new HashSet<Message>();
            ProfilePictureUrl = "https://res.cloudinary.com/upyourchannel/image/upload/v1587136691/Facebook-no-profile-picture-icon-620x389_hwyvki.jpg";
        }

        public virtual IEnumerable<Post> Posts { get; set; }

        public virtual IEnumerable<Video> Videos { get; set; }

        public virtual IEnumerable<Message> Messages { get; set; }

        public int PostsCount => Posts.Count();

        public int VideosCount => Videos.Count();

        public string ProfilePictureUrl { get; set; }

        public string ProfilePicturePublicId { get; set; }
    }
}
