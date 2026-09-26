using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Impelementation
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserManager<Employee> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserRoleService(RoleManager<IdentityRole> roleManager, UserManager<Employee> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddRoleAsync(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            if (!await roleManager.RoleExistsAsync(roleName))
                return IdentityResult.Failed(new IdentityError { Description = "Role does not exist" });
           return await userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<List<string>> GetAllRolesAsync()
        {
            return roleManager.Roles.Select(r => r.Name!).ToList();
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return new List<string> ();
            return await userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> RemoveRoleAsync(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            return await userManager.RemoveFromRoleAsync(user, roleName);
        }
    }
}
