using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Abstraction
{
    public interface IUserRoleService
    {
        Task<IdentityResult> AddRoleAsync(string userId, string roleName);
        Task<IdentityResult> RemoveRoleAsync(string userId, string roleName);
        Task<IList<string>> GetUserRolesAsync(string userId);
        Task<List<string>> GetAllRolesAsync();
    }
}