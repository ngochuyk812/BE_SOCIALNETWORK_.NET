using BE_SOCIALNETWORK.DTO;
using System.Linq;

namespace parking_center.Extensions
{
    public static class HttpContextExtensions
    {
        public static UserDto GetUser(this HttpContext source)
        {
            return (UserDto)source.Items["User"];
        }

    }
}
