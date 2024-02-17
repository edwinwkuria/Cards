using System.Data;
using System.Data.Common;
using Cards.Infrastructure.Seeder;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.Context;

public class DatabaseContext : DbContext, IEntitiesContext
{
    private DbTransaction? _transaction;
    
    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        EntityConfiguration.EntitiesBuilder(builder);
        base.OnModelCreating(builder);

        UserSeeder.SeedUsers(builder);
    }
    
    public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        => base.Set<TEntity>();
    
    public void BeginTransaction()
    {
        if (this.Database.GetDbConnection().State == ConnectionState.Open)
        {
            return;
        }
        this.Database.GetDbConnection().Open();
        _transaction = this.Database.GetDbConnection().BeginTransaction();
    }

    public int Commit()
    {
        var saveChanges = SaveChanges();
        _transaction?.Commit();
        return saveChanges;
    }

    public Task<int> CommitAsync()
    {
        var saveChangesAsync = SaveChangesAsync();
        _transaction?.Commit();
        return saveChangesAsync;
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }
}