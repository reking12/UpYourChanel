using System.Collections.Generic;

namespace UpYourChannel.Web.ViewModels.Post
{
    public class PostSubjectViewModel
    {
        public int TotalPosts { get; set; }

        // maybe make categories like table
        public Dictionary<string,int> PostForCategories { get; set; }
    }
}
