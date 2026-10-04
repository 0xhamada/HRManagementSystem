using HR.BLL.ModelVM.AccountVM;
using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HR.PL.Controllers
{
    

    public class AccountController : Controller
    {
        private readonly SignInManager<Employee> signInManager;
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        private readonly UserManager<Employee> userManger;
        private readonly IEmailService emailService;
        public AccountController(SignInManager<Employee> signInManager, UserManager<Employee> userManger, IEmployeeService employeeService, IDepartmentService departmentService, IEmailService emailService)
        {
            this.signInManager = signInManager;
            this.userManger = userManger;
            this.employeeService = employeeService;
            this.departmentService = departmentService;
            this.emailService = emailService;
        }
        [Authorize(Roles = "Admin,HR")]
        [HttpGet]
        public IActionResult Register()
        {
            var depts =departmentService.GetAllDepartments();
            ViewBag.Departments = depts.result;
            return View();
        }
        [Authorize(Roles = "Admin,HR")]
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
            var result = await signInManager.PasswordSignInAsync(employee.UserName, employee.PassWord, isPersistent: false, lockoutOnFailure: true);
            if(result.IsLockedOut)
            {
                ModelState.AddModelError("", "This account is temporarily locked. Please try again later.");
                return View(employee);
            }
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
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            var user = await userManger.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                return NotFound();
            }
            var token = await userManger.GeneratePasswordResetTokenAsync(user);
            if (token == null)
            {
                ModelState.AddModelError(
                    "",
                    "Could not generate password reset token.");

                return View(vm);
            }
            var resetLink = Url.Action("ResetPassword", "Account", new { email = vm.Email, token = token }, Request.Scheme);
            var subject = "Reset Your Password";
            var body = $"""
                        Hello {user.Name},

                        We received a request to reset your password.

                        Click the link below to reset your password:

                        $"<a href='{resetLink}'>Reset My Password</a><br><br>" 

                        If you did not request a password reset, you can ignore this email.

                        Thank you.
                        """;
            await emailService.SendEmailAsync(user.Email, subject, body);
            TempData["Success"] = "If that email is registered, a reset link has been sent.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            if (string.IsNullOrEmpty(email) ||string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            var model = new ResetPasswordVM
            {
                Email = email,
                Token = token
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManger.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("","Invalid password reset request.");
                return View(model);
            }

            var result = await userManger.ResetPasswordAsync(
                user,
                model.Token,
                model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        "",
                        error.Description);
                }
                return View(model);
            }
            if (!user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                await userManger.UpdateAsync(user);
            }

            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword() => View();


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM vm)
        {
            if(!ModelState.IsValid)
                return View(vm);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var user =await userManger.FindByIdAsync(userId);
            if(user == null)
            {
                return NotFound();
            }
            var result = await userManger.ChangePasswordAsync(user, vm.CurrentPassword, vm.NewPassword);
            if(!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View(vm);
                }
            }
            TempData["Success"] = "Password changed successfully.";
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
