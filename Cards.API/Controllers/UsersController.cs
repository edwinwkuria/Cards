using AutoMapper;
using Cards.BindingModels.UsersController;
using Cards.Infrastructure.Entities;
using Cards.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
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
    public ActionResult Login([FromBody] LoginBindingModel login)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user = _mapper.Map<User>(login);
        var token = _userService.LoginUser(user);
        return Ok(token);
    }
}

