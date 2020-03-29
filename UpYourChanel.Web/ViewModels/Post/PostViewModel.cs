using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Linq;
using UpYourChannel.Web.ViewModels.Comment;

namespace UpYourChannel.Web.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public int VotesCount { get; set; }

        public int CommentsCount => Comments.Count();

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public bool IsThisUser { get; set; }
    }
}
