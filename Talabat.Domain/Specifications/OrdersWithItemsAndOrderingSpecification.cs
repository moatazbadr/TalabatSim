
using Talabat.Domain.Entities.Order;
using Talabat.Domain.Specifications.OrderSpecification;

namespace Talabat.Domain.Specifications;

public class OrdersWithItemsAndOrderingSpecification : BaseSpecifications<Order>
{
    public OrdersWithItemsAndOrderingSpecification(string Email) : base(o => o.BuyerEmail == Email)
    {
        Includes.Add(o => o.OrderItems);
        Includes.Add(o => o.DeliveryMethod);
        AddOrderByDescending(o => o.OrderDate);
    }

    public OrdersWithItemsAndOrderingSpecification(string Email ,int Id) :base(o => o.BuyerEmail == Email && o.Id ==Id)
    {
        Includes.Add(o => o.OrderItems);
        Includes.Add(o => o.DeliveryMethod);
    }



}
