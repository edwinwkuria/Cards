using Cards.BindingModels.UsersController;
using Cards.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> GetUsers()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public ActionResult<string> GetUser(int id)
    {
        return Ok();
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginBindingModel card)
    {
        return Ok();
    }
}

