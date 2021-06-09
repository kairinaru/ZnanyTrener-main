using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ZnanyTrener.API.Entities;

namespace ZnanyTrener.API.Others
{
    public class SeedRoles
    {
        public static async Task Seed(RoleManager<AppRole> roleManager)
        {
            var roles = new List<AppRole>
            {
                new AppRole { Name = "Admin"},
                new AppRole { Name = "Coach"},
                new AppRole { Name = "User"}
            };

            foreach(var role in roles)
            {
                 await roleManager.CreateAsync(role);
            }
        }
    }
}