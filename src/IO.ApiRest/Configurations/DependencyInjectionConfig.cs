using System;
using IO.Business.Interfaces;
using IO.Business.Notifications;
using IO.Business.Services;
using IO.Data.Context;
using IO.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace IO.ApiRest.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ControlDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
