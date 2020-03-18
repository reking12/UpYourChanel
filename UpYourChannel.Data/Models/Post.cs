using System;
using System.Collections.Generic;
using System.Linq;

namespace UpYourChannel.Data.Models
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            CreatedOn = DateTime.UtcNow;
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public Category Category { get; set; }

        public int CommentsCount => Comments.Count();

        public DateTime CreatedOn { get; set; }
    }
}
