using HR.BLL.ModelVM.ResignationVM;
using HR.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HR.PL.Controllers
{
    [Authorize]
    public class ResignationController : Controller
    {
        private readonly IResignationService _resignationService;

        public ResignationController(IResignationService resignationService)
        {
            _resignationService = resignationService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create() => View();
        
        [HttpPost]
        public IActionResult Create(CreateResignationVM resignation)
        {
            if(!ModelState.IsValid)
                return View(resignation);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _resignationService.Submit(userId, resignation);
            if (result.IsHaveErrorOrNo)
            {
                ModelState.AddModelError("", result.errormessage);
                return View(resignation);
            }
            TempData["Success"] = "Resignation submitted. Awaiting Admin review.";
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
         var result =  await _resignationService.Approve(id);
            if (result.IsHaveErrorOrNo)
                TempData["Error"] = result.errormessage;
            return RedirectToAction(nameof(GetPinding));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            var result = await _resignationService.Reject(id);
            if (result.IsHaveErrorOrNo)
                TempData["Error"] = result.errormessage;
            return RedirectToAction(nameof(GetPinding));
        }
        [Authorize(Roles ="Admin")]
        public IActionResult GetPinding()
        {
            var result = _resignationService.GetPending();
            if(result.IsHaveErrorOrNo)
            {
                ModelState.AddModelError("", result.errormessage);
                return View();
            }

            return View(result.result);
        }
    }
}
