using System.Linq.Expressions;
using Cards.Infrastructure.Entities;
using Cards.Infrastructure.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.Repository;

public class EntityRepository <TEntity> : IRepository<TEntity> where TEntity : BaseModel
{
    private DbContext context;
    internal DbSet<TEntity> dbSet;
    private bool _disposed;

    public EntityRepository(DbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> All => dbSet.Where(entity => !entity.IsDeleted).AsQueryable();
    
    public void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.DeletedOn = DateTime.UtcNow;
        context.Update(entity);
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
    {
        IQueryable<TEntity> query = dbSet;
        query = query.Where(c => !c.IsDeleted);

        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);

        return query;
    }

    public TEntity? Get(object id)
    {
        var entity = dbSet.Find(id);
        return entity is { IsDeleted: false } ? entity : null;
    }

    public async Task<TEntity?> GetAsync(object id)
    {
        var entity = await dbSet.FindAsync(id).AsTask();
        return entity is { IsDeleted: false } ? entity : null;
    }

    public TEntity Insert(TEntity entity)
    {
        entity.CreatedOn = DateTime.UtcNow;
        return dbSet.Add(entity).Entity;
    }

    public Task InsertAsync(TEntity entity)
    {
        return dbSet.AddAsync(entity).AsTask();
    }

    public void Update(TEntity entity)
    {
        dbSet.Update(entity);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            context.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}