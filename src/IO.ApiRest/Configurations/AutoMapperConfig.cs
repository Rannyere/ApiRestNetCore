using System;
using AutoMapper;
using IO.ApiRest.DTOs;
using IO.Business.Models;

namespace IO.ApiRest.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Provider, ProviderViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => src.Provider.Name));

            CreateMap<ProductImageViewModel, Product>();
            CreateMap<Product, ProductImageViewModel>()
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => src.Provider.Name));
        }
    }
}
