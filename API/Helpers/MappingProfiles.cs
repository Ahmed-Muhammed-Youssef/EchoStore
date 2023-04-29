﻿using API.DTOs;
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

        }
    }
}
