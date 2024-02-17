using System.Net;
using AutoMapper;
using Cards.API.ConfigModels;
using Cards.Infrastructure.CryptoGraphy;
using Cards.Infrastructure.Entities;
using Cards.Infrastructure.Repository.Abstract;
using Cards.Services.DTOModels;
using Cards.Services.Interfaces;

namespace Cards.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _iuow;
    private readonly IRepository<User> _repository;
    private readonly IJwtHelper _jwtHelper;
    private readonly IMapper _mapper;
    public UserService(IUnitOfWork iuow, IJwtHelper jwtHelper, IMapper mapper)
    {
        _iuow = iuow;
        _repository = _iuow.Repository<User>();
        _jwtHelper = jwtHelper;
        _mapper = mapper;
    }

    public (HttpStatusCode statusCode, string message, List<UserDTO> data) GetAllUsers()
    {
        var users = _repository.All.Select(x => _mapper.Map<UserDTO>(x)).ToList();
        return (HttpStatusCode.OK, "Success", users);
    }

    public (HttpStatusCode statusCode, string message, UserDTO data) GetUserById(Guid id)
    {
        var user = _repository.Get(id);
        return (HttpStatusCode.OK, "Success", _mapper.Map<UserDTO>(user));
    }

    public (HttpStatusCode statusCode, string message, string data) LoginUser(User model)
    {
        var user = _repository.All.FirstOrDefault(x => x.Email.Equals(model.Email));
        if (user == null)
            return (HttpStatusCode.BadRequest, "User Not Found", String.Empty);
        
        if(user.Password.Equals(PasswordHelper.HashPassword(model.Password, user.Salt)))
            return (HttpStatusCode.OK, "Success", _jwtHelper.GenerateUserToken(user));
        
        return (HttpStatusCode.BadRequest, "Incorrect Password", String.Empty);
    }
}