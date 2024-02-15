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

    public IQueryable<TEntity> All => dbSet.AsQueryable();

    public Task BulkInsertAsync(List<TEntity> entities) => context.AddRangeAsync(entities);

    public void BulkUpdate(List<TEntity> entities)
    {
        context.UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        context.Remove(entity);
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);

        foreach (var include in includes)
            query = query.Include(include);

        return query;
    }

    public TEntity Get(object id)
    {
        return dbSet.Find(id);
    }

    public Task<TEntity> GetAsync(object id)
    {
        return dbSet.FindAsync(id).AsTask();
    }

    public TEntity Insert(TEntity entity)
    {
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