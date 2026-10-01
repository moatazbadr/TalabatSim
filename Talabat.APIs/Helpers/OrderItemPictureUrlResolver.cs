using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Domain.Entities.Order;

namespace Talabat.APIs.Helpers;

public class OrderItemPictureUrlResolver :IValueResolver<OrderItem, OrderItemDto, string>
{
    private readonly IConfiguration _config;
    public OrderItemPictureUrlResolver(IConfiguration config)
    {
        _config = config;
    }
    public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
        {
            return _config["ApiBaseUrl"] + source.ItemOrdered.PictureUrl;
        }
        return string.Empty;
    }


}
