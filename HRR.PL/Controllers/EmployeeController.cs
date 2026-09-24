using AutoMapper;
using HR.BLL.ModelVM.Employee;
using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using HR.DAL.Repo.Abstraction;
using HR.PL.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR.PL.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeService employeeService;
    private readonly IMapper mapper;
    public EmployeeController(IEmployeeService employeeService, IMapper mapper)
    {
        this.employeeService = employeeService;
        this.mapper = mapper;
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
    public IActionResult Create(CreateEmployeeVM employeeVm)
    {
        if (!ModelState.IsValid) return View(employeeVm);
        string? imageName = null;
        if (employeeVm.Image != null)
        {
            imageName = Upload.UploadFile(employeeVm.Image, "Images");
        }
        var result = employeeService.AddEmployee(employeeVm, imageName);
        if (result.IsHaveErrorOrNo)
        {
            ModelState.AddModelError("", result.errormessage);
            return View(employeeVm);
        }

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
