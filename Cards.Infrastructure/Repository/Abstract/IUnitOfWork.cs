using Cards.Infrastructure.Entities;

namespace Cards.Infrastructure.Repository.Abstract;

public interface IUnitOfWork
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel;
    int SaveChanges();
    void Dispose(bool disposing);
    void BeginTransaction();
    int Commit();
    void Rollback();
    Task<int> SaveChangesAsync();
    Task<int> CommitAsync();
}