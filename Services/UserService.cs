
using BE_SOCIALNETWORK.Config;
using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.Mapping;
using BE_SOCIALNETWORK.Repositories.IRespositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_SOCIALNETWORK.Services.Interface
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly JWTSettings jWTSettings;
        public UserService(IUnitOfWork unitOfWork, IOptions<JWTSettings> jwtSettings)
        {
            this.unitOfWork = unitOfWork;
            this.jWTSettings = jwtSettings.Value;
        }

        public async Task<bool> FindByUsernameOrEmail(string username, string email)
        {
            var user = await unitOfWork.UserRepository.Find(t=>t.Username == username || t.Email == email, null);
            return user != null;
        }

        public async Task<IActionResult> SignIn(string username, string password, Func<dynamic, IActionResult> callback)
        {
           var user = await unitOfWork.UserRepository.Find(t => t.Username == username , null);
            if(user == null)
            {
                return callback(new { status = "error", data = "Username does not exist" });
            }
            bool verity = SecretHasher.Verify(password, user.Password);
            if (!verity)
            {
                return callback(new { status = "error", data = "Incorrect password" });
            }
            string token = GenerateJWT(user);
            return callback(new { status = "success", data = new { token= token } });
        }
        private string GenerateJWT(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userInfo.Username),
                new Claim("Id",userInfo.Id + ""),
                new Claim(ClaimTypes.Email,userInfo.Email)
            };
            var token = new JwtSecurityToken(jWTSettings.Issuer,
                jWTSettings.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(jWTSettings.DurationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> SignUp(string username, string password, string email, string fullName)
        {
            string hashPassword = SecretHasher.Hash(password);
            User user = new User
            {
                Username = username,
                Email = email,
                FullName = fullName,
                Password = hashPassword
            };
            var rs = await unitOfWork.UserRepository.AddAsync(user);
            unitOfWork.CommitAsync();
            return rs.Username;
        }

    }
}
