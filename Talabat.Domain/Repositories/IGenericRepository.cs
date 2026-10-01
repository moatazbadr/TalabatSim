using Talabat.Domain.Entities;
using Talabat.Domain.Specifications;

namespace Talabat.Domain.Repositories;


public interface IGenericRepository<T> where T : BaseEntity
{

    #region Without specification
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    #endregion


    #region With specification
    Task <IReadOnlyList<T>> GetAllWithSpec(Ispecifications<T> spec);
    Task<T> GetByIdWithSpec(Ispecifications<T> spec);
    Task<int> CountWithSpecAsync(Ispecifications<T> spec);

    Task  AddAsync(T entity);
    void  UpdateAsync(T entity);
    void DeleteAsync(T entity);



    #endregion




}
