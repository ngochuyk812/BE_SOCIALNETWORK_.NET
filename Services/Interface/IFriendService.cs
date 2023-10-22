using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Extensions;
using BE_SOCIALNETWORK.Repositories.Contracts;
using BE_SOCIALNETWORK.Repositories.IRespositories;

namespace BE_SOCIALNETWORK.Services.Interface
{
    public interface IFriendService 
    {
        Task<PaginatedItems<FriendDto>> ListAsyncPageByIdUser(int pageIndex, int idUser);
        Task<FriendDto> CreateRequestFriend(int idUserRequest, int idUserAccept);
        Task<bool> AcceptFriend(int idFriend);
        Task<bool> RejectFriend(int idFriend);

    }
}
