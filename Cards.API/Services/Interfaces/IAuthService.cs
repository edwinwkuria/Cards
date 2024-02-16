using Cards.Services.DTOModels;

namespace Cards.Services.Interfaces;

public interface IAuthService
{
    CurrentUserDTO GetCurrentUser();
}