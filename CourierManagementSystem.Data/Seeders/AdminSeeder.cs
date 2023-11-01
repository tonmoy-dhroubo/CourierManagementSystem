using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CourierManagementSystem.Data.Seeders
{
    public static class AdminSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<CustomUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Customer"));

            var user = new CustomUser
            {
                UserName = "admin",
                Email = "admin@admin.com",
                Name = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };

            var checkAdminExists = await userManager.FindByEmailAsync(user.Email);
            if (checkAdminExists == null)
            {
                await userManager.CreateAsync(user, "adminpassword");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
