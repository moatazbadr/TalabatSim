using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Domain.Entities;
using Talabat.Domain.Entities.Identity;
using Talabat.Domain.Entities.Order;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles :Profile
    {

        public MappingProfiles()
        {
            //will take a product and transform it into a productToReturnDto

            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.productType , o => o.MapFrom(s=>s.productType.Name))
                .ForMember(d => d.productBrand , o => o.MapFrom(s => s.productBrand.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlReslover>())

                ;
            CreateMap<UserAddress, UserAddressDto>().ReverseMap(); //ReverseMap() to map from AddressDto to Address
           
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))   
                ;
        }
    }
}
