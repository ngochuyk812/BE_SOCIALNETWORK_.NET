using AutoMapper;
using BE_SOCIALNETWORK.Config;
using BE_SOCIALNETWORK.Database;
using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Extensions;
using BE_SOCIALNETWORK.Repositories.Contracts;
using BE_SOCIALNETWORK.Repositories.IRespositories;
using BE_SOCIALNETWORK.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BE_SOCIALNETWORK.Services.Interface
{
    public class FriendService :IFriendService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PageSettings pageSettings;
        private readonly IMapper mapper;
        public FriendService(IUnitOfWork unitOfWork, IOptions<PageSettings> pageSettings, IMapper mapper)
        {
            this.mapper = mapper;
            this.pageSettings = pageSettings.Value;
            this.unitOfWork = unitOfWork;
        }
        public async Task<PaginatedItems<FriendDto>> ListAsyncPageByIdUser(int pageIndex, int idUser)
        {
            var page = await unitOfWork.FriendRepository.PageAsync(pageIndex, pageSettings.Size, f => f.UserRequestId == idUser || f.UserAcceptId == idUser, null, i => i.Include(f=>f.UserAccept).Include(f=>f.UserRequest));
            return mapper.Map<PaginatedItems<FriendDto>>(page);
        }

    }
}
