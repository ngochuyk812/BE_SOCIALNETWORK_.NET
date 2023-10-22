using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Extensions;
using BE_SOCIALNETWORK.Repositories.Contracts;
using BE_SOCIALNETWORK.Repositories.IRespositories;

namespace BE_SOCIALNETWORK.Services.Interface
{
    public interface ILikeService
    {
        Task<PaginatedItems<LikeDto>> ListAsyncPageByIdPost(int pageIndex, int idPost);
        Task<bool> CreateLike(int typeLike, int idPost, int idUser);
        Task<bool> UnLike(int idPost, int idUser);
        Task<List<LikeTypeDto>> GetLikeTypes();

    }
}
