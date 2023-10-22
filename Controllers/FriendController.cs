using BE_SOCIALNETWORK.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using BE_SOCIALNETWORK.Helper;
using BE_SOCIALNETWORK.DTO;
using parking_center.Extensions;

namespace BE_SOCIALNETWORK.Controllers;

[ApiController]
[Route("api/friend")]
public class FriendController : ControllerBase
{
    private readonly IFriendService friendService;  
    public FriendController(IFriendService friendService)
    {
        this.friendService = friendService; 
    }

    [HttpGet]
    [Route("/{idUser}/page/{page}")]
    public async Task<IActionResult> GetCommentPageByIdPost(int page, int idUser)
    {
        var rs = await friendService.ListAsyncPageByIdUser(page, idUser);
        return Ok(rs);
    }

    [Authorize]
    [HttpPost]
    [Route("create_request")]
    public async Task<IActionResult> CreateRequestFriend([FromForm] int idUser)
    {
        UserDto user = HttpContext.GetUser();
        var rs = await friendService.CreateRequestFriend(user.Id, idUser);
        if (rs == null) return BadRequest();
        return Ok(rs);
    }

    [Authorize]
    [HttpPost]
    [Route("accept")]
    public async Task<IActionResult> AcceptFriend([FromForm] int idFriend)
    {
        var rs = await friendService.AcceptFriend(idFriend);
        if (rs)
        {
            return Ok(rs);
        }
        return BadRequest();
    }

    [Authorize]
    [HttpPost]
    [Route("reject")]
    public async Task<IActionResult> RejectFriend([FromForm] int idFriend)
    {
        var rs = await friendService.RejectFriend(idFriend);
        if (rs)
        {
            return Ok(rs);
        }
        return BadRequest();
    }
}
