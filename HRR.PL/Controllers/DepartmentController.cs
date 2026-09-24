using AutoMapper;
using HR.BLL.ModelVM.Department;
using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using HR.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HR.PL.Controllers;

public class DepartmentController: Controller
{
    private readonly IDepartmentService dept;
    private readonly IMapper mapper;
    public DepartmentController(IDepartmentService dept, IMapper mapper)
    {
        this.dept = dept;
        this.mapper = mapper;
    }
    public IActionResult Index()
    {
        var result = dept.GetAllDepartments();
        if(result.IsHaveErrorOrNo)
            return BadRequest(result);
        return View(result.result);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(CreateDepartmentVM vm)

    {
        if (!ModelState.IsValid) return View(vm);

        var result = dept.AddDepartment(vm);
        if (result.IsHaveErrorOrNo)
        {
            ModelState.AddModelError("", result.errormessage);
            return View(vm);
        }
        return RedirectToAction("Index");
    }
    public IActionResult Edit(int id)
    {
        var result = dept.GetDepartmentById(id);
        if(result.IsHaveErrorOrNo)
        {
            ModelState.AddModelError("", result.errormessage);

            return View(result.result);
        }
        var maped = mapper.Map<EditDepartmentVM>(result.result);
        return View(maped);
    }
    [HttpPost]
    public IActionResult Edit(EditDepartmentVM vm)
    {
        var res = dept.EditDepartment(vm);
        if(res.IsHaveErrorOrNo || !ModelState.IsValid)
        {
            ModelState.AddModelError("", res.errormessage);
            return View(vm);
        }
        return RedirectToAction("Index");

    }
    public IActionResult Delete(int id)
    {
       var res = dept.DeleteDepartment(id);
        if (res.IsHaveErrorOrNo)
        {
            ModelState.AddModelError("", res.errormessage);
            return Content(res.errormessage);
        }
        return RedirectToAction("Index");
    }
}
