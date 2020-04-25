using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChannel.Web.ViewModels.Comment
{
    public class CommentInputModel
    {
        public int PostId { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

       // public string UserId { get; set; }
    }
}
