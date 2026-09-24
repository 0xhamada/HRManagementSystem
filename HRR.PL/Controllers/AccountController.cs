using HR.BLL.ModelVM.AccountVM;
using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Employee> signInManager;
        private readonly IEmployeeService employeeService;
        private readonly UserManager<Employee> userManger;
        public AccountController(SignInManager<Employee> signInManager, UserManager<Employee> userManger, IEmployeeService employeeService)
        {
            this.signInManager = signInManager;
            this.userManger = userManger;
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {

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
                return RedirectToAction("Login");
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

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginEmployeeVM employee)
        {
            if(!ModelState.IsValid)
                return View(employee);
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
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
