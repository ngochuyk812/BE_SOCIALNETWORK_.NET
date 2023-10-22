using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Helper;
using BE_SOCIALNETWORK.Payload.Request;
using BE_SOCIALNETWORK.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using parking_center.Extensions;

namespace BE_SOCIALNETWORK.Controllers;

[ApiController]
[Route("api/comment")]
public class CommentController : ControllerBase
{
    private readonly ICommentService commentService;
    private readonly IS3Service s3Service;

    public CommentController(ICommentService commentService, IS3Service s3Service)
    {
        this.commentService = commentService;
        this.s3Service = s3Service;
    }

    [HttpGet]
    [Route("post/{idPost}/page/{page}")]
    public async Task<IActionResult> GetCommentPageByIdPost(int page, int idPost)
    {
        var rs = await commentService.ListAsyncPageByIdPost(page, idPost);
        return Ok(rs);
    }

    [Authorize]
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Comment([FromForm] CreateCommentRequest commentRequest)
    {
        UserDto user = HttpContext.GetUser();
        var dataMedia = new List<MediaDto>();
        if (commentRequest?.Files?.Count > 0)
        {
             dataMedia = await s3Service.UploadFilesToS3(commentRequest.Files, "comment");
        }

        var comment = await commentService.CreateComment(commentRequest, dataMedia, user.Id);
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
