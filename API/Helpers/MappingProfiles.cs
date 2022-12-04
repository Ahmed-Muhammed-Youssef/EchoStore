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
                .ForMember(p => p.ProductType, options => options.MapFrom(p => p.ProductType.Name))
                .ForMember(p => p.ProductBrand, options => options.MapFrom(p => p.ProductBrand.Name));
        }
    }
}
