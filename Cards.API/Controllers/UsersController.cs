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
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<string> GetUser(Guid id)
    {
        var user = _userService.GetUserById(id);
        return Ok(user);
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginBindingModel card)
    {
        var login = _userService.LoginUser();
        return Ok(login);
    }
}

