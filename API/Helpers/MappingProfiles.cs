using API.DTOs;
using AutoMapper;
using Core.Entities;
namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(pDto => pDto.ProductType, options => options.MapFrom(p => p.ProductType.Name))
                .ForMember(pDto => pDto.ProductBrand, options => options.MapFrom(p => p.ProductBrand.Name))
                .ForMember(pDto => pDto.PictureUrl, options => options.MapFrom<ProductUrlResolver>());
        }
    }
}
