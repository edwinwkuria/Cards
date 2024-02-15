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

    public void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();

            if (_repositories != null && _repositories.Values != null && _repositories.Values.OfType<IDisposable>().Any())
            {
                foreach (IDisposable repository in _repositories.Values)
                {
                    repository.Dispose();// dispose all repositries
                }
            }
        }
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    public void BeginTransaction()
    {
        _context.BeginTransaction();
    }

    public int Commit()
    {
        return _context.Commit();
    }

    public void Rollback()
    {
        _context.Rollback();
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public Task<int> CommitAsync()
    {
        return _context.CommitAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
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