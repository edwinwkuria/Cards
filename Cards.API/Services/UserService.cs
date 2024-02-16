using Cards.API.TokenHelper;
using Cards.Infrastructure.Entities;
using Cards.Infrastructure.Repository.Abstract;
using Cards.Services.Interfaces;

namespace Cards.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _iuow;
    private readonly IRepository<User> _repository;
    private readonly IJwtHelper _jwtHelper;

    public UserService(IUnitOfWork iuow, IJwtHelper jwtHelper)
    {
        _iuow = iuow;
        _repository = _iuow.Repository<User>();
        _jwtHelper = jwtHelper;
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

    public string LoginUser(User user)
    {
        return _jwtHelper.GenerateUserToken(user);
    }
}