using AutoMapper;
using HR.BLL.ModelVM.LeaveRequestVM;
using HR.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.PL.Controllers
{
    [Authorize(Roles = "Admin,HR")]

    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IMapper _mapper;

        public LeaveTypeController(ILeaveTypeService leaveTypeService, IMapper mapper)
        {
            _leaveTypeService = leaveTypeService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var types = _leaveTypeService.GetAll();
            return View(types.result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateLeaveTypeVM typeVM)
        {
            if(!ModelState.IsValid)
                return View();
            var result = _leaveTypeService.Create(typeVM);
            if(result.IsHaveErrorOrNo)
            {
                ModelState.AddModelError("", result.errormessage);
                return View(typeVM);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var leaveType = _leaveTypeService.GetById(id);
            if(leaveType.IsHaveErrorOrNo ||  leaveType == null)
            {
                return NotFound();
            }
            var mappedType = _mapper.Map<EditLeaveTypeVM>(leaveType.result);
            return View(mappedType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditLeaveTypeVM typeVM)
        {
            if (!ModelState.IsValid)
                return View();
            var result = _leaveTypeService.Edit(typeVM);
            if (result.IsHaveErrorOrNo)
            {
                ModelState.AddModelError("", result.errormessage);
                return View(typeVM);

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var result = _leaveTypeService.Delete(id);

            if (result.IsHaveErrorOrNo)
            {
                TempData["Error"] = result.errormessage;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
