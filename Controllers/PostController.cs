using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Helper;
using BE_SOCIALNETWORK.Payload.Request;
using BE_SOCIALNETWORK.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BE_SOCIALNETWORK.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly IPostService postService;
    private readonly IS3Service uploadFieS3Service;


    private readonly ILogger<PostController> _logger;

    public PostController(ILogger<PostController> logger, IPostService postService, IS3Service uploadFieS3Service)
    {
        _logger = logger;
        this.postService = postService;
        this.uploadFieS3Service = uploadFieS3Service;
    }

    [HttpPost]
    [Route("create")]
    [Authorize]
    public async Task<IActionResult> Create([FromForm] CreatePostRequest body)
    {
        var path = new List<MediaDto>();
        if (body.Files != null)
        {
            path = await uploadFieS3Service.UploadFilesToS3(body.Files, "post");
        }
        var rs = await postService.UploadPost(body, path);
        if (rs!=null)
        {
            return Ok(rs);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("page/{page}")]
    public async Task<IActionResult> GetPage(int page)
    {
        var rs = await postService.ListAsyncPage(page);
        return Ok(rs);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Detail(int id)
    {
        var rs = await postService.FindById(id);
        if (rs == null)
            return NotFound();
        return Ok(rs);
    }



}
