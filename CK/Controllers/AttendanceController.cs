using CK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MimeKit;

namespace CK.Controllers
{
    public class AttendanceController : BaseController
    {
        SalesParameters Parobj = new SalesParameters();
        [HttpGet]
        public IActionResult EditManager(SalesParameters Parobj, int Id, string Message, string StartDate, string EndDate)
        {
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("SELECT * from Manager where Id=@Id ", connection))
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
                        DateTime startDate = Convert.ToDateTime(test["StartDate"]);
                        DateTime endDate = Convert.ToDateTime(test["EndDate"]);

                        // Assigning the formatted date strings to ViewBag
                        ViewBag.SupplierName = startDate.ToString("yyyy-MM-dd");
                        ViewBag.Dmanager = endDate.ToString("yyyy-MM-dd");
                        vi.Add(si);
                    }
                    //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
                    ViewBag.Data = vi;
                    return View();
                }
            }
        }
        [HttpPost]
        public IActionResult EditManager(int itemId, SalesParameters Parobj, string Message, int id, string StartDate, string EndDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
                {
                    connection.Open(); // Open the connection

                    using (SqlCommand command = new SqlCommand("UPDATE Manager SET StartDate = @StartDate ,EndDate=@EndDate where Id=@Id", connection))
                    {
                        //command.Parameters.Add(new SqlParameter("@Message", Message));
                        command.Parameters.Add(new SqlParameter("@Id", id));
                        command.Parameters.Add(new SqlParameter("@StartDate", StartDate));
                        command.Parameters.Add(new SqlParameter("@EndDate", EndDate));
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                return View();
            }

            return RedirectToAction("CreateManager", "Attendance");
        }

        public IActionResult DeleteManager(SalesParameters Parobj, int Id)
        {
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("delete from ManagerCode where ManagerSerial=@Id", connection))
                {
                    connection.Open(); // Open the connection
                    command.Parameters.Add(new SqlParameter("@Id", Id));
                    var test = command.ExecuteReader();
                    return RedirectToAction("CreateManager", "Attendance");
                }
            }
        }
        public IActionResult DeleteEmployee(SalesParameters Parobj, int Id)
        {
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("delete from [EmployeeCode] where EmployeeSerial=@Id", connection))
                {
                    connection.Open(); // Open the connection
                    command.Parameters.Add(new SqlParameter("@Id", Id));
                    var test = command.ExecuteReader();
                    return RedirectToAction("CreateEmployee", "Attendance");
                }
            }
        }
        [HttpGet]
        public IActionResult CreateManager()
        {
            
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("SELECT *,(select max(ManagerId)ManagerId from [TopSoft].[dbo].[ManagerCode])ManagerId FROM RoleCode ", connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.RoleId = Convert.ToInt16(test["RoleSerial"]);
                        si.RoleName = test["RoleName"].ToString();
                        si.ManagerId = Convert.ToInt32(test["ManagerId"]);
                        vi.Add(si);
                    }
                   var x = vi.Select(x => x.ManagerId + 1).Distinct().ToList();
                    ViewBag.MaxId = x[0];
                    TempData["MaxId"] = x[0];
                    ViewBag.Data = vi.GroupBy(m => m.RoleName)
.Select(group => new { RoleId = group.First().RoleId , RoleName = group.Key })
.OrderBy(m => m.RoleName)
.ToList();
                }
            }

            //using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            //{
            //    using (SqlCommand command = new SqlCommand("SELECT * FROM ManagerCode ", connection))
            //    {
            //        connection.Open(); // Open the connection
            //        double TotalSales = 0;
            //        var vi = new List<RptSale>();
            //        var test = command.ExecuteReader();
            //        while (test.Read())
            //        {
            //            RptSale si = new RptSale();
            //            si.ManagerId = Convert.ToInt16(test["ManagerSerial"]);
            //            si.ManagerName = test["ManagerName"].ToString();
            //            vi.Add(si);
            //        }
            //        //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
            //        ViewBag.Data = vi;
            //        return View("CreateManager");
            //    }
            //}
            return View("CreateManager");
        }
        [HttpPost]
        public async Task<IActionResult> CreateManager(SalesParameters Parobj)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            if (Parobj.ManagerId ==0)
            {
                Parobj.ManagerId = 1;
            }
            var Manager = TempData["MaxId"];
            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO  ManagerCode (ManagerName,ManagerId,RoleCode) VALUES (@ManagerName,@ManagerId,@RoleCode)", connection))
                    {

                        //command.Parameters.AddWithValue("@Message", Message);
                        command.Parameters.AddWithValue("@ManagerName", Parobj.ManagerName);
                        command.Parameters.AddWithValue("@ManagerId", Manager);
                        command.Parameters.AddWithValue("@RoleCode", Parobj.RoleName);
                        connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateManager", "Attendance");
            }
            return RedirectToAction("CreateManager", "Attendance");
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM RoleCode ", connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.RoleId = Convert.ToInt16(test["RoleSerial"]);
                        si.RoleName = test["RoleName"].ToString();
                        vi.Add(si);
                    }
                    //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
                    ViewBag.Data = vi;
                    return View("CreateRole");
                }
            }
            return View("CreateRole");
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(SalesParameters Parobj)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO  RoleCode (RoleName) VALUES (@RoleName)", connection))
                    {

                        //command.Parameters.AddWithValue("@Message", Message);
                        command.Parameters.AddWithValue("@RoleName", Parobj.RoleName);
                        connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateRole", "Attendance");
            }
            return RedirectToAction("CreateRole", "Attendance");
        }
        [HttpGet]
        public IActionResult CreateModelType()
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM ModelTypeCode ", connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        if (!Convert.IsDBNull(test["ModelTypeSerial"]))
                            si.ModelTypeId = Convert.ToInt32(test["ModelTypeSerial"]);
                        else
                            si.ModelTypeId = 0;
                        si.ModelTypeName = test["ModelName"].ToString();
                        vi.Add(si);
                    }
                    //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
                    ViewBag.Data = vi;
                    return View("CreateModelType");
                }
            }
            return View("CreateModelType");
        }
        [HttpPost]
        public async Task<IActionResult> CreateModelType(SalesParameters Parobj)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO  ModelTypeCode (ModelName) VALUES (@ModelName)", connection))
                    {

                        //command.Parameters.AddWithValue("@Message", Message);
                        command.Parameters.AddWithValue("@ModelName", Parobj.ModelTypeName);
                        connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateModelType", "Attendance");
            }
            return RedirectToAction("CreateModelType", "Attendance");
        }
        public IActionResult DeleteRole(SalesParameters Parobj, int Id)
        {
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("delete from RoleCode where RoleSerial=@Id", connection))
                {
                    connection.Open(); // Open the connection
                    command.Parameters.Add(new SqlParameter("@Id", Id));
                    var test = command.ExecuteReader();
                    return RedirectToAction("CreateRole", "Attendance");
                }
            }
        }
        [HttpGet]
        public IActionResult CreateEmployee()
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("select max(EmployeeId)EmployeeId from [TopSoft].[dbo].[EmployeeCode] ", connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi2 = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        if (!Convert.IsDBNull(test["EmployeeId"]))
                            si.ManagerId = Convert.ToInt32(test["EmployeeId"]);
                        else
                            si.ManagerId = 0;
                        vi2.Add(si);
                    }
                    var x = vi2.Select(x => x.ManagerId + 1).Distinct().ToList();
                    ViewBag.MaxId = x[0];
                    TempData["MaxId"] = x[0];
                }
            }
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM EmployeeCode ", connection))
                {
                    connection.Open(); // Open the connection
                    var vi3 = new List<RptSale>();
                    var test2 = command.ExecuteReader();
                    while (test2.Read())
                    {
                        RptSale si = new RptSale();
                        si.EmplyeeCode = Convert.ToInt16(test2["EmployeeSerial"]);
                        si.EmplyeeName = test2["EmployeeName"].ToString();
                        vi3.Add(si);
                    }
                    //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
                    ViewBag.Data2 = vi3;
                }
            }
                using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM RoleCode ", connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.RoleId = Convert.ToInt16(test["RoleSerial"]);
                        si.RoleName = test["RoleName"].ToString();
                        vi.Add(si);
                    }
                    ViewBag.Data3 = vi.GroupBy(m => m.RoleName)
.Select(group => new { RoleId = group.First().RoleId, RoleName = group.Key })
.OrderBy(m => m.RoleName)
.ToList();
                }
            }
                

            //using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            //{
            //    using (SqlCommand command = new SqlCommand("SELECT * FROM ManagerCode ", connection))
            //    {
            //        connection.Open(); // Open the connection
            //        double TotalSales = 0;
            //        var vi = new List<RptSale>();
            //        var test = command.ExecuteReader();
            //        while (test.Read())
            //        {
            //            RptSale si = new RptSale();
            //            si.ManagerId = Convert.ToInt16(test["ManagerSerial"]);
            //            si.ManagerName = test["ManagerName"].ToString();
            //            vi.Add(si);
            //        }
            //        //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
            //        ViewBag.Data = vi;
            //        return View("CreateManager");
            //    }
            //}
            return View("CreateEmployee");
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(SalesParameters Parobj)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            if (Parobj.EmployeeId == 0)
            {
                Parobj.EmployeeId = 1;
            }
            var Employee = TempData["MaxId"];
            int x = 0;
            if (Parobj.IsManager)
            {
                x = 1;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO  EmployeeCode (EmployeeSerial,EmployeeName,EmployeeId,RoleCode,Job,IsManager) VALUES (@EmployeeSerial,@EmployeeName,@EmployeeId,@RoleCode,@Job,@IsManager)", connection))
                    {

                        //command.Parameters.AddWithValue("@Message", Message);
                        command.Parameters.AddWithValue("@EmployeeSerial", Parobj.EmployeeSerial);
                        command.Parameters.AddWithValue("@EmployeeName", Parobj.EmployeeName);
                        command.Parameters.AddWithValue("@EmployeeId", Employee);
                        command.Parameters.AddWithValue("@RoleCode", Parobj.RoleName);
                        command.Parameters.AddWithValue("@Job", Parobj.Job);
                        command.Parameters.AddWithValue("@IsManager", x);
                        connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateEmployee", "Attendance");
            }
            return RedirectToAction("CreateEmployee", "Attendance");
        }
        [HttpGet]
        public IActionResult CreateModel()
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand("select max(ModelId)ModelId from [TopSoft].[dbo].[ModelCode] ", connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi2 = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        if (!Convert.IsDBNull(test["ModelId"]))
                            si.ManagerId = Convert.ToInt32(test["ModelId"]);
                        else
                            si.ManagerId = 0;
                        vi2.Add(si);
                    }
                    var x = vi2.Select(x => x.ManagerId + 1).Distinct().ToList();
                    ViewBag.MaxId = x[0];
                    TempData["MaxId"] = x[0];
                }
            }
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand(@"
SELECT ManagerSerial,ManagerName FROM ManagerCode
 ", connection))
                {
                    connection.Open(); // Open the connection
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.ManagerId = Convert.ToInt16(test["ManagerSerial"]);
                        si.ManagerName = test["ManagerName"].ToString();
                        vi.Add(si);
                    }
                    ViewBag.Manager = vi.GroupBy(m => m.ManagerName)
.Select(group => new { ManagerId = group.First().ManagerId, ManagerName = group.Key })
.OrderBy(m => m.ManagerName)
.ToList();
                }
            }
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand(@"SELECT  EmployeeSerial,EmployeeName FROM EmployeeCode ", connection))
                {
                    connection.Open(); // Open the connection
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.EmplyeeCode = Convert.ToInt16(test["EmployeeSerial"]);
                        si.EmplyeeName = test["EmployeeName"].ToString();
                        vi.Add(si);
                    }
                    ViewBag.Employee = vi.GroupBy(m => m.EmplyeeName)
.Select(group => new { EmplyeeCode = group.First().EmplyeeCode, EmplyeeName = group.Key })
.OrderBy(m => m.EmplyeeName)
.ToList();

                }
            }
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand(@"SELECT ModelTypeSerial,ModelName FROM ModelTypeCode ", connection))
                {
                    connection.Open(); // Open the connection
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.ModelTypeId = Convert.ToInt16(test["ModelTypeSerial"]);
                        si.ModelTypeName = test["ModelName"].ToString();
                        vi.Add(si);
                    }
                    ViewBag.ModelType = vi.GroupBy(m => m.ModelTypeName)
.Select(group => new { ModelTypeId = group.First().ModelTypeId, ModelTypeName = group.Key })
.OrderBy(m => m.ModelTypeName)
.ToList();
                }
            }
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                using (SqlCommand command = new SqlCommand(@"SELECT * FROM RoleCode ", connection))
                {
                    connection.Open(); // Open the connection
                    var vi2 = new List<RptSale>();
                    var test1 = command.ExecuteReader();
                    while (test1.Read())
                    {
                        RptSale si = new RptSale();
                        si.RoleId = Convert.ToInt16(test1["RoleSerial"]);
                        si.RoleName = test1["RoleName"].ToString();
                        vi2.Add(si);
                    }
                    ViewBag.VBRole = vi2.GroupBy(m => m.RoleName)
.Select(group => new { RoleId = group.First().RoleId, RoleName = group.Key })
.OrderBy(m => m.RoleName)
.ToList();
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateModel(SalesParameters Parobj)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            if (Parobj.ModelId == 0)
            {
                Parobj.ModelId = 1;
            }
            var ModelId = TempData["MaxId"];
            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO  ModelCode (ModelId,ModelTypeSerial,EmployeeSerial,RoleSerial,FromDate,ToDate,CreatedDate,UpdatedDate,CreatedUser) VALUES (@ModelId,@ModelTypeSerial,@EmployeeSerial,@RoleSerial,@FromDate,@ToDate,@CreatedDate,@UpdatedDate,@CreatedUser)", connection))
                    {

                        //command.Parameters.AddWithValue("@Message", Message);
                        command.Parameters.AddWithValue("@ModelId", ModelId);
                        command.Parameters.AddWithValue("@ModelTypeSerial", Parobj.ModelTypeSerial);
                        command.Parameters.AddWithValue("@EmployeeSerial", Parobj.EmployeeSerial);
                        command.Parameters.AddWithValue("@RoleSerial", Parobj.RoleSerial);
                        command.Parameters.AddWithValue("@FromDate", Parobj.startDate);
                        command.Parameters.AddWithValue("@ToDate", Parobj.endDate);
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        command.Parameters.AddWithValue("@CreatedUser", username);
                        //command.Parameters.AddWithValue("@ManagerSerial", Parobj.ManagerSerial);
                        connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateModel", "Attendance");
            }
            return RedirectToAction("CreateModel", "Attendance");
        }
        [HttpGet]
        public IActionResult DisplayModel()
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            var x = TempData["R"];
            ViewBag.x = x;
            bool containsManaValue = false;
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                string SqlQuery2 = @"SELECT EmployeeSerial
  FROM [TopSoft].[dbo].[EmployeeCode]
  where ismanager=1 ";
                using (SqlCommand command = new SqlCommand(SqlQuery2, connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi2 = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.ByDay = Convert.ToInt32(test["EmployeeSerial"]);
                        vi2.Add(si);
                    }

                    // Convert ViewBag.Mana to a list of integers for easy access
                    IEnumerable<int> manaList = vi2.Where(x => x.ByDay.HasValue).Select(x => x.ByDay.Value);
                    foreach (int manaValue in manaList)
                    {
                        if (username.Contains(manaValue.ToString()))
                        {
                            containsManaValue = true;
                            break; // No need to continue checking once a match is found
                        }
                    }
                }
            }
            if(!containsManaValue)
            {
                TempData["R"] = "R";
                ViewBag.R = "R";
            }
            return View("DisplayModel");
        }
        [HttpPost]
        public IActionResult DisplayModel(SalesParameters Parobj, int id,int Checked)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            TempData["RequestStatus"] = Parobj.RequestStatus;
            if (id >= 0)
            {
                using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
                {
                    string SqlQuery = null;
                    if (Role== "HRAdmin")
                    {
                        SqlQuery += @"update [TopSoft].[dbo].[ModelCode] set HRModelStatus ";
                    }
                    else
                    {
                        SqlQuery += @"update [TopSoft].[dbo].[ModelCode] set ModelStatus ";
                    }
                    if (Checked==1)
                    {
                        SqlQuery += " ='1'";
                    }
                    else if(Checked == 3)
                    {
                        SqlQuery += " ='3'";
                        SqlQuery += " , Notes= '" + Parobj.TransferMessage + "'";
                    }
                    else
                    {
                        SqlQuery += " ='2'";
                    }
                    SqlQuery += " Where ModelSerial = " + id;
                    using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                    {
                        connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                        if (id != 0)
                        {
                            var message = new MimeMessage();
                        string Reply = null;
                        string Manager = null;
                        if (Checked == 1)
                        {
                            Reply = "Accept";
                        }
                        else if (Checked == 3)
                        {
                            Reply = "Reject";
                        }
                        if (Role == "HRAdmin")
                        {
                            Manager = "HR Management";
                        }
                        else
                        {
                            Manager = "Manager";
                        }
                            message.From.Add(new MailboxAddress(username, "AhmedHosny1340@gmail.com"));
                            message.To.Add(new MailboxAddress(username, "AhmedHosny1340@gmail.com"));
                            message.Subject = "Reply for Your Submit";

                            var bodyBuilder = new BodyBuilder { TextBody = "Your " + Manager + " " + Reply + @" your Order" };
                            message.Body = bodyBuilder.ToMessageBody();
                            using (var client = new MailKit.Net.Smtp.SmtpClient())
                            {
                                // For demo purposes, accept all SSL certificates (in case the server supports STARTTLS)
                                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                                // Note: only needed if the SMTP server requires authentication
                                client.Authenticate("AhmedHosny1340@gmail.com", "vifcfefeassobcza");

                                client.Send(message);
                                client.Disconnect(true);
                            }
                        }
                    }
                }

            }
            bool containsManaValue = false;
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                string SqlQuery2 = @"SELECT EmployeeSerial
  FROM [TopSoft].[dbo].[EmployeeCode]
  where ismanager=1 ";
                using (SqlCommand command = new SqlCommand(SqlQuery2, connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi2 = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.ByDay = Convert.ToInt32(test["EmployeeSerial"]);
                        vi2.Add(si);
                    }

                    // Convert ViewBag.Mana to a list of integers for easy access
                    IEnumerable<int> manaList = vi2.Where(x => x.ByDay.HasValue).Select(x => x.ByDay.Value);
                    foreach (int manaValue in manaList)
                    {
                        if (username.Contains(manaValue.ToString()))
                        {
                            containsManaValue = true;
                            break; // No need to continue checking once a match is found
                        }
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                string SqlQuery2 = @"select * from [TopSoft].[dbo].[RptModel] where  ModelSerial is not null ";
                if (ViewBag.Role != "HRAdmin")
                {
                    if (ViewBag.Role == "Employee" && containsManaValue)
                        SqlQuery2 += " and ManagerCode='" + username + "'";
                    else
                        SqlQuery2 += " and EmployeeCode='" + username + "'";
                }
                if (Role== "HRAdmin")
                {
                    if (Parobj.RequestStatus <= 3)
                    {
                        if (Parobj.RequestStatus == 3)
                        {
                            SqlQuery2 += " and HRModelStatus =3";
                        }
                        else if (Parobj.RequestStatus == 1)
                            SqlQuery2 += " and HRModelStatus =1";
                        else
                            SqlQuery2 += " and (HRModelStatus ='2' or HRModelStatus is null)";
                    }
                }
                else
                {
                    if (Parobj.RequestStatus <= 3)
                    {
                        if (Parobj.RequestStatus == 3)
                        {
                            SqlQuery2 += " and ModelStatus =3";
                        }
                        else if (Parobj.RequestStatus == 1)
                            SqlQuery2 += " and ModelStatus =1";
                        else
                            SqlQuery2 += " and (ModelStatus ='2' or ModelStatus is null)";
                    }
                }
                using (SqlCommand command = new SqlCommand(SqlQuery2, connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi2 = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.ModelSerial = Convert.ToInt32(test["ModelSerial"]);
                        si.EmplyeeCode = Convert.ToInt32(test["EmployeeCode"]);
                        si.EmplyeeName = test["EmployeeName"].ToString();
                        si.ManagerName = test["ManagerName"].ToString();
                        si.RoleName= test["RoleName"].ToString();
                        si.ModelTypeName = test["ModelName"].ToString();
                        si.StoreName = test["EmployeeJob"].ToString();
                        si.OrderStatus = test["OrderStatus"].ToString();
                        si.HROrderStatus = test["HROrderStatus"].ToString();
                        si.SupplierName = test["FromDate"].ToString();
                        si.Dmanager = test["ToDate"].ToString();
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
                        si.DpName = test["CreatedDate"].ToString();
                        DateTime parsedDate3;
                        if (DateTime.TryParse(si.DpName, out parsedDate3))
                        {
                            si.DpName = parsedDate3.ToString("yyyy-MM-dd");
                        }
                        si.Username = test["CreatedUser"].ToString();
                        vi2.Add(si);
                    }
                    ViewBag.Data = vi2;
                }
            }
            if (!containsManaValue)
            {
                TempData["R"] = "R";
                ViewBag.R = "R";
            }
            return View("DisplayModel");
        }
    }
}
