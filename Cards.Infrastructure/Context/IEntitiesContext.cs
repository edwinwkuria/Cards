using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.Context;

public interface IEntitiesContext : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    void BeginTransaction();
    int Commit();
    void Rollback();
    Task<int> CommitAsync();
}