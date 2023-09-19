using BE_SOCIALNETWORK.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class Participant : BaseModel
    {
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }  

    }
}
