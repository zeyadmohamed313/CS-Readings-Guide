using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Seeding
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> _roleManager)
        {
            var rolesCount = _roleManager.Roles.Count();
            if (rolesCount <= 0)
            {

                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "User"
                });
            }
        }
    }
}
