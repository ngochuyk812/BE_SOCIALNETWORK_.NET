using BE_SOCIALNETWORK.Database;
using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.Repositories.Contracts;
using BE_SOCIALNETWORK.Repositories.IRespositories;
using BE_SOCIALNETWORK.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> Like([FromForm] int idPost,[FromForm] int type)
    {
        var userId = HttpContext.User.FindFirst("Id").Value;
        if (userId == null)
        {
            return BadRequest();
        }
        var rs = await likeService.CreateLike(type, idPost, int.Parse(userId));
        if (rs)
        {
            return Ok(rs);

        }
        return BadRequest();
    }


    [Authorize]
    [HttpPost]
    [Route("remove")]
    public async Task<IActionResult> UnLike([FromForm] int idPost)
    {
        var userId = HttpContext.User.FindFirst("Id").Value;
        if (userId == null)
        {
            return BadRequest();
        }
        var rs = await likeService.UnLike(idPost, int.Parse(userId));
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
