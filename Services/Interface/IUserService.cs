using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.Repositories.Contracts;
using BE_SOCIALNETWORK.Repositories.IRespositories;
using Microsoft.AspNetCore.Mvc;

namespace BE_SOCIALNETWORK.Services.Interface
{
    public interface IUserService
    {
        public Task<string> SignUp(string username, string password, string email, string fullName);
        public Task<IActionResult> SignIn(string username, string password, Func<dynamic, IActionResult> callback);

        public Task<bool> FindByUsernameOrEmail(string username, string email);

    }
}
