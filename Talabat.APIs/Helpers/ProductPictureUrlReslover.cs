using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Domain.Entities;

namespace Talabat.APIs.Helpers;

public class ProductPictureUrlReslover : IValueResolver<Product, ProductToReturnDto, string>
{
    private readonly IConfiguration configuration;

    public ProductPictureUrlReslover(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return $"{configuration["ApiBaseUrl"]}{source.PictureUrl}";

        }

        return string.Empty;
        
    }
}
