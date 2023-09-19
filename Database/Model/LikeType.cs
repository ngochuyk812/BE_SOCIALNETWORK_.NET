using BE_SOCIALNETWORK.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class LikeType : BaseModel
    {
        public string Name { get; set; }
    }
}
