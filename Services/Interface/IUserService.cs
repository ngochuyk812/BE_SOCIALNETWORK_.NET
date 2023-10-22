using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Payload.Response;
using BE_SOCIALNETWORK.Repositories.Contracts;
using BE_SOCIALNETWORK.Repositories.IRespositories;
using Microsoft.AspNetCore.Mvc;

namespace BE_SOCIALNETWORK.Services.Interface
{
    public interface IUserService
    {
        public Task<string> SignUp(string username, string password, string email, string fullName);
        public Task<SignInResponse> SignIn(string username, string password);
        public Task<bool> FindByUsernameOrEmail(string username, string email);
        public Task<bool> FindByUsername(string username);

    }
}
