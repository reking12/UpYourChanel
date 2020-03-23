using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChannel.Web.ViewModels.Comment
{
    public class CommentInputModel
    {
        public string Content { get; set; }

        public int PostId { get; set; }

        public string UserId { get; set; }


    }
}
