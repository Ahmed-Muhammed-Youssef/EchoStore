using API.DTOs;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class OrderUrlResolver : IValueResolver<OrderedProductInfo, OrderedProductInfoDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderedProductInfo source, OrderedProductInfoDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductInfo.PictureUrl))
            {
                return _configuration["ApiUrl"] + source.ProductInfo.PictureUrl;
            }
            return string.Empty;
        }
    }
}
