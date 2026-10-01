using Microsoft.EntityFrameworkCore;
using Talabat.Domain.Entities;
using Talabat.Domain.Repositories;
using Talabat.Domain.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext _storeContext;

    public GenericRepository(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }

    #region Without specification
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _storeContext.Set<T>().ToListAsync();
    }


    public async Task<T> GetByIdAsync(int id)
    {

        return await _storeContext.Set<T>().FindAsync(id);
    }

    #endregion

    #region With specification
    public async Task<IReadOnlyList<T>> GetAllWithSpec(Ispecifications<T> spec)
    {
        var result= await SpecificationEvalutor<T>.GetQuery(_storeContext.Set<T>(), spec).ToListAsync();
        return result ;
    }
    public async Task<T> GetByIdWithSpec(Ispecifications<T> spec)
    {
       var result =await SpecificationEvalutor<T>.GetQuery(_storeContext.Set<T>(),spec).FirstOrDefaultAsync();
       
        return  result;
    
    }

    public Task<int> CountWithSpecAsync(Ispecifications<T> spec)
    {
        return SpecificationEvalutor<T>.GetQuery(_storeContext.Set<T>(), spec).CountAsync();
    }

    public async Task AddAsync(T entity)
    {
      await _storeContext.Set<T>().AddAsync(entity);
    }

    public void UpdateAsync(T entity)
    {
        _storeContext.Set<T>().Update(entity);

       
    }

    public void DeleteAsync(T entity)
    {
        _storeContext.Set<T>().Remove(entity);
    }


    #endregion

}
