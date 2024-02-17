using Cards.Infrastructure.CryptoGraphy;
using Cards.Infrastructure.DataTypes;
using Cards.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.Seeder;

public static class UserSeeder
{
    public static ModelBuilder SeedUsers(ModelBuilder modelBuilder)
    {
        var salt1 = PasswordHelper.GenerateSalt();
        var salt2 = PasswordHelper.GenerateSalt();
        var salt3 = PasswordHelper.GenerateSalt();
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Role = UserRoles.Member,
                Salt = salt1, Password = PasswordHelper.HashPassword("John123", salt1)
            },
            
            new User
            {
                Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "jane.doe@gmail.com", Role = UserRoles.Member,
                Salt = salt2, Password = PasswordHelper.HashPassword("Jane123", salt1)
            },
            
            new User
            {
                Id = Guid.NewGuid(), FirstName = "Michael", LastName = "Brown", Email = "michael.brown@gmail.com", Role = UserRoles.Admin,
                Salt = salt3, Password = PasswordHelper.HashPassword("Michael123", salt1)
            }
            );
        
            

        return modelBuilder;
    }
}