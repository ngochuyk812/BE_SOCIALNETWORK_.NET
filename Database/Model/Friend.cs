using BE_SOCIALNETWORK.Database.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class Friend : BaseModel
    {
        [ForeignKey("UserRequest")]       
        public int UserRequestId { get; set; }
        [ForeignKey("UserAccept")]
        public int UserAcceptId { get; set; }
        public int Status { get; set; }
        public User UserRequest { get; set; }
        public User UserAccept { get; set; }
    }
}
