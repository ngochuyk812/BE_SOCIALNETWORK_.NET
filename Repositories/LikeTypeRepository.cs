using BE_SOCIALNETWORK.Database;
using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.Repositories.Contracts;
using BE_SOCIALNETWORK.Repositories.IRespositories;

namespace BE_SOCIALNETWORK.Repositories.Interface
{
    public class LikeTypeRepository : GenericRepository<LikeType>, ILikeTypeRepository
    {
        public LikeTypeRepository(Social_NetworkContext _context) : base(_context)
        {
        }
    }
}
