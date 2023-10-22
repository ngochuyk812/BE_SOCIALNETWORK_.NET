using BE_SOCIALNETWORK.Database.Contracts;
using Microsoft.AspNetCore.Http.Extensions;

namespace BE_SOCIALNETWORK.DTO
{
    public class LikeTypeDto : BaseModel
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        
    }
}
