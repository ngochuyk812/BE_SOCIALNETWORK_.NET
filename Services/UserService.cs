
using AutoMapper;
using BE_SOCIALNETWORK.Config;
using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.Mapping;
using BE_SOCIALNETWORK.Payload.Response;
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
        private readonly IMapper mapper;
        public UserService(IUnitOfWork unitOfWork, IOptions<JWTSettings> jwtSettings, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.jWTSettings = jwtSettings.Value;
        }

        public async Task<bool> FindByUsernameOrEmail(string username, string email)
        {
            var user = await unitOfWork.UserRepository.Find(t=>t.Username == username || t.Email == email, null);
            return user != null;
        }
        public async Task<bool> FindByUsername(string username)
        {
            var user = await unitOfWork.UserRepository.Find(t => t.Username == username , null);
            return user != null;
        }

        public async Task<SignInResponse> SignIn(string username, string password)
        {
            var user = await unitOfWork.UserRepository.Find(t => t.Username == username , null);
            if(user == null)
            {
                return null;
            }
            bool verity = SecretHasher.Verify(password, user.Password);
            if (!verity)
            {
                return null;
            }
            string token = GenerateJWT(user);
            var rs = mapper.Map<SignInResponse>(user);
            rs.AccessToken = token;
            return rs;
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
