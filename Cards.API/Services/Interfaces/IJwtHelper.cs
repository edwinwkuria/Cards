using Cards.Infrastructure.Entities;

namespace Cards.Services.Interfaces;

public interface IJwtHelper
{
    string GenerateUserToken(User user);
}