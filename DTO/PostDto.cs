using BE_SOCIALNETWORK.Database.Contracts;
using BE_SOCIALNETWORK.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BE_SOCIALNETWORK.DTO
{
    public class PostDto : BaseModel
    {
        public int UserId { get; set; }   
        public string Caption { get; set; }    
        public int Audience { get; set; }    
        public int Layout { get; set; }    
        public int Status { get; set; }
        public long CommentCount { get; set; }
        public long LikeCount { get; set; }

        public virtual UserDto User{ get; set; }   
        public virtual ICollection<MediaPostDto> MediaPosts { get; set; }
        public virtual IEnumerable<CommentDto> Comments { get; set; }
        public virtual IEnumerable<LikeDto> Likes { get; set; }
    }
}
