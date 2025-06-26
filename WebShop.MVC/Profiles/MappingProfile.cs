using AutoMapper;
using TapNGoMVC.ViewModels;
using WebShop.DAL.Models;

namespace WebShop.MVC.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserLoginVM>().ReverseMap();
            CreateMap<User, UserRegisterVM>().ReverseMap();
        }
    }
}
