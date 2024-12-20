﻿using CK.Models.TopSoft;
using CK.Repo.Customer;
using CK.Repo.SalesOrder;
using CK.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CK.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerRepo _CustomerRepo;
        private readonly SalesData _SalesData;
        public CustomerController(ICustomerRepo CustomerRepo, SalesData salesData)
        {
            _CustomerRepo = CustomerRepo;
            _SalesData = salesData;
        }
        [HttpGet]
        public async Task<JsonResult> GetFilteredArea(int Zone)
        {
            var data = await _CustomerRepo.GetArea(Zone);
            return Json(data);
        }
        [HttpGet]
        public async Task<JsonResult> GetFilteredSteet(int Zone,int Area)
        {
            var data = await _CustomerRepo.GetStreet(Zone,Area);
            return Json(data);
        }
        // Create
        public async Task<IActionResult> CreateCustomerCode()
        {
            ViewBag.VBZone = await _CustomerRepo.GetZone();
            var data = TempData["Check"];
            if (data != null)
            {
                TempData["Check"] = "T";
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomerCode(CustomerCode customer)
        {
            ViewBag.VBZone = await _CustomerRepo.GetZone();
            bool IsExist = await _CustomerRepo.CheckExist(customer);
            if (IsExist)
            {
                TempData["Check"] = "T";
                return View();
            }
            if (customer == null)
                return BadRequest("Customer data is invalid.");

            await _CustomerRepo.AddCustomerCodeAsync(customer);
            return RedirectToAction("GetAllCustomerCodes", "Customer");
        }
        // Read (Get by ID)
        [HttpGet]
        public async Task<IActionResult> EditCustomer(int id)
        {
            var customer = await _CustomerRepo.GetCustomerCodeByIdAsync(id);
            ViewBag.VBZone = await _CustomerRepo.GetZone();
            ViewBag.VBBranch = await _CustomerRepo.GetBranch();
            int Zone = Convert.ToInt32(customer.ZoneSerial); // Assuming `Header` contains the Customer
            int Area = Convert.ToInt32(customer.AreaSerial); // Assuming `Header` contains the Customer
            ViewBag.VBArea = await _CustomerRepo.GetArea(Zone);
            ViewBag.VBStreet = await _CustomerRepo.GetStreet(Zone,Area);
            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            return View(customer);
        }
        // Read (Get all)
        [HttpGet]
        public IActionResult GetAllCustomerCodes()
        {
            var customers = _CustomerRepo.GetAllCustomerCodes();
            var data = TempData["SuccessMessage"];
            var check = TempData["Check"];
            var Dou = TempData["D"];
            if (check != null)
            {
                TempData["Check"] = "T";
            }
            if (Dou!= null)
            {
                TempData["D"] = "D";
            }
            if (data != null)
            {
                TempData["SuccessMessage"] = "edit";
            }
            return View(customers);
        }
        // Update
        [HttpPost]
        public async Task<IActionResult> UpdateCustomerCode(int id, CustomerCode customer)
        {
            bool IsExist = await _CustomerRepo.CheckExist(customer);
            if (IsExist)
            {
                TempData["D"] = "D";
                return RedirectToAction("GetAllCustomerCodes", "Customer");
            }
            try
            {
                await _CustomerRepo.UpdateCustomerCodeAsync(customer);
                TempData["SuccessMessage"] = "edit";
                return RedirectToAction("GetAllCustomerCodes", "Customer");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        public async Task<IActionResult> DeleteCustomerCode(int id)
        {
            bool Check = await _CustomerRepo.CheckTransactions(id);
            if (Check)
            {
                TempData["Check"] = "T";
                return RedirectToAction("GetAllCustomerCodes", "Customer");
            }
            try
            {
                await _CustomerRepo.DeleteCustomerCodeAsync(id);
                return RedirectToAction("GetAllCustomerCodes", "Customer");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
