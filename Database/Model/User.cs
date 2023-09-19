using BE_SOCIALNETWORK.Database.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? RefeshToken { get; set; }
        public string FullName{ get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? Banner { get; set; }
        public string? Story { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}