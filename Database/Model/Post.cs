using BE_SOCIALNETWORK.Database.Contracts;
using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class Post : BaseModel
    {
        public int UserId { get; set; }   
        public string Caption { get; set; }    
        public int Audience { get; set; }    
        public int Layout { get; set; }    
        public int Status { get; set; }    
        public virtual User User{ get; set; }   
        public virtual ICollection<MediaPost> MediaPosts { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
        public virtual IEnumerable<Like> Likes { get; set; }


    }
}