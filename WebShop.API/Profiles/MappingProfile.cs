using AutoMapper;
using WebShop.API.DTOs;
using WebShop.API.Models;

namespace WebShop.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
