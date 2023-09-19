using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Extensions;
using BE_SOCIALNETWORK.Payload.Request;

namespace BE_SOCIALNETWORK.Services.Interface
{
    public interface ICommentService
    {
        Task<PaginatedItems<CommentDto>> ListAsyncPageByIdPost(int pageIndex, int idPost);
        Task<CommentDto> CreateComment(CreateCommentRequest comment, List<MediaDto> media, int userId);
        Task<bool> RemoveComment(int idComment);
    }
}
