using EventSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace EventSystem.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var availableRoles = Enum.GetValues(typeof(AvailableRoles));
            var savedRoles = roleManager.Roles.ToList();

            foreach (var role in availableRoles)
            {
                var roleName = Enum.GetName(typeof(AvailableRoles), role);
                if (!savedRoles.Any(x => x.Name == roleName))
                {
                    roleManager.CreateAsync(new IdentityRole { Name = roleName }).GetAwaiter().GetResult();
                }
            }
        }
    }
}
