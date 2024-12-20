using CK.Models;
using CK.Repo.SalesOrder;
using CK.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CK.Models.TopSoft;
using CK.DTO;

namespace CK.Controllers
{
    public class SalesOrderController : BaseController
    {
        private readonly ISalesOrderRepo _SalesOrderRepo;
        private readonly SalesData _SalesData;
        public SalesOrderController(ISalesOrderRepo salesOrderRepo, SalesData salesData)
        {
            _SalesOrderRepo = salesOrderRepo;
            _SalesData = salesData;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _SalesOrderRepo.GetAllTransactions(StoreIddynamic);
            var Customers = data
    .OrderByDescending(x => DateTime.TryParse(x.CreatedDatetime, out var dt) ? dt : DateTime.MinValue)
    .Select(x => new
    {
        x.Phone1,
        x.HSalesCode,
        x.SalesStatus,
        CreatedAtTime = DateTime.TryParse(x.CreatedDatetime, out var dt) ? dt : (DateTime?)null, // Safely parse to DateTime
        x.CustomerName,
        x.ZoneName,
        x.AreaName,
        x.StreetName,
        x.ServiceCost,
        x.GrandTotal,
        x.GrandTotalWithFees
    })
    .ToList();
            if (Customers.Count > 0)
                ViewBag.Customers = Customers;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BranchReply(HsalesCode header)
        {
            _SalesOrderRepo.BranchReply(header, username);
            return RedirectToAction ("Index","SalesOrder");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSalesOrders()
        {
            List<SalesOrderDTO> salesOrders = await _SalesOrderRepo.GetAllSalesOrder(StoreIddynamic);
            var data = TempData["SuccessMessage"];
            if (data != null)
            {
                TempData["SuccessMessage"] = "Sales Order has been successfully Updated!";
            }
            return View(salesOrders);
        
        }
        [HttpGet]
        public async Task<IActionResult> GetSalesOrder(int salesCode)
        {
            var salesOrder = await _SalesOrderRepo.GetSalesOrderByCodeAsync(salesCode);
            if (salesOrder == null) return NotFound();

            return Ok(salesOrder);
        }
        //public async Task<IActionResult> DisplaySalesOrder(int id)
        //{
        //    var salesOrder = await _SalesOrderRepo.GetSalesOrderAsync(id);
        //    if (salesOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    var viewModel = new SalesOrderRequest
        //    {
        //        Header =salesOrder,
        //        Details = salesOrder.DsalesCodes.ToList()
        //    };

        //    return View(viewModel);
        //}
        [HttpGet]
        public async Task<IActionResult> EditSalesOrder(int id)
        {
            //id = 1;
            var salesOrder = await _SalesOrderRepo.GetSalesOrderAsync(id);
            int customerId = Convert.ToInt32(salesOrder.Header.CustomerCode); // Assuming `Header` contains the Customer
                   if (salesOrder == null)
            {
                return NotFound();
            }

            ViewBag.VBCustomer = await _SalesOrderRepo.GetCustomerList();
            ViewBag.VBDepartment = await _SalesOrderRepo.GetCategoryList();
            ViewBag.VBBranch = await _SalesOrderRepo.GetBranchesList(customerId);

            return View(salesOrder);
        }

        [HttpPost]
        public async Task<IActionResult> EditSalesOrder(HsalesCode header, List<DsalesCode> details)
        {
            if (ModelState.IsValid)
            {
                await _SalesOrderRepo.UpdateSalesOrderAsync(header, details);
                TempData["SuccessMessage"] = "Sales Order has been successfully updated!";
                return RedirectToAction("DisplaySalesOrder", new { id = header.SalesCode });
            }

            ViewBag.VBCustomer = await _SalesOrderRepo.GetCustomerList();
            ViewBag.VBDepartment = await _SalesOrderRepo.GetCategoryList();
            return View(header);
        }
        [HttpGet]
        public async Task<JsonResult> GetFilteredBranch(int Customer)
        {
            var data = await _SalesOrderRepo.GetBranchesList(Customer);
            return Json(data);
        }
        [HttpGet]
        public async Task<JsonResult> GetFilteredItems(string branch, string category)
        {
            var items = await _SalesOrderRepo.GetItems( branch, category);
            return Json(items);
        }
        [HttpPost]
        public async Task<JsonResult> GetPrice([FromBody] GetPriceRequest request)
        {
            try
            {
                // Use your repository method to get the price
                var data = await _SalesOrderRepo.GetPriceofItem(request.BranchCode, request.ItemId);
                return Json(data);
            }
            catch (Exception ex)
            {
                // Handle exceptions gracefully
                return Json(new { error = ex.Message });
            }
        }
        public class GetPriceRequest
        {
            public string BranchCode { get; set; }
            public string ItemId { get; set; }
        }
        public async Task<IActionResult> CreateSalesOrder()
        {
            ViewBag.VBCustomer = await _SalesOrderRepo.GetCustomerList();
            ViewBag.VBDepartment = await _SalesOrderRepo.GetCategoryList();
            ViewBag.MaxCode = await _SalesOrderRepo.GetMaxCode();
            // Initialize VBItems
            var items = await _SalesOrderRepo.GetItems("0", "0");
            ViewBag.VBItems = items?.Select(i => new SelectListItem
            {
                Value = i.ItemId.ToString(),
                Text = i.ItemName
            }).ToList() ?? new List<SelectListItem>();
            var data = TempData["SuccessMessage"];
            if (data != null)
            {
                TempData["SuccessMessage"] = "Sales Order has been successfully created!";
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSalesOrder(SalesOrderRequest request,SalesParameters salesParameters)
        {
            await _SalesOrderRepo.AddSalesOrderAsync(request.Header, request.Details,username);
            TempData["SuccessMessage"] = "Sales Order has been successfully created!";
            return RedirectToAction("CreateSalesOrder","SalesOrder");// CreatedAtAction(nameof(GetSalesOrder), new { salesCode = request.Header.SalesCode }, request.Header);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSalesOrder(int salesCode,SalesOrderRequest request)
        {
            //if (salesCode != request.Header.SalesCode) return BadRequest();

            await _SalesOrderRepo.UpdateSalesOrderAsync(request.Header, request.Details);
            TempData["SuccessMessage"] = "Sales Order has been successfully Updated!";
            return RedirectToAction("GetAllSalesOrders", "SalesOrder");
        }

        public async Task<IActionResult> DeleteSalesOrder(int id)
        {
            await _SalesOrderRepo.DeleteSalesOrderAsync(id);
            return RedirectToAction("GetAllSalesOrders", "SalesOrder");
        }
    }
}
//int customerId = Convert.ToInt32(salesOrder.Cus); // Assuming `Header` contains the Customer
