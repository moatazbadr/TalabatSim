using Microsoft.EntityFrameworkCore;
using Talabat.Domain.Entities;
using Talabat.Domain.Specifications;

namespace Talabat.Repository;

public static class SpecificationEvalutor<T> where T : BaseEntity
{
    //function to build the query dynamic


    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, Ispecifications<T> specifications)
    {
        var query = inputQuery;
        
        if (specifications.Criteria is not null)
                query =query.Where(specifications.Criteria);

        //adding sorting to the query
        if (specifications.OrderBy is not null)
            query = query.OrderBy(specifications.OrderBy);

        if (specifications.OrderByDescending is not null)
            query = query.OrderByDescending(specifications.OrderByDescending);


        if(specifications.IsPagingEnabled)
        {
            query = query.Skip(specifications.skip).Take(specifications.take);
        }



        if (specifications.Includes is not null)
        {
            foreach (var include in specifications.Includes)
            {
                query = query.Include(include);
            }
        }


        return query;

    }



}
