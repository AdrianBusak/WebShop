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
            // User
            CreateMap<User, UserLoginVM>().ReverseMap();
            CreateMap<User, UserRegisterVM>().ReverseMap();
            CreateMap<User, UserProfileVM>().ReverseMap();
            CreateMap<User, UserEditProfileVM>().ReverseMap();

            // Product → ProductResponseVM
            CreateMap<Product, ProductResponseVM>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CountryNames, opt => opt.MapFrom(src => src.ProductCountries.Select(pc => pc.Country.Name)));

            CreateMap<Product, ProductUserVM>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.Content)));


            // Product ↔ ProductCreateVM
            CreateMap<ProductCreateVM, Product>()
                .ForMember(dest => dest.ProductCountries, opt => opt.MapFrom(src =>
                    src.SelectedCountriesIds.Select(id => new ProductCountry { CountryId = id })
                ));

            // Product ↔ ProductEditVM
            CreateMap<Product, ProductEditVM>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.SelectedCountriesIds, opt => opt.MapFrom(src => src.ProductCountries.Select(pc => pc.CountryId)));

            CreateMap<ProductEditVM, Product>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            // Product → ProductDetailsVM
            CreateMap<Product, ProductDetailsVM>()
                .ForMember(dest => dest.CountryNames, opt => opt.MapFrom(src => src.ProductCountries.Select(pc => pc.Country.Name)))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.Content)));

            // Category
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Category, CategoryCreateVM>().ReverseMap();
            CreateMap<Category, CategoryEditVM>().ReverseMap();

            // Country
            CreateMap<Country, CountryVM>().ReverseMap();
            CreateMap<Country, CountryEditVM>().ReverseMap();
            CreateMap<Country, CountryCreateVM>().ReverseMap();

            // CartItem
            CreateMap<CartItem, CartItemVM>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : "Nepoznat proizvod"))
                 .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
                     src.Product != null && src.Product.Images != null && src.Product.Images.Any()
                         ? src.Product.Images.First().Content
                         : string.Empty
                 ))
                 .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                 .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product != null ? src.Product.Price : 0))
                 .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            //Cart
            CreateMap<Cart, CartVM>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(
                    src => src.CartItems
                              .Where(ci => ci.Product != null)
                              .Sum(ci => ci.Product.Price * ci.Quantity)
                ));
        }
    }
}
