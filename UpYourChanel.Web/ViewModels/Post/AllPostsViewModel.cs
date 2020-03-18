using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChannel.Web.ViewModels.Post
{
    public class AllPostsViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
    }
}
