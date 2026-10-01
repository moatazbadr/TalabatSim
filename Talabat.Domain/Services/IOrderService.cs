using Talabat.Domain.Entities.Order;

namespace Talabat.Domain.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress);
    Task <IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order> GetOrderByIdAsync( string buyerEmail, int Orderid);
}
