using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Domain.Entities;
using Talabat.Domain.Repositories;
using Talabat.Domain.Specifications;

namespace Talabat.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _genericRepository;
    private readonly IGenericRepository<ProductBrand> _productBrandRepository;
    private readonly IGenericRepository<ProductType> _productTypeRepository;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> genericRepository, IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepository, IMapper mapper)
    {
        _mapper = mapper;
        _genericRepository = genericRepository;
        _productBrandRepository = productBrandRepository;
        _productTypeRepository = productTypeRepository;
    }

    //get all the products 
    [HttpGet("GetAllProducts")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(PaginationResponse<ProductToReturnDto>),200)]
    //values for sorting [priceAsc , priceDesc]
    public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams productSpecParams) 
    {
        var spec = new ProductWithBrandAndTypeSpecification(productSpecParams);
        var products = await _genericRepository.GetAllWithSpec(spec);
        var MappedProducts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
        var countSpec = new ProductWithFiltrationForCountAsync(productSpecParams);
        var ReturnedObject = new PaginationResponse<ProductToReturnDto>
        {
            PageIndex = productSpecParams.PageIndex,
            pageSize = productSpecParams.PageSize,
            Count = await _genericRepository.CountWithSpecAsync(countSpec),
            Data = MappedProducts
        };

        return Ok(ReturnedObject);
    }

    //get Product By Id

    [HttpGet("GetProductById/{id}")]
    [ProducesResponseType(typeof(ProductToReturnDto),200)]
    [ProducesResponseType(typeof(ApiResponse),404)]
    public async Task<ActionResult> GetProductById(int id)
    {

        var spec = new ProductWithBrandAndTypeSpecification(id);
        var product = await _genericRepository.GetByIdWithSpec(spec);
        var MappedProduct = _mapper.Map<ProductToReturnDto>(product);


        if (MappedProduct == null)
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok(MappedProduct);
    }


    //get all product types 
    [HttpGet("GetAllProductTypes")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductType>),200)]
    public async Task<IActionResult> GetAllProductTypes()
    {
        var productTypes =(IReadOnlyList<ProductType>) await _productTypeRepository.GetAllAsync();  
        return Ok(productTypes);
    }

    //get all product brands
    [HttpGet("GetAllProductBrands")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductBrand>),200)]
    public async Task<IActionResult> GetAllProductBrands()
    {
        var productBrands =(IReadOnlyList<ProductBrand>) await _productBrandRepository.GetAllAsync();
        return Ok(productBrands);
    }
    
    //get Product Type By Id
    [HttpGet("GetAllProductType/{id}")]
    [ProducesResponseType(typeof(ProductType),200)]
    [ProducesResponseType(typeof(ApiResponse),404)]
    public async Task<IActionResult> GetAllProductType(int id)
    {
        var productType = await _productTypeRepository.GetByIdAsync(id);
        if (productType == null)
        {
            return NotFound(new ApiResponse(404));
        } 
        return Ok(productType);
    }

    [HttpGet("GetAllProductBrand/{id}")]
    [ProducesResponseType(typeof(ProductBrand),200)]
    [ProducesResponseType(typeof(ApiResponse),404)]
    public async Task<IActionResult> GetAllProductBrand(int id)
    {
        var productBrand = await _productBrandRepository.GetByIdAsync(id);
        if (productBrand == null)
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok(productBrand);
    }

}
