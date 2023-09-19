using BE_SOCIALNETWORK.Database.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class MediaPost : BaseModel
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string Src { get; set; }
        public string Type { get; set; }

    }
}