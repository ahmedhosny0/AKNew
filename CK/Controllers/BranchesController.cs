using CK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using DocumentFormat.OpenXml.ExtendedProperties;
using CK.Models.AXDB;
using CK.Models.TopSoft.Model;
namespace CK.Controllers
{
    public class BranchesController : BaseController
    {
        SalesParameters Parobj = new SalesParameters();
        AxdbContext Axdb = new AxdbContext();
        DataCenterContext db = new DataCenterContext();
        CkproUsersContext db2 = new CkproUsersContext();
        CkhelperdbContext db3 = new CkhelperdbContext();
        DataCenterPrevYrsContext db4 = new DataCenterPrevYrsContext();
        private readonly ILogger<BranchesController> _logger;
        private readonly CkproUsersContext _dbContext;
        private static readonly List<RptUser3> Users = new List<RptUser3>();
        public BranchesController(ILogger<BranchesController> logger, CkproUsersContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
 
        public string encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public string Decrypt(string clearText)
        {
            string DecryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Convert.FromBase64String(clearText);
            using (Aes decryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(DecryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                decryptor.Key = pdb.GetBytes(32);
                decryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return clearText;
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult CreateStore()
        {
            ViewBag.VBDManager = _dbContext.Users.Where(a => a.Role == "TerrManager").Select(x => x.User1).Distinct().ToList();
            ViewBag.VBFManager = _dbContext.Users.Where(a => a.Role == "FoodManager").Select(x => x.User1).Distinct().ToList();
            ViewBag.VBBranchOwner = _dbContext.Users.Where(a => a.Role == "BranchOwner").Select(x => x.User1).Distinct().ToList();
            var user = new User();
            ViewBag.MaxId = _dbContext.Storeusers.Max(x => x.Id+1);
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStore([Bind("Inventlocation,Storenumber,Username,Password,Name,Server,RmsstoNumber,Id,Email,Dbase,PriceCategory,Franchise,Company,Zkip,StartDate,ArabicN,District,Dmanager,Fmanager,BranchOwner")] Storeuser store)
        {
            // Encrypt the password before saving
            //ViewBag.VBDManager = _dbContext.Users.Where(a => a.Role == "TerrManager").Select(x => x.User1).Distinct().ToList();
            //ViewBag.VBFManager = _dbContext.Users.Where(a => a.Role == "FoodManager").Select(x => x.User1).Distinct().ToList();
            //    DateTime endDate = Convert.ToDateTime(store.StartDate);
            //    store.StartDate = endDate.ToString("yyyy-MM-dd");
            store.Inventlocation ??= "Null";
            store.Storenumber ??= "Null";
            store.RmsstoNumber ??= "Null";
            store.ArabicN ??= "Null";
            store.Company ??= "Null";
            store.StartDate ??= "Null";
            store.Dbase ??= "Null";
            store.DecryptedPassword ??= "Null";
            store.District ??= "Null";
            store.Dmanager ??= "UnKnown";
            store.Fmanager ??= "UnKnown";
            store.Email ??= "Null";
            store.Franchise??= "Null";
            store.Name ??= "Null";
            store.PriceCategory ??= "Null";
            store.Server ??= "Null";
            store.Zkip ??= "Null";
            store.Username ??= "Null";
            store.Password ??= "Null";
            store.BranchOwner ??= "Null";
            //store.RMSD365 ??= "Null";
            //store.dbZKName ??= "Null";
            //store.IsClosed ??= "Null";
            //store.IsAttendance ??= "Null";
            //store.IsInMap ??= "Null";
            //store.lat ??= "Null";
            //store.llong ??= "Null";
            store.UpdatedDateTime=DateTime.Now;
            store.Password = encrypt(store.Password);

            _dbContext.Add(store);
            await _dbContext.SaveChangesAsync();
            /*
                using (SqlConnection connection = new SqlConnection(Parobj.RmsConnection))
                {
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO  [CKHelperdb].[dbo].[Store] 
([Franchise],[RMSD365],[StoreIdR],[CompanyName],[StoreNameR],[StoreIdD],[StoreNameD],[Email],[FirstTransactionR],[LastTransactionR],[FirstTransactionD],[LastTransactionD],[PriceCategory],[ServerName],[ZKIP],[dbName],[dbZKName],[IsClosed],[IsAttendance],[IsAlly],[IsInMap],[lat],[long])
VALUES @Franchise, @RMSD365, @StoreIdR, @CompanyName, @StoreNameR, @StoreIdD, @StoreNameD, @Email, @FirstTransactionR, @LastTransactionR, @FirstTransactionD, @LastTransactionD, @PriceCategory, @ServerName, @ZKIP, @dbName, @dbZKName, @IsClosed, @IsAttendance, @IsAlly, @IsInMap, @lat, @long)", connection))
                    {

                    //command.Parameters.AddWithValue("@Message", Message);
                    command.Parameters.AddWithValue("@Franchise", store.Franchise);
                    command.Parameters.AddWithValue("@RMSD365", store.RMSD365);
                    command.Parameters.AddWithValue("@StoreIdR", store.RmsstoNumber);
                    command.Parameters.AddWithValue("@CompanyName", store.Company);
                    command.Parameters.AddWithValue("@StoreNameR", store.Username);
                    command.Parameters.AddWithValue("@StoreIdD", store.Storenumber);
                    command.Parameters.AddWithValue("@StoreNameD", store.Username);
                    command.Parameters.AddWithValue("@Email", store.Email);
                    command.Parameters.AddWithValue("@FirstTransactionR", "NULL");
                    command.Parameters.AddWithValue("@LastTransactionR", "NULL");
                    command.Parameters.AddWithValue("@FirstTransactionD", "NULL");
                    command.Parameters.AddWithValue("@LastTransactionD", "NULL");
                    command.Parameters.AddWithValue("@PriceCategory", store.PriceCategory);
                    command.Parameters.AddWithValue("@ServerName", store.Server);
                    command.Parameters.AddWithValue("@ZKIP", store.Zkip);
                    command.Parameters.AddWithValue("@dbName", store.Dbase);
                    command.Parameters.AddWithValue("@dbZKName", store.dbZKName);
                    command.Parameters.AddWithValue("@IsClosed", store.IsClosed);
                    command.Parameters.AddWithValue("@IsAttendance", store.IsAttendance);
                    command.Parameters.AddWithValue("@IsAlly", "0");
                    command.Parameters.AddWithValue("@IsInMap", store.IsInMap);
                    command.Parameters.AddWithValue("@lat", store.lat);
                    command.Parameters.AddWithValue("@long", store.llong);
                    connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            */
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                string Sql = @"
delete from storeuser
insert into storeuser
select * from [192.168.1.156].CkproUsers.dbo.Storeuser

delete from [192.168.1.210].AXDBtest.dbo.storeusers
insert into [192.168.1.210].AXDBtest.dbo.storeusers
select *
from [192.168.1.156].CkproUsers.dbo.Storeuser ";
                using (SqlCommand command = new SqlCommand(Sql, connection))
                {
                    connection.Open(); // Open the connection
                    command.ExecuteNonQuery(); // Execute the command
                }
            }
            try
            {
                if (store.Storenumber != "RMS")
                {
                    if (store.Server != null || store.Server != "")
                    {
                        if (store.Server.Contains("New")|| store.Server.Contains("new"))
                        {
                            using (SqlConnection connection = new SqlConnection(Parobj.AxdbConnection))
                            {
                                string Sqlserver = null;
                                Sqlserver += @"
USE master;  
EXEC sp_dropserver
   @server=N'" + store.Server + @"',  
   @droplogins ='droplogins';
EXEC sp_addlinkedserver   
   @server=N'" + store.Server + @"',  
   @srvproduct = N'SQL Server';  
";
                                using (SqlCommand command = new SqlCommand(Sqlserver, connection))
                                {
                                    connection.Open(); // Open the connection
                                    command.ExecuteNonQuery(); // Execute the command
                                }
                            }
                        }

                    }
                }
            }
            catch
            {
                return RedirectToAction("DisplayStores");

            }

            return RedirectToAction("DisplayStores");
        }
        public bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out _);
        }
        public string decrypt(string cipherText)
        {
            if (!IsBase64String(cipherText))
            {
                // Handle the error, e.g., log it, return a default value, or throw an exception
                return "Invalid encrypted password format";
            }

            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public async Task<IActionResult> DisplayStores()
        {
            using (var db2 = new CkproUsersContext())
            {
                var storeUsers = await db2.Storeusers.OrderByDescending(x=>x.Id).ToListAsync();
                // Decrypt each user's password
                foreach (var user in storeUsers)
                {
                    user.DecryptedPassword = decrypt(user.Password); // Assuming 'decrypt' is your decryption method
                }

                var stores = _dbContext.Storeusers.OrderByDescending(x => x.Id).ToList();
                if (username == null)
                {
                    return View("Logout");
                }
                return View(storeUsers);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditStore(int? id)
        {
            var stores = await _dbContext.Storeusers.FindAsync(id);
            ViewBag.VBDManager = _dbContext.Users.Where(a => a.Role == "TerrManager").Select(x => x.User1).Distinct().ToList();
            ViewBag.VBFManager = _dbContext.Users.Where(a => a.Role == "FoodManager").Select(x => x.User1).Distinct().ToList();
            ViewBag.VBBranchOwner = _dbContext.Users.Where(a => a.Role == "BranchOwner").Select(x => x.User1).Distinct().ToList();
            if (stores.StartDate!= "Null"&& stores.StartDate != "UnKnown")
            {
                DateTime endDate = Convert.ToDateTime(stores.StartDate);
                stores.StartDate = endDate.ToString("yyyy-MM-dd");
            }
            // Assigning the formatted date strings to ViewBag
           // ViewBag.SupplierName = startDate.ToString("yyyy-MM-dd");
            //if Password Encrypted Algorithm is not 64 i will display this password as it -Ahmed Hosny
            try
            {
                stores.Password = Decrypt(stores.Password);
            }
            catch (Exception ex)
            {
                stores.Password = stores.Password;
            }
            //stores.Password = Decrypt(stores.Password);
            return View(stores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStore(int id, [Bind("Inventlocation,Storenumber,Username,Password,Name,Server,RmsstoNumber,Id,Email,Dbase,PriceCategory,Franchise,Company,Zkip,StartDate,ArabicN,District,Dmanager,Fmanager,UpdatedDatetime,BranchOwner")] Storeuser store)
        {
            store.Password = encrypt(store.Password);
            store.Inventlocation ??= "Null";
            store.Storenumber ??= "Null";
            store.RmsstoNumber ??= "Null";
            store.ArabicN ??= "Null";
            store.Company ??= "Null";
            store.StartDate ??= "Null";
            store.Dbase ??= "Null";
            store.DecryptedPassword ??= "Null";
            store.District ??= "Null";
            store.Dmanager ??= "UnKnown";
            store.Fmanager ??= "UnKnown";
            store.Email ??= "Null";
            store.Franchise ??= "Null";
            store.Name ??= "Null";
            store.PriceCategory ??= "Null";
            store.Server ??= "Null";
            store.Zkip ??= "Null";
            store.Username ??= "Null";
            store.BranchOwner ??= "Null";
            store.UpdatedDateTime= DateTime.Now;
            _dbContext.Update(store);
            await _dbContext.SaveChangesAsync();
            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            //    {
            //        connection.Open(); // Open the connection

            //        using (SqlCommand command = new SqlCommand("UPDATE Manager SET StartDate = @StartDate ,EndDate=@EndDate where Id=@Id", connection))
            //        {
            //            //command.Parameters.Add(new SqlParameter("@Message", Message));
            //            command.Parameters.Add(new SqlParameter("@Id", id));
            //            command.Parameters.Add(new SqlParameter("@StartDate", StartDate));
            //            command.Parameters.Add(new SqlParameter("@EndDate", EndDate));
            //            command.ExecuteNonQuery(); // Execute the command
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return View();
            //}
            using (SqlConnection connection = new SqlConnection(Parobj.TopSoftConnection))
            {
                string Sql = @"
delete from storeuser
insert into storeuser
select * from [192.168.1.156].CkproUsers.dbo.Storeuser

delete from [192.168.1.210].AXDBtest.dbo.storeusers
insert into [192.168.1.210].AXDBtest.dbo.storeusers
select *
from [192.168.1.156].CkproUsers.dbo.Storeuser ";
                using (SqlCommand command = new SqlCommand(Sql, connection))
                {
                    connection.Open(); // Open the connection
                    command.ExecuteNonQuery(); // Execute the command
                }
            }
            try
            {
                if (store.Storenumber != "RMS")
                {
                    if (store.Server != null || store.Server != "")
                    {
                        if (store.Server.Contains("New"))
                        {
                            using (SqlConnection connection = new SqlConnection(Parobj.AxdbConnection))
                            {
                                string Sqlserver = null;
                                Sqlserver += @"
USE master;  
EXEC sp_dropserver
   @server=N'" + store.Server + @"',  
   @droplogins ='droplogins';
EXEC sp_addlinkedserver   
   @server=N'" + store.Server + @"',  
   @srvproduct = N'SQL Server';  
";

                                using (SqlCommand command = new SqlCommand(Sqlserver, connection))
                                {
                                    connection.Open(); // Open the connection
                                    command.ExecuteNonQuery(); // Execute the command
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                return RedirectToAction("DisplayStores");

            }
            return RedirectToAction("DisplayStores");
        }

        [HttpPost]
        public IActionResult DeleteStore(int? id)
        {
            // Retrieve the user details from the database based on the username
            var store = _dbContext.Storeusers.FirstOrDefault(u => u.Id == id);
            if (store == null)
            {
                return NotFound(); // Return a 404 Not Found if user is not found
            }

            // Remove the user from the database
            _dbContext.Storeusers.Remove(store);
            _dbContext.SaveChanges();

            // Redirect to the display users page after the deletion is successful
            return RedirectToAction("DisplayStores");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}

