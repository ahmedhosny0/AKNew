using CK.Models.TopSoft;
using CK.Repo.Area;
using CK.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CK.Controllers
{
    public class AreaController : BaseController
    {
        private readonly IAreaRepo _AreaRepo;
        private readonly SalesData _SalesData;
        public AreaController(IAreaRepo AreaRepo, SalesData salesData)
        {
            _AreaRepo = AreaRepo;
            _SalesData = salesData;
        }
        // Create
        public async Task<IActionResult> CreateAreaCode()
        {
            ViewBag.VBMaxCode = await _AreaRepo.GetMaxCode();
            ViewBag.VBZone= await _AreaRepo.GetZone();
            var data = TempData["SuccessMessage"];
            if (data != null)
            {
                TempData["SuccessMessage"] = "success";
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAreaCode(AreaCode Area)
        {
            if (Area.Code > 0)
            {
                ViewBag.VBZone = await _AreaRepo.GetZone();
                await _AreaRepo.AddAreaCodeAsync(Area);
                TempData["SuccessMessage"] = "success";
            }
             return RedirectToAction("CreateAreaCode", "Area");
        }
        // Read (Get by ID)
        [HttpGet]
        public async Task<IActionResult> EditArea(int id)
        {
            var Area = await _AreaRepo.GetAreaCodeByIdAsync(id);
            ViewBag.VBZone = await _AreaRepo.GetZone();
            TempData["SuccessMessage"] = "success";
            return View(Area);
        }
        // Read (Get all)
        [HttpGet]
        public IActionResult GetAllAreaCodes()
        {
            var Areas = _AreaRepo.GetAllAreaCodes();
            var data = TempData["SuccessMessage"];
            var check = TempData["Check"];
            if (check != null)
            {
                TempData["Check"] = "T";
            }
            if (data != null)
            {
                TempData["SuccessMessage"] = "success";
            }
            return View(Areas);
        }
        // Update
        [HttpPost]
        public async Task<IActionResult> UpdateAreaCode(int id, AreaCode Area)
        {
            try
            {
                await _AreaRepo.UpdateAreaCodeAsync(Area);
                return RedirectToAction("GetAllAreaCodes", "Area");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        public async Task<IActionResult> DeleteAreaCode(int id)
        {
            bool Check = await _AreaRepo.CheckTransactions(id);
            if (Check)
            {
                TempData["Check"] = "T";
                return RedirectToAction("GetAllAreaCodes", "Area");
            }
            try
            {
                await _AreaRepo.DeleteAreaCodeAsync(id);
                return RedirectToAction("GetAllAreaCodes", "Area");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
