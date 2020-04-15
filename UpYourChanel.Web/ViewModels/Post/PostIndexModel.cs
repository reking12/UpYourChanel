using System.Collections.Generic;
using UpYourChannel.Web.ViewModels.Comment;

namespace UpYourChannel.Web.ViewModels.Post
{
    public class PostIndexModel
    {
        public PostViewModel Post { get; set; }

        public CommentInputModel Comment { get; set; }

        public IEnumerable<CommentViewModel> Top3Comments { get; set; }

        public IEnumerable<CommentViewModel> Top3Answers { get; set; }
    }
}
