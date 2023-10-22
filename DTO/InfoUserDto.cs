using BE_SOCIALNETWORK.Database.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE_SOCIALNETWORK.DTO
{
    public class InfoUserDto : BaseModel
    {
        public string Username { get; set; }
        public string FullName{ get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? Banner { get; set; }
        public string? Story { get; set; }

    }
}