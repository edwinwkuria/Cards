using System.ComponentModel.DataAnnotations;

namespace Cards.BindingModels.UsersController;

public class LoginBindingModel
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}