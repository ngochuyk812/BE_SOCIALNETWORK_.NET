using BE_SOCIALNETWORK.Database.Contracts;
using BE_SOCIALNETWORK.Database.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SOCIALNETWORK.DTO
{
    public class LikeDto : BaseModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public PostDto Post { get; set; }
        public UserDto User { get; set; }
        public LikeType LikeType { get; set; }

    }
}
