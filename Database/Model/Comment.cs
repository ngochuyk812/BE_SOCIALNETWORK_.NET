using BE_SOCIALNETWORK.Database.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class Comment : BaseModel
    {
        [ForeignKey("CommentParent")]
        public int? ParentId { get; set; }
        [ForeignKey("User")]  
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public virtual Comment CommentParent { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<MediaComment> Medias { get; set; }


    }
}