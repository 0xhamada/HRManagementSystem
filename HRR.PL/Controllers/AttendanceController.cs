using HR.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HR.PL.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyAttendance()
        {
            var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var status = _attendanceService.GetTodayStatus(employeeId);
            ViewBag.Status = status.result;
            var result = _attendanceService.GetMyAttendance(employeeId);
            if(result.IsHaveErrorOrNo)
            {
                ModelState.AddModelError("", result.errormessage);
                return NotFound();
            }
            return View(result.result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckIn()
        {
            var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _attendanceService.CheckIn(employeeId);

            if (result.IsHaveErrorOrNo)
                TempData["Error"] = result.errormessage;
            else
                TempData["Success"] = "Checked in successfully";

            return RedirectToAction(nameof(MyAttendance));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut()
        {
            var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _attendanceService.CheckOut(employeeId);

            if (result.IsHaveErrorOrNo)
                TempData["Error"] = result.errormessage;
            else
                TempData["Success"] = "Checked in successfully";

            return RedirectToAction(nameof(MyAttendance));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AllAttendance()
        {
            var Attendances = _attendanceService.GetAllAttendance();
            if (Attendances.IsHaveErrorOrNo)
            {
                ModelState.AddModelError("", Attendances.errormessage);
                return NotFound();

            }
            return View(Attendances.result);
        }
    }
}
