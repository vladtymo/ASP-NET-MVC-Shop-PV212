using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Entities;

namespace Core.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ProductDto, Product>()
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description + "!"));

            CreateMap<Product, ProductDto>();

            CreateMap<Order, OrderDto>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(x => x.ProductsCount, opt => opt.MapFrom(src => src.Products.Count));
        }
    }
}
