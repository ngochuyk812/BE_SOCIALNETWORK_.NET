using BE_SOCIALNETWORK.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SOCIALNETWORK.DTO
{
    public class FriendDto : BaseModel
    {
        public int UserRequestId { get; set; }
        public int UserAcceptId { get; set; }
        public int Status { get; set; }
        public UserDto UserRequest { get; set; }
        public UserDto UserAccept { get; set; }
    }
}
