using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Helper;
using BE_SOCIALNETWORK.Payload.Request;
using BE_SOCIALNETWORK.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using parking_center.Extensions;

namespace BE_SOCIALNETWORK.Controllers;

[ApiController]
[Route("api/like")]
public class LikeController : ControllerBase
{
    private readonly ILikeTypeService likeTypeService;
    private readonly ILikeService likeService;
    public LikeController(ILikeTypeService likeTypeService, ILikeService likeService)
    {
        this.likeService = likeService;
        this.likeTypeService = likeTypeService;
    }

    [HttpGet]
    [Route("post/{idPost}/page/{page}")]
    public async Task<IActionResult> GetPageByIdPost(int page, int idPost)
    {
        var rs = await likeService.ListAsyncPageByIdPost(page, idPost);
        return Ok(rs);
    }


    [Authorize]
    [HttpPost]
    [Route("create")]
    [Consumes("application/json")]
    public async Task<IActionResult> Like(CreateLikeRequest createBody)
    {
        UserDto user = HttpContext.GetUser();
        var rs = await likeService.CreateLike(createBody.Type, createBody.IdPost, user.Id);
        if (rs)
        {
            return Ok(rs);

        }
        return BadRequest();
    }


    [Authorize]
    [HttpPost]
    [Route("remove")]
    [Consumes("application/json")]
    public async Task<IActionResult> UnLike([FromForm] int idPost)
    {
        UserDto user = HttpContext.GetUser();
        var rs = await likeService.UnLike(idPost, user.Id);
        if (rs)
        {
            return Ok(rs);
        }
        return BadRequest();
    }


    [HttpGet]
    [Route("get_type")]
    public async Task<IActionResult> GetLikeType()
    {
        var rs = await likeService.GetLikeTypes();
        return Ok(rs);
    }

}
