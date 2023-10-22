using BE_SOCIALNETWORK.Database.Contracts;
using BE_SOCIALNETWORK.Database.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SOCIALNETWORK.DTO
{
    public class CustomPostHomeDto : BaseModel
    {
        public string Caption { get; set; }
        public int Audience { get; set; }
        public int Layout { get; set; }
        public int Status { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public virtual LikeTypeDto LikeType { get; set; }
        public virtual UserDto User { get; set; }
        public virtual ICollection<MediaPostDto> MediaPosts { get; set; }

    }
}
