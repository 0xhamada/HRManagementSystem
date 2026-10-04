using AutoMapper;
using HR.BLL.ModelVM.Employee;
using HR.BLL.Service.Abstraction;
using HR.BLL.Service.Impelementation;
using HR.DAL.Entities;
using HR.DAL.Repo.Abstraction;
using HR.PL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR.PL.Controllers;

[Authorize(Roles = "Admin,HR")]
public class EmployeeController : Controller
{
    private readonly IEmployeeService employeeService;
    private readonly IMapper mapper;
    private readonly IEmailService emailService;
    public EmployeeController(IEmployeeService employeeService, IMapper mapper, IEmailService emailService)
    {
        this.employeeService = employeeService;
        this.mapper = mapper;
        this.emailService = emailService;
    }
    public IActionResult Index()
    {
        var res = employeeService.GetActiveEmployee();
        if (res.IsHaveErrorOrNo)
            return Content(res.errormessage);
        return View(res.result);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeVM employeeVm)
    {
        if (!ModelState.IsValid) return View(employeeVm);
        string? imageName = null;
        if (employeeVm.Image != null)
        {
            imageName = Upload.UploadFile(employeeVm.Image, "Images");
        }
        var result =await employeeService.AddEmployee(employeeVm, imageName);
        if (result.IsHaveErrorOrNo)
        {
            ModelState.AddModelError("", result.errormessage);
            return View(employeeVm);
        }
        // Generate password reset token
        var token = await employeeService.GenerateResetTokenAsync(employeeVm.Email);

        if (token == null)
        {
            ModelState.AddModelError(
                "",
                "Could not generate password reset token.");

            return View(employeeVm);
        }
        // Create reset password URL
        var resetLink = Url.Action(
            "ResetPassword",
            "Account",
            new
            {
                email = employeeVm.Email,
                token = token
            },
            Request.Scheme);

        // Send email
        var subject = "Set Your Password";

        var body = $"""
        Hello {employeeVm.Name},

        Your employee account has been created.

        Please click the link below to set your password:

        {resetLink}

        This link allows you to create your password.

        Thank you.
        """;

        await emailService.SendEmailAsync(
            employeeVm.Email,
            subject,
            body);

        return RedirectToAction("Index");
    }
    public IActionResult Edit(string id)
    {
        var result = employeeService.GetEmployeeById(id);
        if (result.IsHaveErrorOrNo || result.result == null)
        {
            return NotFound();
        }

        var maped = mapper.Map<EditEmployeeVM>(result.result);

        return View(maped);
    }
    [HttpPost]
    public IActionResult Edit(EditEmployeeVM vm)
    {
        if (!ModelState.IsValid) return View(vm);
        string? imageName = null;
        if (vm.NewImage != null)
        {
            imageName = Upload.UploadFile(vm.NewImage, "Images");
        }
        var res = employeeService.EditEmployee(vm, imageName);
        if (res.IsHaveErrorOrNo)
        {
            ModelState.AddModelError("", res.errormessage);
            return View(vm);
        }
        return RedirectToAction("Index");

    }
    public IActionResult Delete(string id)
    {
        var deleted = employeeService.DeleteEmployee(id);

        if (deleted.IsHaveErrorOrNo)
        {
            return View();
        }
        return RedirectToAction("Index");
    }
}
