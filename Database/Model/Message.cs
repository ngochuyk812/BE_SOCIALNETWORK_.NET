using BE_SOCIALNETWORK.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class Message : BaseModel
    {
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int Status { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }
        public virtual ICollection<MediaMessage> MediaMessages { get; set; }
    }
}
