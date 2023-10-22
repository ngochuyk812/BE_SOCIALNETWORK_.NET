using AutoMapper;
using BE_SOCIALNETWORK.Database.Contracts;
using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.DTO;
using System.IO;
using System.Security.Claims;

namespace BE_SOCIALNETWORK.Mapping
{
    public class UrlBase : IMemberValueResolver<BaseModel, BaseModel, string, string>
    {
        private IHttpContextAccessor _httpContextAccessor;

        public UrlBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(BaseModel source, BaseModel destination, string sourceMember, string destMember, ResolutionContext context)
        {
            string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/{sourceMember}";
            return myHostUrl;
        }
    }
}
