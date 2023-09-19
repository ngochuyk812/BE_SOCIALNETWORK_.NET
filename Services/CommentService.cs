﻿using AutoMapper;
using BE_SOCIALNETWORK.Config;
using BE_SOCIALNETWORK.Database;
using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Extensions;
using BE_SOCIALNETWORK.Payload.Request;
using BE_SOCIALNETWORK.Repositories.Contracts;
using BE_SOCIALNETWORK.Repositories.IRespositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BE_SOCIALNETWORK.Services.Interface
{
    public class CommentService : ICommentService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly PageSettings pageSettings;
        private readonly IMapper mapper;
        public CommentService(IUnitOfWork unitOfWork, IOptions<PageSettings> pageSettings, IMapper mapper)
        {
            this.mapper = mapper;
            this.pageSettings = pageSettings.Value;
            this.unitOfWork = unitOfWork;
        }
        public async Task<PaginatedItems<CommentDto>> ListAsyncPageByIdPost(int pageIndex, int idPost)
        {
            var page = await unitOfWork.CommentRepository.PageAsync(pageIndex, pageSettings.CommentSize, f => f.PostId == idPost, null, i => i.Include(f=>f.User));
            return mapper.Map<PaginatedItems<CommentDto>>(page);
        }
        public async Task<CommentDto> CreateComment(CreateCommentRequest comment, List<MediaDto> medias, int userId)
        {
            Comment commentEntity = new Comment
            {
                PostId = comment.IdPost,
                UserId = userId,
                ParentId = comment.IdParent == -1 ? null : comment.IdParent,
                Content = comment.Content,
                CreatedDate = DateTime.Now,

            };

            List<MediaComment> mediasEntity = new List<MediaComment>();
            foreach (var media in medias)
            {
                mediasEntity.Add(new MediaComment
                {
                    CreatedDate = DateTime.UtcNow,
                    Src = media.Src,
                    Type = media.Type,
                });
            }
            commentEntity.Medias = mediasEntity;
            var entity = await unitOfWork.CommentRepository.AddAsync(commentEntity);
            await unitOfWork.CommitAsync();
            if(entity != null)
            return mapper.Map<CommentDto>(entity);
            return null;
        }
        public async Task<bool> RemoveComment(int idComment)
        {
                var comment = await unitOfWork.CommentRepository.Find(f=>f.Id == idComment);
                if(comment == null)
                {
                return true;
                }
                unitOfWork.CommentRepository.DeleteRange(f=>f.ParentId == idComment);
                unitOfWork.CommentRepository.Delete(idComment);
                unitOfWork.Commit();
                return true;
        }

    }
}