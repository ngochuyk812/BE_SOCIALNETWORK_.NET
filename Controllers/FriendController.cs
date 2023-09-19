using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Payload.Request;
using BE_SOCIALNETWORK.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        var userId = HttpContext.User.FindFirst("Id").Value;
        if (userId == null)
        {
            return BadRequest();
        }
        var dataMedia = new List<MediaDto>();
        if (commentRequest?.Files?.Count > 0)
        {
             dataMedia = await s3Service.UploadFilesToS3(commentRequest.Files, "comment");
        }

        var comment = await commentService.CreateComment(commentRequest, dataMedia, int.Parse(userId));
        if(comment == null) return BadRequest();
        return Ok(comment);
    }

    [Authorize]
    [HttpPost]
    [Route("remove")]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IActionResult> RemoveComment([FromForm] int idComment)
    {
        var userId = HttpContext.User.FindFirst("Id").Value;
        if (userId == null)
        {
            return BadRequest();
        }
        var rs = await commentService.RemoveComment(idComment);
        if (rs)
        {
            return Ok(rs);
        }
        return BadRequest();
    }

}
