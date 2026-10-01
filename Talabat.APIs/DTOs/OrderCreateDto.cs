using System.ComponentModel.DataAnnotations;
using Talabat.Domain.Entities.Identity;
using Talabat.Domain.Entities.Order;

namespace Talabat.APIs.DTOs
{
    public class OrderCreateDto
    {
        [Required]
      public  string BasketId { get; set; }
      [Required]
      public  int deliveryMethodId { get; set; }
      [Required]
      public AddressDto shippingAddress { get; set; }
    }
}
