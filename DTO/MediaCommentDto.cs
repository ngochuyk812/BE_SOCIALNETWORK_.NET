using BE_SOCIALNETWORK.Database.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BE_SOCIALNETWORK.DTO
{
    public class MediaCommentDto : BaseModel
    {
        public int CommentId { get; set; }
        public virtual CommentDto Comment { get; set; }
        public string Type { get; set; }
    }
}