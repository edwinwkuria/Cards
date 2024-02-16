using Cards.Infrastructure.DataTypes;

namespace Cards.Services.DTOModels;

public class CurrentUserDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public UserRoles? Role { get; set; }
}