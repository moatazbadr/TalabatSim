namespace Talabat.APIs.DTOs;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal price { get; set; }
}