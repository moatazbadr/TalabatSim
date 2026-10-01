using Talabat.Domain.Entities.Order;

namespace Talabat.APIs.DTOs;

public class OrderToReturnDto
{
    public int Id { get; set; }
    public string BuyerEmail { get; set; } = string.Empty;
    public DateTimeOffset OrderDate { get; set; }
    public string Status { get; set; }
    public Address ShippingAddress { get; set; } = new Address();
    public ICollection<OrderItemDto> OrderItems { get; set; } = new HashSet<OrderItemDto>();
    public string DeliveryMethod { get; set; }
    public decimal DeliveryMethodCost { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }


    public string PaymentIntentId { get; set; }
}
