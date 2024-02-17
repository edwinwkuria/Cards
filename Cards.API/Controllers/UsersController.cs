using System.Net;
using System.Runtime.InteropServices;
using AutoMapper;
using Cards.API.Responses;
using Cards.BindingModels.UsersController;
using Cards.Infrastructure.Entities;
using Cards.Services.DTOModels;
using Cards.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("Get Users")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
    public ActionResult GetUsers()
    {
        try
        {
            var response = _userService.GetAllUsers();
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }

    [HttpGet("{id}", Name = "Get User")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
    public ActionResult<string> GetUser(Guid id)
    {
        try
        {
            var response = _userService.GetUserById(id);
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }

    [HttpPost("login", Name = "Login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public ActionResult Login([FromBody] LoginBindingModel login)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode((int) HttpStatusCode.BadRequest,ModelState);
        }
        
        try
        {
            var user = _mapper.Map<User>(login);
            var response = _userService.LoginUser(user);
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }
}

