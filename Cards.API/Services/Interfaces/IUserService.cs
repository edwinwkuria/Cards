using System.Net;
using Cards.Infrastructure.Entities;
using Cards.Services.DTOModels;

namespace Cards.Services.Interfaces;

public interface IUserService
{
    (HttpStatusCode statusCode, string message, List<UserDTO> data) GetAllUsers();
    (HttpStatusCode statusCode, string message, UserDTO data) GetUserById(Guid id);
    (HttpStatusCode statusCode, string message, string data) LoginUser(User user);
}