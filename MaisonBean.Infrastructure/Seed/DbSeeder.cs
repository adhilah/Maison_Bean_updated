using MaisonBean.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public static class DbSeeder
{
    public static async Task SeedAdminAsync(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole<int>> roleManager)
    {
        const string adminRole = "ADMIN";

        const string adminEmail =
            "admin@gmail.com";

        const string adminPassword =
            "Admin@123";

        // CREATE ROLE

        if (!await roleManager
            .RoleExistsAsync(adminRole))
        {
            await roleManager
                .CreateAsync(
                    new IdentityRole<int>
                    {
                        Name = adminRole,

                        NormalizedName =
                            adminRole.ToUpper()
                    });
        }

        // FIND ADMIN

        var admin =
            await userManager
                .FindByEmailAsync(adminEmail);

        // CREATE ADMIN

        if (admin == null)
        {
            admin = new AppUser
            {
                UserName = "admin",

                Email = adminEmail,

                EmailConfirmed = true
            };

            var result =
                await userManager
                    .CreateAsync(
                        admin,
                        adminPassword
                    );

            if (!result.Succeeded)
            {
                throw new Exception(
                    "Admin creation failed: " +

                    string.Join(
                        ", ",
                        result.Errors.Select(
                            e => e.Description
                        )
                    )
                );
            }
        }

        // ASSIGN ROLE

        var roles =
            await userManager
                .GetRolesAsync(admin);

        if (!roles.Contains(adminRole))
        {
            // REMOVE OLD ROLE

            if (roles.Contains("Admin"))
            {
                await userManager
                    .RemoveFromRoleAsync(
                        admin,
                        "Admin"
                    );
            }

            await userManager
                .AddToRoleAsync(
                    admin,
                    adminRole
                );
        }
    }
}