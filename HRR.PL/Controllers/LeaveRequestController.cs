using HR.BLL.ModelVM.LeaveRequestVM;
using HR.BLL.Service.Abstraction;
using HR.BLL.Service.Impelementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HR.PL.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            var leavetypes = _leaveRequestService.GetAllLeaveTypes();
            ViewBag.LeaveTypes = leavetypes.result;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateLeaveRequestVM request)
        {
            if(!ModelState.IsValid)
            {
                var leavetypes = _leaveRequestService.GetAllLeaveTypes();

                ViewBag.LeaveTypes = leavetypes.result;
                return View(request);
            }
            var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _leaveRequestService.CreateRequest(employeeId ,request);
            if(result.IsHaveErrorOrNo)
            {
                ModelState.AddModelError("", result.errormessage);
                var leavetypes = _leaveRequestService.GetAllLeaveTypes();

                ViewBag.LeaveTypes = leavetypes.result;
                return View(request);
            }
            TempData["Success"] = "Leave request submitted successfully";
            return RedirectToAction(nameof(MyRequests));
        }
        public IActionResult MyRequests()
        {
           var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(employeeId == null)
            {
                return Unauthorized();
            }
           var requests = _leaveRequestService.GetMyRequests(employeeId);
            return View(requests.result);
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult PendingApprovals()
        {
            var requests = _leaveRequestService.GetPendingRequests();
            if (requests.IsHaveErrorOrNo)
                TempData["Error"] = requests.errormessage;
            return View(requests.result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Approve(int requestid)
        {
            var result = _leaveRequestService.Approve(requestid);
            if (result.IsHaveErrorOrNo)
                TempData["Error"] = result.errormessage;
            return RedirectToAction(nameof(PendingApprovals));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Reject(int requestid)
        {
            var result = _leaveRequestService.Reject(requestid);
            if (result.IsHaveErrorOrNo)
                TempData["Error"] = result.errormessage;
            return RedirectToAction(nameof(PendingApprovals));
        }
    }
}
