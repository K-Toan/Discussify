using Microsoft.AspNetCore.Identity;
using Discussify.IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace Discussify.IdentityService.Data;

public static class DbInitializer
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
    {
        if (await roleManager.Roles.AnyAsync())
        {
            Console.WriteLine("Roles already exist, skipping role seeding.");
            return;
        }
        
        var roles = new List<string>
        {
            AppUserRole.User,
            AppUserRole.Moderator,
            AppUserRole.Admin
        };

        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<int>(role));

        Console.WriteLine("Done initializing roles!");
    }
}