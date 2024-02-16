using Cards.Infrastructure.DataTypes;
using Cards.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.Seeder;

public static class UserSeeder
{
    public static ModelBuilder SeedUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Role = UserRoles.Member,
                Password = "Password"
            },
            
            new User
            {
                Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "jane.doe@gmail.com", Role = UserRoles.Member,
                Password = "Password"
            },
            
            new User
            {
                Id = Guid.NewGuid(), FirstName = "Michael", LastName = "Brown", Email = "jane.doe@gmail.com", Role = UserRoles.Admin,
                Password = "Password"
            }
            );
        
            

        return modelBuilder;
    }
}