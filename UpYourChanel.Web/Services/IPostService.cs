﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels.Post;

namespace UpYourChannel.Web.Services
{
     public interface IPostService
    {
        Post ById(int id);

        Task CreatePostAsync(string title, string content, string userId);

        Task EditPostAsync(int postId, string newContent, string newTitle);

        IQueryable<Post> AllPosts();

        Task<int> PostsCountAsync();

        Task<PostInputViewModel> ReturnPostByIdAsync(int postId);
    }
}
