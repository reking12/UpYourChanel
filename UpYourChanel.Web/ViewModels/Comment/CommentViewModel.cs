using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChannel.Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string UserUserName { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(Content);

        public DateTime CreatedOn { get; set; }

        public int VotesCount { get; set; }
    }
}
