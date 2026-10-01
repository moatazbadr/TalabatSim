using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat.Domain.Entities.Order;

public class Order : BaseEntity
{
    public Order(string buyerEmail, Address shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subtotal)
    {
        BuyerEmail = buyerEmail;
        ShippingAddress = shippingAddress;
        OrderItems = orderItems;
        DeliveryMethod = deliveryMethod;
        Subtotal = subtotal;
    }
    public Order()
    {
        
    } 

    public string BuyerEmail { get; set; } = string.Empty;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public Address ShippingAddress { get; set; } = new Address();
    public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    public DeliveryMethod DeliveryMethod { get; set; } = new DeliveryMethod();

    public decimal Subtotal { get; set; }
    [NotMapped]
    public decimal Total => Subtotal + DeliveryMethod.Cost;


    public string PaymentIntentId { get; set; } = string.Empty;


}
