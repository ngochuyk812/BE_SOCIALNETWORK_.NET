using BE_SOCIALNETWORK.Config;
using BE_SOCIALNETWORK.Database;
using BE_SOCIALNETWORK.Helper;
using BE_SOCIALNETWORK.Mapping;
using BE_SOCIALNETWORK.Repositories.Contracts;
using BE_SOCIALNETWORK.Repositories.IRespositories;
using BE_SOCIALNETWORK.Services;
using BE_SOCIALNETWORK.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
#nullable enable
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var services = builder.Services;
var _configuration = builder.Configuration;
services.Configure<JWTSettings>(_configuration.GetSection("JWT"));
services.Configure<PageSettings>(_configuration.GetSection("Pagination"));
services.Configure<AWSSetings>(_configuration.GetSection("AWS"));
services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));


/*services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = _configuration["JWT:Issuer"],
        ValidAudience = _configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
    };
});*/
services.AddDbContext<Social_NetworkContext>(options =>
{
    options.UseSqlServer(_configuration.GetConnectionString("MyConnect"));
});
services.AddHttpContextAccessor();
services.AddControllers();
services.AddAutoMapper
(typeof(MappingProfile).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddControllers()
  .AddNewtonsoftJson(options =>
      options.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore
   );

services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped(typeof(ICommentService), typeof(CommentService));
services.AddScoped(typeof(IFriendService), typeof(FriendService));
services.AddScoped(typeof(ILikeService), typeof(LikeService));
services.AddScoped(typeof(IMediaCommentService), typeof(MediaCommentService));
services.AddScoped(typeof(IMediaMessageService), typeof(MediaMessageService));
services.AddScoped(typeof(IMediaPostService), typeof(MediaPostService));
services.AddScoped(typeof(IMessageService), typeof(MessageService));
services.AddScoped(typeof(IParticipantService), typeof(ParticipantService));
services.AddScoped(typeof(IPostService), typeof(PostService));
services.AddScoped(typeof(IRoomService), typeof(RoomService));
services.AddScoped(typeof(IUserService), typeof(UserService));
services.AddScoped(typeof(IS3Service), typeof(S3Service));
services.AddScoped(typeof(ILikeTypeService), typeof(LikeTypeService));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<JwtMiddleware>();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();
app.Run();
