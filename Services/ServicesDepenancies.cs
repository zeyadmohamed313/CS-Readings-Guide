using Core.Entites.Identity;
using Core.Generic.Repositories;
using Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Repository.Data;
using Repository.Repositories.Generic;
using Repository.Repositories;
using Core.Services;
using Services.Mapping;

namespace Services
{
    public static class ServicesDepenancies
    {
        public static IServiceCollection AddServicesDependancies(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = "127.0.0.1:6379";
            });
            services.AddHttpContextAccessor();
            services.AddScoped<IBookServices,BookServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IUserActivityServices, UserActivityServices>();
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}