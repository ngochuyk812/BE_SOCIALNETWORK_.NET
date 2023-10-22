using BE_SOCIALNETWORK.Database;
using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.Repositories.Contracts;
using BE_SOCIALNETWORK.Repositories.IRespositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_SOCIALNETWORK.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<User> _logger;

    public UserController(ILogger<User> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        this.unitOfWork = unitOfWork;
    }

}
