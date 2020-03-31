using System;
using System.Collections.Generic;

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

        public Category Category { get; set; }

        public virtual User User { get; set; }

        public string UserId { get; set; }

        public virtual Post Post { get; set; }

        public int PostId { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual IEnumerable<Vote> Votes { get; set; }
    }
}