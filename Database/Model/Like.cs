using BE_SOCIALNETWORK.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class Like : BaseModel
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("LikeType")]
        public int Type { get; set; }
        public virtual Post Post { get; set; }
        public User User { get; set; }
        public LikeType LikeType { get; set; }


    }
}
