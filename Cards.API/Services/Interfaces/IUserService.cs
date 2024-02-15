using Cards.Infrastructure.Entities;

namespace Cards.Services.Interfaces;

public interface IUserService
{
    List<User> GetAllUsers();
    User GetUserById(Guid id);
    string LoginUser();
}