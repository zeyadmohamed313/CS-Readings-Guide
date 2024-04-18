using Core;
using Core.Entites.Identity;
using Core.Generic.Repositories;
using Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Data;
using Repository.Repositories;
using Repository.Repositories.Generic;

namespace Repository
{
    public static class RepositoryDependancies
    {
        public static IServiceCollection AddRepoDependancies(this IServiceCollection services)
        {
            services.AddScoped<IUserActivityRepository, UserActivityRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();

            // Add services to the container.
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Data Source=DESKTOP-8QKV55J\\SQLEXPRESS;Initial Catalog=Cs Readings Guide;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // Configure Identity options if needed
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                // Add other options...
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();




            return services;
        }
    }
}