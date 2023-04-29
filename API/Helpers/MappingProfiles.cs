using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductInfo, ProductInfoDto>()
                .ForMember(pDto => pDto.ProductType, options => options.MapFrom(p => p.ProductType.Name))
                .ForMember(pDto => pDto.ProductBrand, options => options.MapFrom(p => p.ProductBrand.Name))
                .ForMember(pDto => pDto.PictureUrl, options => options.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CartDto, Cart>();
            CreateMap<CartItemDto, CartItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Core.Entities.OrderAggregate.Order, OrderToReturnDto>()
                .ForMember(otr => otr.DeliveryMethod, c => c.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(otr => otr.ShippingPrice, c => c.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<Core.Entities.OrderAggregate.OrderedProductInfo, OrderedProductInfoDto>()
                .ForMember(opi => opi.Name, c => c.MapFrom(s => s.ProductInfo.Name))
                .ForMember(opi => opi.Price, c => c.MapFrom(s => s.ProductInfo.Price))
                .ForMember(opi => opi.PictureUrl, c => c.MapFrom(s => s.ProductInfo.PictureUrl));

        }
    }
}
