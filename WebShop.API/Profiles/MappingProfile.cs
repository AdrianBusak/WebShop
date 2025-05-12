using AutoMapper;
using WebShop.API.DTOs;
using WebShop.API.Models;

namespace WebShop.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductResponseDto>().ReverseMap();
            CreateMap<Image, ImageResponseDto>().ReverseMap();
            CreateMap<Image, ImageCreateDto>().ReverseMap();
            CreateMap<Image, ImageUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryResponseDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
        }
    }
}
