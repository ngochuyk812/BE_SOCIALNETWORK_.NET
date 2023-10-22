using BE_SOCIALNETWORK.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SOCIALNETWORK.DTO
{
    public class MessageDto : BaseModel
    {
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public RoomDto Room { get; set; }
        public UserDto User { get; set; }
        public virtual ICollection<MediaMessageDto> MediaMessages { get; set; }
    }
}
    