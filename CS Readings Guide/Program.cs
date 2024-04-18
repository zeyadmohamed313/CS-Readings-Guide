using Core.Entites.Identity;
using CS_Readings_Guide.ErrorHandlingMiddleware;
using Microsoft.AspNetCore.Identity;
using Repository;
using Services;
using Repository.Seeding;
using Serilog;
using System.Data;
using CS_Readings_Guide.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepoDependancies()
            .AddServicesDependancies()
            .AddApiDependancies();
//Serilog Configure
Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Services.AddSerilog();



var app = builder.Build();


using (var scope = app.Services.CreateScope())// to deal with it as scoped not singleton
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedAsync(roleManager);
    await UserSeeder.SeedAsync(userManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
