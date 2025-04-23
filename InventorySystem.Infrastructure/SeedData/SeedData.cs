
using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Infrastructure.SeedData
{
    public static class SeedData
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User", "Manager" };
            foreach (var rolename in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(rolename))
                {
                    await roleManager.CreateAsync(new IdentityRole(rolename));
                }
            }
        }
    }
}
