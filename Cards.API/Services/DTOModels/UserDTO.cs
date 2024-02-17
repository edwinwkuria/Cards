using System.Runtime.CompilerServices;
using Cards.Infrastructure.Entities;

namespace Cards.Services.DTOModels;

public class UserDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password => FirstName + "123";
    public string Role { get; set; }
}