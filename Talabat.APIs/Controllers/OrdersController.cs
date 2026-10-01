using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Domain.Entities.Identity;
using Talabat.Domain.Entities.Order;
using Talabat.Domain.Services;

namespace Talabat.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    //1-Create Order Endpoint 
    
    [HttpPost]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(OrderToReturnDto),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderCreateDto orderCreateDto)
    {
        var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
        var MappedAddress= _mapper.Map<AddressDto, Address>(orderCreateDto.shippingAddress);

        var Order = await _orderService.CreateOrderAsync(buyerEmail: BuyerEmail, deliveryMethodId: orderCreateDto.deliveryMethodId, shippingAddress: MappedAddress, basketId: orderCreateDto.BasketId);
         if (Order == null) return BadRequest(new ApiResponse(400 ,"Problem Creating Order"));
        
        var MappedOrderToReturnDto = _mapper.Map<Order, OrderToReturnDto>(Order);

        return Ok(MappedOrderToReturnDto);




    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(statusCode:200,Type = typeof(IReadOnlyList<OrderToReturnDto>))]
    [ProducesResponseType(typeof (ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
    {
        var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
        var Orders = await _orderService.GetOrdersForUserAsync(BuyerEmail);
        if (Orders == null || Orders.Count == 0) return NotFound(new ApiResponse(404, "No Orders Found for this user"));
        var MappedOrderToReturnDto = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(Orders);

        return Ok(MappedOrderToReturnDto);
    }

    [HttpGet("{Id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForAUser(int Id) 
    { 
        var BuyerEmail =User.FindFirstValue(claimType:ClaimTypes.Email);
        var OrderToReturn = await _orderService.GetOrderByIdAsync(BuyerEmail, Id);
        if (OrderToReturn is null)
        {
            return NotFound(new ApiResponse(404, "No Order Found for this user"));
        }
        var MappedOrderToReturnDto = _mapper.Map<Order, OrderToReturnDto>(OrderToReturn);
        return Ok(MappedOrderToReturnDto);
    
    
    }




}
