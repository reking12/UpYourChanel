using System;

namespace UpYourChannel.Data.Models
{
    public class Message 
    {
        public Message()
        {
            CreatedOn = DateTime.UtcNow;
            IsNew = true;
        }
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsNew { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? PostId { get; set; }
    }
}
