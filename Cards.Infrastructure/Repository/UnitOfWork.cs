using System.Collections;
using Cards.Infrastructure.Context;
using Cards.Infrastructure.Entities;
using Cards.Infrastructure.Repository.Abstract;

namespace Cards.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private IEntitiesContext _context;
    public bool _disposed;
    private Hashtable _repositories;
    private Hashtable _dapperrepositories;

    public UnitOfWork(IEntitiesContext context)
    {
        _context = context;
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel
    {
        if (_repositories == null)
        {
            _repositories = new Hashtable();
        }

        var type = typeof(TEntity).Name;
        if (_repositories.ContainsKey(type))
        {
            return (IRepository<TEntity>)_repositories[type];
        }

        var repositoryType = typeof(EntityRepository<>);
        _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));
        return (IRepository<TEntity>)_repositories[type];
    }
}    