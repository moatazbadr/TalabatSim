using System.Linq.Expressions;
using Talabat.Domain.Entities;

namespace Talabat.Domain.Specifications.OrderSpecification;

public class BaseSpecifications<T> : Ispecifications<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; }  = new List<Expression<Func<T, object>>>();

    //property for sorting 
    public Expression<Func<T, object>> OrderBy { get ; set ; }
    public Expression<Func<T, object>> OrderByDescending { get ; set ; }
    public int skip { get ; set ; }
    public int take { get ; set ; }
    public bool IsPagingEnabled { get ; set; }

    public BaseSpecifications()
    {
        //Includes = new List<Expression<Func<T, object>>>();
    }
    public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
    {
        Criteria = criteriaExpression;

        
    }

    public void AddOrderBy (Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    public void AddOrderByDescending (Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }
    public void ApplyPaging(int skip, int take)
    {
        IsPagingEnabled = true;
        this.skip = skip;
        this.take = take;
    }


}
