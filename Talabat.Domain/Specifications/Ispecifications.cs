using System.Linq.Expressions;
using Talabat.Domain.Entities;

namespace Talabat.Domain.Specifications;



public interface Ispecifications<T> where T : BaseEntity
{
    //_dbContext.products.where(p=>p.id==id).include(p=>p.productType).include(p=>p.productBrand); 


    //1- signature for the where condition [ where(p=>p.id==id) ]
        public Expression<Func<T,bool>> Criteria { get; set; } //  translates to where(p=>p.id==id) 


    //2- signature for the List of Includes [ include(p=>p.productType).include(p=>p.productBrand) ]
    public List<Expression<Func<T,object>>> Includes { get; set; }

    //3- signature for the sorting [ orderBy(p=>p.Price) or orderByDescending(p=>p.Price) ]
    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDescending { get; set; }

    public int skip{ get; set; }
    public int take{ get; set; }

    public bool IsPagingEnabled { get; set; }
}
