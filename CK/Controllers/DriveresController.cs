using CK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
namespace CK.Controllers
{

    public class DriveresController : BaseController
    {
        private readonly IWebHostEnvironment _environment;

        public DriveresController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        SalesParameters Parobj = new SalesParameters();
        [HttpGet]
        public async Task<IActionResult> EditDriver(SalesParameters Parobj, int Id, string Message, string StartDate, string EndDate)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                string SQl = null;
                    SQl = @"SELECT * from Driver where Id=@Id";
                using (SqlCommand command = new SqlCommand(SQl, connection))
                {
                    connection.Open(); // Open the connection
                    //command.Parameters.Add(new SqlParameter("@Alert", Message));
                    command.Parameters.Add(new SqlParameter("@Id", Id));
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        //si.StoreName = test["alert"].ToString();
                        //ViewBag.Branches = si.StoreName;
                        if (!Convert.IsDBNull(test["StartDate"]) && !Convert.IsDBNull(test["EndDate"]))
                        {
                            DateTime startDate = Convert.ToDateTime(test["StartDate"]);
                            DateTime endDate = Convert.ToDateTime(test["EndDate"]);
                            ViewBag.SupplierName = startDate.ToString("yyyy-MM-dd");
                            ViewBag.Dmanager = endDate.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            ViewBag.SupplierName=null; ViewBag.Dmanager = null;
                        }
                        // Assigning the formatted date strings to ViewBag
                        si.ItemName = test["StartDate"].ToString();
                        ViewBag.ItemName = si.ItemName;
                        si.ItemName = test["Id"].ToString();
                        ViewBag.ItemName = si.ItemName;
                        si.Username = test["DriverName"].ToString();
                        ViewBag.DriverName = si.Username;
                        si.Fmanager = test["CardPhoto"].ToString();
                        ViewBag.Fmanager = si.Fmanager;
                        si.DpName = test["CarNumber"].ToString();
                        si.Company = test["ChassisNumber"].ToString();
                        ViewBag.Company = si.Company;
                        si.Employee = test["Motornumber"].ToString();
                        ViewBag.Employee = si.Employee;
                        string numbers = Regex.Match(si.DpName, @"\d+").Value;

                        // Extract characters (non-digits)
                        string chars = Regex.Replace(si.DpName, @"\d+", "");
                        ViewBag.numbers = numbers;
                        ViewBag.chars = chars;
                        TempData["photo"] = ViewBag.Fmanager;

                        si.Certificate = test["CarCertificate"].ToString();
                        ViewBag.Certificate = si.Certificate;
                        si.Remarks = test["Notes"].ToString();
                        ViewBag.Remarks = si.Remarks;
                        si.InsuranceCompany = test["InsuranceCompany"].ToString();
                        ViewBag.InsuranceCompany = si.InsuranceCompany;
                        si.InsuranceValue = Convert.ToDouble(test["InsuranceValue"]);
                        ViewBag.InsuranceValue = si.InsuranceValue;
                        si.InstallmentValue = Convert.ToDouble(test["InstallmentValue"]);
                        ViewBag.InstallmentValue = si.InstallmentValue;
                        si.DriverPhone = test["DriverPhone"].ToString();
                        ViewBag.DriverPhone = si.DriverPhone;
                        si.YearModel = Convert.ToInt32(test["YearModel"]);
                        ViewBag.YearModel = si.YearModel;
                        si.CarModel = test["CarModel"].ToString();
                        ViewBag.CarModel = si.CarModel;
                        si.OwnedBy = test["OwnedBy"].ToString();
                        ViewBag.OwnedBy = si.OwnedBy;
                        vi.Add(si);
                    }
                    //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
                    ViewBag.Data = vi;
                    return View();
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditDriver(SalesParameters Parobj,int id, IFormFile CardPhoto, string StartDate, string EndDate)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            if (_environment == null)
            {
                return StatusCode(500, "Environment configuration is missing.");
            }
            var x = TempData["photo"];
            // Check if CardPhoto is null
            //if (CardPhoto == null || CardPhoto.Length == 0)
            //{
            //    return BadRequest("No file uploaded.");
            //}
            string filePath=null;
            // Safely access FileName and combine paths
            if (CardPhoto !=null)
            {
                var fileName = Path.GetFileName(CardPhoto.FileName); // Extract just the file name to avoid path manipulation vulnerabilities
                 filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await CardPhoto.CopyToAsync(stream);
                }
                filePath = "/images/" + fileName; // Example path construction
            }
            else
            {
                ViewBag.x= x;
                filePath = ViewBag.x;
            }
            if(filePath==null || filePath =="")
            {
                filePath = "/images/Car.png";
            }
            Parobj.CarNumber = Parobj.CarNumberLetters + " " + Parobj.CarNumberDigits;
            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
                {
                    connection.Open(); // Open the connection
                    string Sql = @"
UPDATE Driver SET DriverName=@Driver,CardPhoto=@photo,StartDate=@Start,EndDate=@End,CarNumber=@Car,ChassisNumber=@ChassisNumber,
MotorNumber=@MotorNumber ,CarCertificate=@CarCertificate,Notes=@Notes,
InsuranceCompany=@InsuranceCompany,InsuranceValue=@InsuranceValue,InstallmentValue=@InstallmentValue,DriverPhone=@DriverPhone,YearModel=@YearModel,CarModel=@CarModel,OwnedBy=@OwnedBy
where Id=@Id
";
                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        if (Parobj.ChassisNumber == null)
                        {
                            Parobj.ChassisNumber = "Not Found";
                        }
                        if (Parobj.MotorNumber == null)
                        {
                            Parobj.MotorNumber = "Not Found";
                        }
                        if (Parobj.Certificate == null)
                        {
                            Parobj.Certificate = "Not Found";
                        }
                        if (Parobj.Remarks == null)
                        {
                            Parobj.Remarks = "Not Comment";
                        }
                        if (Parobj.InstallmentValue == null)
                        {
                            Parobj.InstallmentValue = 0;
                        }
                        if (Parobj.InsuranceValue == null)
                        {
                            Parobj.InsuranceValue = 0;
                        }
                        if (Parobj.YearModel == null)
                        {
                            Parobj.YearModel = 0;
                        }
                        if (Parobj.DriverPhone == null)
                        {
                            Parobj.DriverPhone = "Not Found";
                        }
                        //command.Parameters.Add(new SqlParameter("@Message", Message));
                        command.Parameters.Add(new SqlParameter("@Id", id));
                        command.Parameters.Add(new SqlParameter("@Start", StartDate));
                        command.Parameters.Add(new SqlParameter("@End", EndDate));
                        command.Parameters.Add(new SqlParameter("@photo", filePath));
                        command.Parameters.Add(new SqlParameter("@Driver", Parobj.DriverName));
                        command.Parameters.Add(new SqlParameter("@Car", Parobj.CarNumber));
                        command.Parameters.Add(new SqlParameter("@ChassisNumber", Parobj.ChassisNumber));
                        command.Parameters.Add(new SqlParameter("@MotorNumber", Parobj.MotorNumber));
                        command.Parameters.Add(new SqlParameter("@OwnedBy", Parobj.OwnedBy));
                        command.Parameters.Add(new SqlParameter("@CarModel", Parobj.CarModel));
                        command.Parameters.Add(new SqlParameter("@YearModel", Parobj.YearModel));
                        command.Parameters.Add(new SqlParameter("@DriverPhone", Parobj.DriverPhone));
                        command.Parameters.Add(new SqlParameter("@InsuranceValue", Parobj.InsuranceValue));
                        command.Parameters.Add(new SqlParameter("@InstallmentValue", Parobj.InstallmentValue));
                        command.Parameters.Add(new SqlParameter("@InsuranceCompany", Parobj.InsuranceCompany));
                        command.Parameters.Add(new SqlParameter("@Notes", Parobj.Remarks));
                        command.Parameters.Add(new SqlParameter("@CarCertificate", Parobj.Certificate));
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            TempData["Edit"] = "Edit";
            return RedirectToAction("CreateDriver", "Driveres");
        }

        public IActionResult DeleteDriver(SalesParameters Parobj, int Id)
        {
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("delete from Driver where Id=@Id", connection))
                {
                    connection.Open(); // Open the connection
                    command.Parameters.Add(new SqlParameter("@Id", Id));
                    var test = command.ExecuteReader();
                    TempData["Delete"] = "Delete";
                    return RedirectToAction("CreateDriver", "Driveres");
                }
            }
        }
        [HttpGet]
        public IActionResult CreateDriver()
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            var Re = TempData["Result"];
            var Del = TempData["Delete"];
            var Edit = TempData["Edit"];
            ViewBag.Re = Re;
            ViewBag.Del = Del;
            ViewBag.Edit = Edit;
            //GenerateAndSendReport();

            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                string SQL = null;
                //if (Id > 0)
                //{
                //    SQL = @"SELECT * from Driver where Id=@Id";
                //}
                //else
                //{
                    SQL = @"  select * from rptcars order by Remainingdays asc--where
 -- Remainingdays <=30 ";
               // }
                using (SqlCommand command = new SqlCommand(SQL, connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        //si.StoreName = test["alert"].ToString();
                        if (!Convert.IsDBNull(test["RemainingDays"]))
                            si.ByDay = Convert.ToInt32(test["RemainingDays"]);
                        else si.ByDay = 0;
                        si.ItemName = test["Id"].ToString();
                        si.SupplierName = test["StartDate"].ToString();
                        si.Dmanager = test["EndDate"].ToString();
                        si.Username = test["DriverName"].ToString();
                        si.Fmanager = test["CardPhoto"].ToString();
                        si.DpName = test["CarNumber"].ToString();
                        si.Company = test["ChassisNumber"].ToString();
                        si.Employee = test["Motornumber"].ToString();
                        DateTime parsedDate;
                        if (DateTime.TryParse(si.SupplierName, out parsedDate))
                        {
                            si.SupplierName = parsedDate.ToString("yyyy-MM-dd");
                        }
                        DateTime parsedDate2;
                        if (DateTime.TryParse(si.Dmanager, out parsedDate2))
                        {
                            si.Dmanager = parsedDate2.ToString("yyyy-MM-dd");
                        }
                        vi.Add(si);
                    }
                  // var orderedVi = vi.OrderByDescending(sale => sale.ByDay).ToList();
                    ViewBag.Data = vi;
                    return View("CreateDriver");
                }
            }
            return View("CreateDriver");
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriver(SalesParameters Parobj, IFormFile CardPhoto,  string StartDate, string EndDate)
        {
            
            
            
            
            
            
            
            
            
            
            
            
                        
            if (_environment == null)
            {
                return StatusCode(500, "Environment configuration is missing.");
            }

            // Check if CardPhoto is null
            //if (CardPhoto == null || CardPhoto.Length == 0)
            //{
            //    return BadRequest("No file uploaded.");
            //}

            // Safely access FileName and combine paths
            string filePath = null;
            // Safely access FileName and combine paths
            if (CardPhoto != null)
            {
                var fileName = Path.GetFileName(CardPhoto.FileName); // Extract just the file name to avoid path manipulation vulnerabilities
                filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await CardPhoto.CopyToAsync(stream);
                }
                filePath = "/images/" + fileName; // Example path construction
            }
            else
            {
                filePath = "/images/Car.png";
            }
            //var fileName = Path.GetFileName(CardPhoto.FileName); // Extract just the file name to avoid path manipulation vulnerabilities
            //string filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
            Parobj.CarNumber = Parobj.CarNumberLetters + " "+ Parobj.CarNumberDigits;
            //try
            //{
            //    using (var stream = System.IO.File.Create(filePath))
            //    {
            //        await CardPhoto.CopyToAsync(stream);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception or handle it as necessary
            //    return StatusCode(500, $"An error occurred: {ex.Message}");
            //}
            // filePath = "/images/" + fileName; // Example path construction
            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
                {
                    string SQl = @"
INSERT INTO  [Driver] (DriverName,CardPhoto,StartDate,EndDate,CarNumber,ChassisNumber,Motornumber,CarCertificate,Notes,
InsuranceCompany,InsuranceValue,InstallmentValue,DriverPhone,YearModel,CarModel,OwnedBy) 
VALUES (@Driver,@photo,@Start,@End,@Car,@ChassisNumber,@Motornumber,@CarCertificate,@Notes,
@InsuranceCompany,@InsuranceValue,@InstallmentValue,@DriverPhone,@YearModel,@CarModel,@OwnedBy)
";
                    using (SqlCommand command = new SqlCommand(SQl, connection))
                    {
                        if (Parobj.ChassisNumber ==null)
                        {
                            Parobj.ChassisNumber = "Not Found";
                        }
                        if (Parobj.MotorNumber == null)
                        {
                            Parobj.MotorNumber = "Not Found";
                        }
                        if (Parobj.Certificate == null)
                        {
                            Parobj.Certificate = "Not Found";
                        }
                        if (Parobj.Remarks == null)
                        {
                            Parobj.Remarks = "Not Comment";
                        }
                        if (Parobj.InstallmentValue == null)
                        {
                            Parobj.InstallmentValue = 0;
                        }
                        if (Parobj.InsuranceValue == null)
                        {
                            Parobj.InsuranceValue = 0;
                        }
                        if (Parobj.YearModel == null)
                        {
                            Parobj.YearModel = 0;
                        }
                        if (Parobj.DriverPhone == null)
                        {
                            Parobj.DriverPhone = "Not Found";
                        }
                        //command.Parameters.AddWithValue("@Message", Message);
                        command.Parameters.AddWithValue("@Start", StartDate);
                        command.Parameters.AddWithValue("@End", EndDate);
                        command.Parameters.AddWithValue("@photo", filePath);
                        command.Parameters.AddWithValue("@Driver", Parobj.DriverName);
                        command.Parameters.AddWithValue("@Car", Parobj.CarNumber);
                        command.Parameters.AddWithValue("@ChassisNumber", Parobj.ChassisNumber);
                        command.Parameters.AddWithValue("@Motornumber", Parobj.MotorNumber);
                        command.Parameters.AddWithValue("@CarCertificate", Parobj.Certificate);
                        command.Parameters.AddWithValue("@Notes", Parobj.Remarks);
                        command.Parameters.AddWithValue("@InsuranceCompany", Parobj.InsuranceCompany);
                        command.Parameters.AddWithValue("@InstallmentValue", Parobj.InstallmentValue);
                        command.Parameters.AddWithValue("@InsuranceValue", Parobj.InsuranceValue);
                        command.Parameters.AddWithValue("@DriverPhone", Parobj.DriverPhone); 
                        command.Parameters.AddWithValue("@YearModel", Parobj.YearModel);
                        command.Parameters.AddWithValue("@CarModel", Parobj.CarModel);
                        command.Parameters.AddWithValue("@OwnedBy", Parobj.OwnedBy);
                        connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateDriver", "Driveres");
            }
            TempData["Result"]= "done";
            return RedirectToAction("CreateDriver", "Driveres");
        }
    }
}
