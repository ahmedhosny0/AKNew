using CK.DTO;
using CK.Models.TopSoft;
using CK.Repo.Street;
using CK.Repo.Street;
using CK.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace CK.Controllers
{
    public class StreetController : BaseController
    {
        private readonly IStreetRepo _StreetRepo;
        private readonly SalesData _SalesData;
        public StreetController(IStreetRepo StreetRepo, SalesData salesData)
        {
            _StreetRepo = StreetRepo;
            _SalesData = salesData;
        }
        [HttpGet]
        public async Task<JsonResult> GetFilteredArea(int Zone)
        {
            var data = await _StreetRepo.GetArea(Zone);
            return Json(data);
        }
        // Create
        public async Task<IActionResult> CreateStreetCode()
        {
            ViewBag.VBZone = await _StreetRepo.GetZone();
            ViewBag.VBMaxCode = await _StreetRepo.GetMaxCode();
            ViewBag.VBBranch = await _StreetRepo.GetBranch();
            var data = TempData["SuccessMessage"];
            if (data != null)
            {
                TempData["SuccessMessage"] = "success";
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateStreetCode(StreetCode Street)
        {
            if (Street.Code > 0)
            {
                await _StreetRepo.AddStreetCodeAsync(Street);
                TempData["SuccessMessage"] = "success";
            }
            return RedirectToAction("CreateStreetCode", "Street");
        }
        // Read (Get by ID)
        [HttpGet]
        public async Task<IActionResult> EditStreet(int id)
        {
            var Street = await _StreetRepo.GetStreetCodeByIdAsync(id);
            ViewBag.VBZone = await _StreetRepo.GetZone();
            ViewBag.VBMaxCode = await _StreetRepo.GetMaxCode();
            ViewBag.VBBranch = await _StreetRepo.GetBranch();
            int Zone = Convert.ToInt32(Street.ZoneSerial); // Assuming `Header` contains the Customer
            ViewBag.VBArea = await _StreetRepo.GetArea(Zone);
            return View(Street);
        }
        // Read (Get all)
        [HttpGet]
        public async Task<IActionResult> GetAllStreetCodes()
        {
            List<SalesOrderDTO> Streets =  _StreetRepo.GetAllStreetCodes();
            var data = TempData["SuccessMessage"];
            var check = TempData["Check"];
            if (check != null)
            {
                TempData["Check"] = "T";
            }
            if (data != null)
            {
                TempData["SuccessMessage"] = "edit";
            }
            return View(Streets);
        }
        // Update
        [HttpPost]
        public async Task<IActionResult> UpdateStreetCode(int id, StreetCode Street)
        {
            try
            {
                await _StreetRepo.UpdateStreetCodeAsync(Street);
                TempData["SuccessMessage"] = "success";
                return RedirectToAction("GetAllStreetCodes", "Street");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        public async Task<IActionResult> DeleteStreetCode(int id)
        {
            bool Check = await _StreetRepo.CheckTransactions(id);
            if (Check)
            {
                TempData["Check"] = "T";
                return RedirectToAction("GetAllStreetCodes", "Street");
            }
            try
            {
                await _StreetRepo.DeleteStreetCodeAsync(id);
                return RedirectToAction("GetAllStreetCodes", "Street");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
