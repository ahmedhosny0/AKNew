using CK.Models.TopSoft;
using CK.Repo.Zone;
using CK.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CK.Controllers
{
    public class ZoneController : BaseController
    {
        private readonly IZoneRepo _ZoneRepo;
        private readonly SalesData _SalesData;
        public ZoneController(IZoneRepo ZoneRepo, SalesData salesData)
        {
            _ZoneRepo = ZoneRepo;
            _SalesData = salesData;
        }
        // Create
        public async Task<IActionResult> CreateZoneCode()
        {
            ViewBag.VBMaxCode = await _ZoneRepo.GetMaxCode();
            var data = TempData["SuccessMessage"];
            if (data != null)
            {
                TempData["SuccessMessage"] = "success";
            }
            return View("CreateZoneCode");
        }
        [HttpPost]
        public async Task<IActionResult> CreateZoneCode(ZoneCode Zone)
        {
            if(Zone.Code >0) {
                await _ZoneRepo.AddZoneCodeAsync(Zone);
                TempData["SuccessMessage"] = "success";
            }
            return RedirectToAction ("CreateZoneCode", "Zone");
        }
        // Read (Get by ID)
        [HttpGet]
        public async Task<IActionResult> EditZone(int id)
        {
            var Zone = await _ZoneRepo.GetZoneCodeByIdAsync(id);
            TempData["SuccessMessage"] = "success";
            return View(Zone);
        }
        // Read (Get all)
        [HttpGet]
        public IActionResult GetAllZoneCodes()
        {
            var Zones = _ZoneRepo.GetAllZoneCodes();
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
            return View(Zones);
        }
        // Update
        [HttpPost]
        public async Task<IActionResult> UpdateZoneCode(int id, ZoneCode Zone)
        {
            try
            {
                await _ZoneRepo.UpdateZoneCodeAsync(Zone);
                return RedirectToAction("GetAllZoneCodes", "Zone");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        public async Task<IActionResult> DeleteZoneCode(int id)
        {
            bool Check = await _ZoneRepo.CheckTransactions(id);
            if (Check)
            {
                TempData["Check"] = "T";
                return RedirectToAction("GetAllZoneCodes", "Zone");
            }
            try
            {
                await _ZoneRepo.DeleteZoneCodeAsync(id);
                return RedirectToAction("GetAllZoneCodes", "Zone");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
