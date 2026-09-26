using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HR.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRoleService userRoleService;
        private readonly UserManager<Employee> userManager;

        public AdminController(UserManager<Employee> userManager, IUserRoleService userRoleService)
        {
            this.userManager = userManager;
            this.userRoleService = userRoleService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ManageUsers()
        {
            var Users = userManager.Users;
            var activeUser = Users.Where(a => a.IsDeleted == false).ToList();
            var allRoles = await userRoleService.GetAllRolesAsync();
            var userRoles = new Dictionary<string, IList<string>> ();
            // we need to solve N+1 query problem here 
            /*
             this loop calls the database once per user, sequentially — N users means N separate round-trips to SQL Server, one after another. For a small user list (tens of users) this is completely fine and not worth optimizing now. But mentally file this away: if this list ever grew to hundreds/thousands of users, you'd want a single query fetching all user-role mappings at once, rather than N+1 round trips. This is called the N+1 query problem
             */
            foreach (var user in activeUser)
            {
                var roles =await userRoleService.GetUserRolesAsync(user.Id);// how do you correctly get the roles here, given async?
                userRoles[user.Id] = roles;
            }
            ViewBag.Roles = userRoles;
            ViewBag.AllRoles = allRoles;
            return View(activeUser);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string userId , string roleName)
        {
            var result = await userRoleService.AddRoleAsync(userId, roleName);
            if(!result.Succeeded)
            {
                TempData["Error"] = string.Join(", ", result.Errors.Select(e => e.Description));
            }
            return RedirectToAction(nameof(ManageUsers));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var result = await userRoleService.RemoveRoleAsync(userId, roleName);

            if (!result.Succeeded)
            {
                TempData["Error"] = string.Join(", ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(ManageUsers));
        }
    }
}
