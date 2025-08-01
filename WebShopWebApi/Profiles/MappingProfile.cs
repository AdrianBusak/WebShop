﻿using AutoMapper;
using WebShop.DAL.Models;
using WebShopWebApi.DTOs;

namespace WebShopWebApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponseDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductDetailDto>().ReverseMap();


            CreateMap<Image, ImageResponseDto>().ReverseMap();
            CreateMap<Image, ImageCreateDto>().ReverseMap();
            CreateMap<Image, ImageUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryResponseDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<Country, CountryResponseDto>().ReverseMap();
            CreateMap<Country, CountryCreateDto>().ReverseMap();
            CreateMap<Country, CountryUpdateDto>().ReverseMap();

            CreateMap<ProductCountry, CountryProductDto>().ReverseMap();

            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();

            CreateMap<Cart, CartResponseDto>().ReverseMap();
            
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<CartItem, CartResponseDto>().ReverseMap();
            CreateMap<CartItem, CartItemCreateDto>().ReverseMap();
            CreateMap<CartItem, CartItemUpdateDto>().ReverseMap();

            CreateMap<Log, LogResponseDto>().ReverseMap();
            CreateMap<Log, LogCreateDto>().ReverseMap();
        }
    }
}
