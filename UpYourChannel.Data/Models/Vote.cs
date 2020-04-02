using UpYourChannel.Data.Models.Enums;

namespace UpYourChannel.Data.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int? PostId { get; set; }

        public virtual Post Post { get; set; }

        public int? CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public VoteType VoteType { get; set; }
    }
}
