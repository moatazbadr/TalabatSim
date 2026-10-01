using Talabat.Domain.Entities;
using Talabat.Domain.Repositories;

namespace Talabat.Domain;

public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<TEntity>Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> completeAsync();
}
