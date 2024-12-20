using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using CK.Models;
namespace CK.Controllers
{
    public class CategoryLimit : BaseController
    {
        SalesParameters Parobj = new SalesParameters();
        [HttpPost]
        public IActionResult AttachItemwithCategory(SalesParameters Parobj, string CategoryName, string ItemBarCode)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            using (SqlConnection connection = new SqlConnection(Parobj.RmsConnection))
            {
                using (SqlCommand command = new SqlCommand("select distinct BarCode,Category from Ckprousers.dbo.Catlimit", connection))
                {

                    connection.Open(); // Open the connection
                    command.ExecuteNonQuery(); // Execute the command
                    var vi = new List<RptSale>();
                    var ll = 0;
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.DpName = test["Category"].ToString();
                        si.ItemLookupCode = test["BarCode"].ToString();
                        vi.Add(si);
                    }
                    ViewBag.Data = vi.Select(x => x.DpName).Distinct().ToList();
                    var Item = vi.Where(x => x.ItemLookupCode == ItemBarCode).Select(x => x.ItemLookupCode).Distinct().ToList();
                    if (Item.Count > 0)
                    {
                        ViewBag.Item = "V";
                        return View("AttachItemwithCategory");
                    }
                }
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.RmsConnection))
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO  Ckprousers.dbo.Catlimit (Category, BarCode) VALUES (@Category, @Barcode)", connection))
                    {

                        command.Parameters.AddWithValue("@Category", CategoryName);
                        command.Parameters.AddWithValue("@Barcode", ItemBarCode);
                        connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            ViewBag.ItemSave = "V";
            return View("AttachItemwithCategory");
        }
        [HttpGet]
        public IActionResult AttachItemwithCategory(string ItemBarCode)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            using (SqlConnection connection = new SqlConnection(Parobj.RmsConnection))
            {
                using (SqlCommand command = new SqlCommand("select distinct BarCode,Category from Ckprousers.dbo.Catlimit", connection))
                {

                    connection.Open(); // Open the connection
                    command.ExecuteNonQuery(); // Execute the command
                    var vi = new List<RptSale>();
                    var ll = 0;
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.DpName = test["Category"].ToString();
                        si.ItemLookupCode = test["BarCode"].ToString();
                        vi.Add(si);
                    }
                    ViewBag.Data = vi.Select(x => x.DpName).Distinct().ToList();
                    var Item = vi.Where(x => x.ItemLookupCode == ItemBarCode).Select(x => x.ItemLookupCode).Distinct().ToList();
                    if (Item.Count > 0)
                    {
                        ViewBag.Item = "V";
                        return View("AttachItemwithCategory");
                    }
                }
            }
            return View("AttachItemwithCategory");
        }
        public IActionResult DeleteItem(SalesParameters Parobj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.RmsConnection))
                {
                    using (SqlCommand command = new SqlCommand("Delete from  Ckprousers.dbo.Catlimit where Id =@Id", connection))
                    {

                        command.Parameters.AddWithValue("@Id", Parobj.Supplier);
                        connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction("DisplayItems", "CategoryLimit");
        }
        [HttpGet]
        public IActionResult DisplayItems(SalesParameters Parobj, string CategoryName, string ItemBarCode)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            

            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.RmsConnection))
                {
                    using (SqlCommand command = new SqlCommand("select Id,Category, Barcode from Ckprousers.dbo.Catlimit ", connection))
                    {
                        connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                        var vi = new List<RptSale>();
                        var ll = 0;
                        var test = command.ExecuteReader();
                        while (test.Read())
                        {
                            RptSale si = new RptSale();
                            si.StoreName = test["Category"].ToString();
                            si.ItemLookupCode = test["Barcode"].ToString();
                            si.ItemName = test["Id"].ToString();
                            ViewBag.Id = si.ItemName;
                            vi.Add(si);
                        }
                        ViewBag.Data = vi;
                        return View("DisplayItems");
                    }
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
