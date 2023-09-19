using BE_SOCIALNETWORK.Database.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class MediaMessage : BaseModel
    {
        [ForeignKey("Message")]
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
        public string Type { get; set; }

    }
}