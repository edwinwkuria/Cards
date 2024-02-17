using Cards.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.Context;

internal class EntityConfiguration
{
    public static ModelBuilder EntitiesBuilder(ModelBuilder builder)
    {
        #region user
        builder.Entity<User>(user =>
        {
            user.ToTable("users");
            user.HasKey(u => u.Id);
            user.Property(u => u.Id).HasMaxLength(25).HasColumnName("id");
            user.Property(u => u.FirstName).HasMaxLength(50).HasColumnName("firstname");
            user.Property(u => u.LastName).HasMaxLength(50).HasColumnName("lastname");
            user.Property(u => u.Email).HasMaxLength(50).HasColumnName("email");
            user.Property(u => u.Role).HasMaxLength(20).HasColumnName("role");
            user.Property(u => u.Password).HasMaxLength(500).HasColumnName("password");
            user.Property(u => u.Salt).HasColumnName("salt");
            user.Property(u => u.CreatedOn).HasColumnName("created_on");
            user.Property(u => u.CreatedBy).HasMaxLength(25).HasColumnName("created_by");
            user.Property(u => u.IsActive).HasColumnName("is_active");
            user.Property(u => u.IsDeleted).HasColumnName("is_deleted");
            user.Property(u => u.DeletedOn).HasColumnName("deleted_on");
            user.Property(u => u.DeletedBy).HasMaxLength(25).HasColumnName("deleted_by");
        });
        #endregion
        #region cards
        builder.Entity<Card>(user =>
        {
            user.ToTable("cards");
            user.HasKey(u => u.Id);
            user.Property(u => u.Id).HasMaxLength(25).HasColumnName("id");
            user.Property(u => u.Name).HasMaxLength(250).HasColumnName("name");
            user.Property(u => u.Description).HasMaxLength(500).HasColumnName("description");
            user.Property(u => u.Colour).HasMaxLength(50).HasColumnName("colour");
            user.Property(u => u.Status).HasMaxLength(25).HasColumnName("status");
            user.Property(u => u.CreatedOn).HasColumnName("created_on");
            user.Property(u => u.CreatedBy).HasMaxLength(25).HasColumnName("created_by");
            user.Property(u => u.IsActive).HasColumnName("is_active");
            user.Property(u => u.IsDeleted).HasColumnName("is_deleted");
            user.Property(u => u.DeletedOn).HasColumnName("deleted_on");
            user.Property(u => u.DeletedBy).HasMaxLength(25).HasColumnName("deleted_by");
        });
        #endregion

        return builder;
    }
}