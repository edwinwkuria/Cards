﻿namespace Cards.Infrastructure.Entities;

public class User : BaseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get ; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}