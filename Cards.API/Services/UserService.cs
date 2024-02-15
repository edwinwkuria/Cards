using Cards.Infrastructure.Entities;
using Cards.Infrastructure.Repository.Abstract;
using Cards.Services.Interfaces;

namespace Cards.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _iuow;
    private readonly IRepository<User> _repository;

    public UserService(IUnitOfWork iuow)
    {
        _iuow = iuow;
        _repository = _iuow.Repository<User>();
    }

    public List<User> GetAllUsers()
    {
        var users = _repository.All.ToList();
        return users;
    }

    public User GetUserById(Guid id)
    {
        var user = _repository.Get(id);
        return user;
    }

    public string LoginUser()
    {
        return "token";
    }
}