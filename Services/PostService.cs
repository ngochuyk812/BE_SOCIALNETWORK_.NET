using AutoMapper;
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
using System.Security.Claims;

namespace BE_SOCIALNETWORK.Services.Interface
{
    public class PostService : IPostService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PageSettings pageSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public PostService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper, IOptions<PageSettings> pageSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.pageSettings = pageSettings.Value;
        }
        public async Task<Post> UploadPost(CreatePostRequest body, List<MediaDto> pathMedia)
        {
            var userId =  _httpContextAccessor.HttpContext.User.FindFirstValue("Id");
            if(userId == null)
            {
                return null; 
            }
            Post post = new Post
            {
                Audience = body.Audience,
                Caption = body.Caption,
                Layout = body.Layout,
                UserId = int.Parse(userId),
            };

            List<MediaPost> mediaPosts = new List<MediaPost>();
            foreach(var media in pathMedia)
            {
                mediaPosts.Add(new MediaPost
                {
                    CreatedDate = DateTime.UtcNow,
                    Src = media.Src,
                    Type = media.Type,
                });
            }
            post.MediaPosts = mediaPosts;
            var entity = await _unitOfWork.PostRepository.AddAsync(post);
            await _unitOfWork.CommitAsync();
            return entity;
        }
        public async Task<PaginatedItems<PostDto>> ListAsyncPage(int pageIndex)
        {
            var data = await _unitOfWork.PostRepository.PageAsync(pageIndex, pageSettings.Size, f => true, null, i=>i.Include(f=>f.MediaPosts));
            var PaginatedDto = mapper.Map<PaginatedItems<PostDto>>(data);
            var dataPage = PaginatedDto.Data;
            foreach (var item in dataPage)
            {
                var comment = await _unitOfWork.CommentRepository.ListAsync(f => f.PostId == item.Id);
                var like = await _unitOfWork.LikeRepository.ListAsync(f => f.PostId == item.Id);
                item.CommentCount = comment.Count();
                item.LikeCount = like.Count();
            }
            PaginatedDto.Data = dataPage;
            return PaginatedDto;
        }
       
        public async Task<PostDto> FindById(int id)
        {
            var item = await _unitOfWork.PostRepository.Find(f=>f.Id == id, i=>i.Include(f=>f.MediaPosts));
            return mapper.Map<PostDto>(item);
        }

    }
}
