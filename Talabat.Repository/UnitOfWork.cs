using System.Collections;
using Talabat.Domain;
using Talabat.Domain.Entities;
using Talabat.Domain.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository;

public class UnitOfWork : IUnitOfWork
{
    private Hashtable _repositories;
    private readonly StoreContext _context;

    public UnitOfWork(StoreContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    public async Task<int> completeAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();

    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(type))
        {
            var Repository= new GenericRepository<TEntity>(_context);
            _repositories.Add(type, Repository);
           
        }
        return _repositories[type] as  IGenericRepository<TEntity>;

       
    }
}
