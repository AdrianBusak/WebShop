using AutoMapper;
using TapNGoMVC.ViewModels;
using WebShop.DAL.Models;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserLoginVM>().ReverseMap();
            CreateMap<User, UserRegisterVM>().ReverseMap();

            CreateMap<Product, ProductResponseVM>().ReverseMap();
            CreateMap<Product, ProductResponseVM>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Product, ProductCreateVM>().ReverseMap();

            CreateMap<Product, ProductEditVM>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.SelectedCountriesIds, opt => opt.MapFrom(src => src.ProductCountries.Select(pc => pc.CountryId)))
                .ForMember(dest => dest.SelectedImagesIds, opt => opt.MapFrom(src => src.Images.Select(i => i.Id)));

            CreateMap<ProductEditVM, Product>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.ProductCountries, opt => opt.Ignore()) // ručno u kontroleru
                .ForMember(dest => dest.Images, opt => opt.Ignore()); // ručno u kontroleru

            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Category, CategoryCreateVM>().ReverseMap();
            CreateMap<Category, CategoryEditVM>().ReverseMap();
        }
    }
}
