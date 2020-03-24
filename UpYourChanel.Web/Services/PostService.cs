using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels.Post;

namespace UpYourChannel.Web.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public PostService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public PostIndexModel ById(int id)
        {
            var postFromDb = db.Posts.Include(x => x.User)
                .Include(x => x.Comments).FirstOrDefault(x => x.Id == id);
            var post = new PostIndexModel()
            {
                Post = mapper.Map<PostViewModel>(postFromDb)
            };
            return post;
        }

        public async Task CreatePost(PostInputViewModel input)
        {
            var post = mapper.Map<Post>(input);
            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();
        }

        //TODO: make it async
        public AllPostsViewModel AllPosts()
        {
            var posts = new AllPostsViewModel()
            {
                Posts = mapper.Map<IEnumerable<PostViewModel>>(db.Posts)
            };
            return posts;
        }
    }
}
