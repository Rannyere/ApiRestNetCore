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
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
        }
    }
}
