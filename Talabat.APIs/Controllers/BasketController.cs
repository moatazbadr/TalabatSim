using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Domain.Entities;
using Talabat.Domain.Repositories;

namespace Talabat.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public BasketController(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    //Get the Basket by Id this will also act like a Refresh for the Basket if it exists in the database
    [HttpGet("{basketId}")]  
    public async Task<ActionResult<CustomerBasket?>> GetBasketAsync(string basketId)
    {
        var basket = await _basketRepository.GetBasketAsync(basketId);
        if (basket == null)
            return new CustomerBasket(basketId);
        return Ok(basket);


    }


    //you can use this endpoint to update the basket by sending the basket object in the request body
    [HttpPost]
    public async Task< ActionResult<CustomerBasket?>> UpdateBasketAsync(CustomerBasketDto basket)
    {
        var MappedBasket = _mapper.Map<CustomerBasket>(basket);
        var updatedBasket = await _basketRepository.UpdateBasketAsync(MappedBasket);
        if (updatedBasket == null)
            return BadRequest(new ApiResponse(400,"Problem updating the basket"));
        return Ok(updatedBasket);
    }

    [HttpDelete("{basketId}")]
    public async Task<ActionResult> DeleteBasketAsync(string basketId)
    {
        var deleted = await _basketRepository.DeleteBasketAsync(basketId);
        if (!deleted)
            return BadRequest(new ApiResponse(400, "Problem deleting the basket"));
        return Ok();
    }



}
