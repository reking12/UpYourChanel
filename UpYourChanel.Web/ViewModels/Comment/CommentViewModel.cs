using Ganss.XSS;
using System;

namespace UpYourChannel.Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(Content);

        public DateTime CreatedOn { get; set; }

        public int VotesCount { get; set; }

        public bool IsThisUser { get; set; }

        public bool IsAnswer { get; set; }
    }
}
