using Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Seeding
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<AppUser> _userManager)
        {
            var usersCount = _userManager.Users.Count();
            if (usersCount <= 0)
            {
                var defaultuser = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FirstName = "admin",
                    LastName = "admin",
                    PhoneNumber = "123456",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultuser, "123456Aa*");
                await _userManager.AddToRoleAsync(defaultuser, "Admin");
            }
        }
    }
}
