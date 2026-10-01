namespace Talabat.Domain.Entities.Order;

public class OrderItem : BaseEntity
{
    public OrderItem(ProductItemOrdered itemOrdered, int quantity, decimal price)
    {
        ItemOrdered = itemOrdered;
        Quantity = quantity;
        this.price = price;
    }
    public OrderItem()
    {
        
    }

    public ProductItemOrdered ItemOrdered { get; set; } = new ProductItemOrdered();
    public int Quantity { get; set; }
    public decimal price { get; set; }

}
