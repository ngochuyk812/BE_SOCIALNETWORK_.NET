using BE_SOCIALNETWORK.Database.Contracts;
using BE_SOCIALNETWORK.Database.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SOCIALNETWORK.Database.Model
{
    [NotMapped]
    public class CustomPostHome : BaseModel
    {
        public int UserId { get; set; }
        public string Caption { get; set; }
        public int Audience { get; set; }
        public int Layout { get; set; }
        public int Status { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public int? LikeTypeId { get; set; }
        public virtual LikeType LikeType { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<MediaPost> MediaPosts { get; set; }
    }
}
