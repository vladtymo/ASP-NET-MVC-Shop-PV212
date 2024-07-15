using AutoMapper;
using Core.Dtos;
using Data.Entities;

namespace Core.MapperProfiles
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
