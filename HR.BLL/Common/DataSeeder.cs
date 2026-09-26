using HR.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Common
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "HR", "Employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        //public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        //{
        //    var userManager = serviceProvider.GetRequiredService<UserManager<Employee>>();

        //    string adminEmail = "admin@hr.com";
        //    string adminPassword = "Admin@123";  // change after first login!

        //    var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
        //    if (existingAdmin == null)
        //    {
        //        var admin = new Employee
        //        {
        //            UserName = adminEmail,
        //            Email = adminEmail,
        //            EmailConfirmed = true
        //        };

        //        var result = await userManager.CreateAsync(admin, adminPassword);
        //        if (result.Succeeded)
        //        {
        //            await userManager.AddToRoleAsync(admin, "Admin");
        //        }
        //    }
        //}
    }
}
