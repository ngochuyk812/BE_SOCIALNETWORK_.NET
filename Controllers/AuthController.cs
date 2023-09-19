using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.Payload.Request;
using BE_SOCIALNETWORK.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BE_SOCIALNETWORK.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService userService;
    private readonly ILogger<User> _logger;

    public AuthController(ILogger<User> logger, IUserService userService)
    {
        _logger = logger;
        this.userService = userService;
    }

    [HttpPost]
    [Route("sign_up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest body) 
    { 
        if(string.IsNullOrEmpty(body.Username) || string.IsNullOrEmpty(body.Password) 
            || string.IsNullOrEmpty(body.FullName) || string.IsNullOrEmpty(body.Email))
        {
            return BadRequest(new { status = "error", data = "Please enter complete information" });
        }
        bool find = await userService.FindByUsernameOrEmail(body.Username, body.Email);
        if (find)
        {
            return BadRequest(new {status="error", data= "Username or email exists" });
        }
        string rs = await userService.SignUp(body.Username, body.Password, body.Email, body.FullName);
        return Ok(new { status = "success", data= new { username =rs } });
    }

    [HttpPost]
    [Route("sign_in")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest body)
    {
        if (string.IsNullOrEmpty(body.Username) || string.IsNullOrEmpty(body.Password) )
        {
            return BadRequest(new { status = "error", data = "Please enter complete information" });
        }
        return await userService.SignIn(body.Username, body.Password, (e) =>
        {
            if (e.status == "error")
            {
                return BadRequest(new { status = e.status, data = e.data });
            }
            else
            {
                return Ok(new { status = "success", data = e.data });
            }
        });

    }


}
