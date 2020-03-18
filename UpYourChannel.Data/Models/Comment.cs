using System;

namespace UpYourChannel.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            CreatedOn = DateTime.UtcNow;
        }
        public int Id { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public Category Category { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }

        public Post Post { get; set; }

        public int PostId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}