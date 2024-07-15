using AutoMapper;
using ShopMvcApp_PV212.Dtos;
using ShopMvcApp_PV212.Entities;

namespace ShopMvcApp_PV212.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap()
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description + "!"));
        }
    }
}
