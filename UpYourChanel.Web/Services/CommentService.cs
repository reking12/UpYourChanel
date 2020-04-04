﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels.Comment;

namespace UpYourChannel.Web.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public CommentService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task CreateCommentAsync(int postId, string userId, string content,int? parentId)
        {
            var comment = new Comment()
            {
                Content = content,
                PostId = postId,
                UserId = userId,
                ParentId = parentId
            };
            await db.Comments.AddAsync(comment);
            await db.SaveChangesAsync();
        }

        public IEnumerable<CommentViewModel> AllCommentsForPost(int postId)
        {
            return mapper.Map<IEnumerable<CommentViewModel>>(db.Comments.Where(x => x.PostId == postId));
        }

        public IEnumerable<CommentViewModel> Top3CommentsForPost(int postId)
        {
            var comments = db.Comments.Where(x => x.PostId == postId).OrderBy(x => x).Take(3);
            return mapper.Map<IEnumerable<CommentViewModel>>(comments);
        }

        public async Task EditComment(int commentId, string newContent)
        {
            var comment = await db.Comments.FirstOrDefaultAsync(x => x.Id == commentId);
            comment.Content = newContent;
            await db.SaveChangesAsync();
        }
    }
}
