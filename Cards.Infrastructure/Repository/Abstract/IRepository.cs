using System.Linq.Expressions;
using Cards.Infrastructure.Entities;

namespace Cards.Infrastructure.Repository.Abstract;

public interface IRepository<TEntity> : IDisposable where TEntity : BaseModel
{
    IQueryable<TEntity> All { get; }

    /// <summary>
    /// Get all entities from db based on filter, order and included properties
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    IQueryable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

    /// <summary>
    /// Get single entity by primary key
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    TEntity Get(object id);

    /// <summary>
    /// Get single entity by primary key async
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(object id);

    /// <summary>
    /// Insert entity to db
    /// </summary>
    /// <param name="entity"></param>
    TEntity Insert(TEntity entity);

    /// <summary>
    /// Insert to db async
    /// </summary>
    /// <param name="entity"></param>
    Task InsertAsync(TEntity entity);
    Task BulkInsertAsync(List<TEntity> entities);
    /// <summary>
    /// Update entity in db
    /// </summary>
    /// <param name="entity"></param>
    void Update(TEntity entity);
    void BulkUpdate(List<TEntity> entities);
    /// <summary>
    /// Delete entity from db by primary key
    /// </summary>
    /// <param name="id"></param>
    void Delete(TEntity entity);
}
