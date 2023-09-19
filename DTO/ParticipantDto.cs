using BE_SOCIALNETWORK.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class ParticipantDto : BaseModel
    {
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }  

    }
}
