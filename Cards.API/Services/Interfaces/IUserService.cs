using Cards.Infrastructure.Entities;
using Cards.Services.DTOModels;

namespace Cards.Services.Interfaces;

public interface IUserService
{
    List<UserDTO> GetAllUsers();
    UserDTO GetUserById(Guid id);
    string LoginUser(User user);
}