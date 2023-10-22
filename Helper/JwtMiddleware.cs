using BE_SOCIALNETWORK.Config;
using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Services.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_SOCIALNETWORK.Helper
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTSettings JWTSettings;
        public JwtMiddleware(RequestDelegate next, IOptions<JWTSettings> jwtSettings)
        {
            _next = next;
            JWTSettings = jwtSettings.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                //Validate the token
                attachUserToContext(context, token);
            await _next(context);
        }
        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key));
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidIssuer = JWTSettings.Issuer,
                    ValidAudience = JWTSettings.Audience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims;
                var user = new UserDto
                {
                    Id = int.Parse(claims.First(f => f.Type == "Id").Value),
                    Username = claims.First(f => f.Type == ClaimTypes.Name).Value
                };
                context.Items["User"] = user;
            }
            catch (Exception)
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
