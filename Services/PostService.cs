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
using parking_center.Extensions;
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
        public async Task<PostDto> UploadPost(CreatePostRequest body, List<MediaDto> pathMedia)
        {
            var userId = _httpContextAccessor.HttpContext.GetUser();
            if(userId == null)
            {
                return null; 
            }
            Post post = new Post
            {
                Audience = body.Audience,
                Caption = body.Caption,
                Layout = body.Layout,
                UserId = userId.Id,
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
            var rs = await FindById(entity.Id);
            return rs;
        }
        public async Task<PaginatedItems<CustomPostHomeDto>> ListAsyncPage(int pageIndex)
        {
            var userId = _httpContextAccessor.HttpContext.GetUser();
            int idUser = -1;
            if(userId != null)
            {
                idUser = userId.Id;
            }
            var query = _unitOfWork.PostRepository.QueryFromSql<CustomPostHome>($"SELECT [Id], [UserId]\r\n      ,[Caption]\r\n      ,[Audience]\r\n      ,[Layout]\r\n      ,[Status]\r\n      ,[CreatedDate]\r\n    ,(SELECT COUNT(*) FROM Comment WHERE PostId = Post.Id) as CommentCount\r\n    ,(SELECT COUNT(*) FROM [Like]  WHERE PostId = Post.Id) as LikeCount\r\n\t,(SELECT type FROM [Like]  WHERE PostId = Post.Id and UserId = {idUser}) as LikeTypeId\r\n  FROM [Post] \r\n");
            var rs = await _unitOfWork.PostRepository.PageWithQueryAsync<CustomPostHome>(pageIndex, pageSettings.Size, query, f => true, null, f => f.Include(a => a.MediaPosts).Include(f=>f.User).Include(f=>f.LikeType));
            return mapper.Map<PaginatedItems<CustomPostHomeDto>>(rs);
        }
       
        public async Task<PostDto> FindById(int id)
        {
            var item = await _unitOfWork.PostRepository.Find(f=>f.Id == id, i=>i.Include(f=>f.MediaPosts).Include(f=>f.User));
            var comment = await _unitOfWork.CommentRepository.PageAsync(1, pageSettings.Size, f => f.PostId == item.Id, null, f => f.Include(i => i.Medias).Include(f=>f.User));
            var like = await _unitOfWork.LikeRepository.PageAsync(1, 2 * pageSettings.Size, f => f.PostId == item.Id, null, f => f.Include(f => f.User));
            item.Comments = comment.Data;
            item.Likes = like.Data;
            return mapper.Map<PostDto>(item);
        }

    }
}
