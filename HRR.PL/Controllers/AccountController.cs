using HR.BLL.ModelVM.AccountVM;
using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR.PL.Controllers
{
    [Authorize(Roles = "Admin,HR")]

    public class AccountController : Controller
    {
        private readonly SignInManager<Employee> signInManager;
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        private readonly UserManager<Employee> userManger;
        public AccountController(SignInManager<Employee> signInManager, UserManager<Employee> userManger, IEmployeeService employeeService, IDepartmentService departmentService)
        {
            this.signInManager = signInManager;
            this.userManger = userManger;
            this.employeeService = employeeService;
            this.departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var depts =departmentService.GetAllDepartments();
            ViewBag.Departments = depts.result;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterEmployeeVM employee)
        {
            
            if (!ModelState.IsValid)
                return View(employee);
            var result = await employeeService.RegisterEmployee(employee);
            if (result.Succeeded)
            {
                //var createduser = await userManger.FindByNameAsync(employee.UserName);
                //if(createduser != null)
                //    await signInManager.SignInAsync(createduser, false);
                return RedirectToAction("ManageUsers", "Admin");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);
                }
            }
            
            return View(employee);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginEmployeeVM employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var res = await userManger.FindByNameAsync(employee.UserName);
            if (res == null || res.IsDeleted == true)
            {
                ModelState.AddModelError("", "Invalid UserName Or Password");
                return View(employee);
            }
            var result = await signInManager.PasswordSignInAsync(employee.UserName, employee.PassWord, true, false);

            if (result.Succeeded)
            {
                // miss confirm email
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName Or Password");
            }
            return View(employee);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
