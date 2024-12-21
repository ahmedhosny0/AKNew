using CK.Models.AXDBTest;
using CK.Models.TopSoft;
using CK.Models;
using CK.ViewModel;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using CK.DTO;
using CK.Models.AXDB;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.CodeAnalysis.Operations;
using DocumentFormat.OpenXml.Drawing;
using Org.BouncyCastle.Asn1.X509;
using CK.Models.CKPro;

namespace CK.Repo.SalesOrder
{
    public class SalesOrderRepo : ISalesOrderRepo
    {
        private readonly SalesData _SalesData;
        private readonly AxdbContext _AxdbContext;
        private readonly CkproUsersContext _CkproUsersContext;
        private readonly TopSoftContext _TopSoftContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SalesOrderRepo(SalesData salesData
, CkproUsersContext ckproUsersContext, TopSoftContext topSoftContext, IHttpContextAccessor httpContextAccessor, AxdbContext axdbContext
            )
        {
            _SalesData = salesData;
            _CkproUsersContext = ckproUsersContext;
            _TopSoftContext = topSoftContext;
            _httpContextAccessor = httpContextAccessor;
            _AxdbContext = axdbContext; 
        }
        public async Task<bool> CheckTransactions(int Branch)
        {
            var data = await _TopSoftContext.StreetCodes.Where(x => x.BranchSerial == Branch).ToListAsync();
            if (data.Count > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<int> GetMaxCode()
        {
            int maxSalesCode = await _TopSoftContext.HsalesCodes
            .Where(h => h.SalesCode.HasValue) // Select only valid SalesCode values
            .OrderByDescending(h => h.SalesCode.Value) // Order descending to find max value
            .Select(h => h.SalesCode.Value) // Select the actual value
            .FirstOrDefaultAsync(); // Fetch the first value
            return maxSalesCode+1;
        }
        public async Task<List<CustomerCode>> GetCustomerList()
        {
            List<CustomerCode> data = (from
Cust in _TopSoftContext.CustomerCodes
                                      select new CustomerCode // Explicitly select the ItemsData type
                                      {
                                          CustomerCode1 = Cust.CustomerCode1,
                                          CustomerName = Cust.CustomerName
                                      })
                         .Distinct().OrderByDescending(s => s.CustomerName).ToList();
            return data;
        }
        public async Task<List<BranchDTO>> GetBranchesList(int Customer)
        {
            List<BranchDTO> data = (from
Cust in _TopSoftContext.CustomerCodes
                                       join st in _TopSoftContext.StreetCodes!
                                       on new { Cust.ZoneSerial,Cust.AreaSerial, StreetSerial = Cust.StreetSerial ?? 0 }
                                       equals new { st.ZoneSerial,st.AreaSerial, StreetSerial = st.Serial }
                                       join br in _TopSoftContext.BranchData!
                                       on st.BranchSerial equals br.Serial
                                       where
                                       Cust.CustomerCode1 == Customer//"5637144670"
                                       select new BranchDTO // Explicitly select the ItemsData type
                                       {
                                           BranchId = br.BranchIdD + ":" +br.BranchIdR,
                                           BranchName = br.BranchName,
                                           ServiceCost = st.ServiceCost,
                                           DeliveryTime = st.DeliveryTime
                                       })
                         .Distinct().OrderByDescending(s => s.BranchName).ToList();
            return data;
        }
        public async Task<List<BranchDatum>> GetBranchesList(string username, string Isuser)
        {
                IQueryable<BranchDatum> query;
                query = _TopSoftContext.BranchData;
            
            var Brancheslist = await query
    .GroupBy(m => m.BranchName)
    .Select(group => new CK.Models.TopSoft.BranchDatum
    {
        BranchIdD = group.First().BranchIdD + ":" + group.First().BranchIdR,
        BranchName = group.Key
    })
    .OrderBy(m => m.BranchName)
    .ToListAsync();
            return Brancheslist;
        }
        public async Task<List<string>> GetCategoryList()
        {
            var categoriesToMatch = new List<string>
{
    "Smoking Accessories", "Shawarma", "Service Charge", "Salty Snacks", "Salad",
    "Rich Cut & Fries", "Rich Cut", "Protein Bar", "POP Corn & Cotton Candy", "Pizza",
    "Phone Accessories", "Package Sweet Snacks", "Package Ice Cream", "Package Beverage",
    "Other Tobacco", "Oriental Pastry", "Offers Rich Cut", "Offers - Coffe & Croissant",
    "Offers", "Non Edible Grocery", "Kahwetek", "Juice", "Hot Meal", "Healthy Beauty Care",
    "General Merchandise", "Fresh Fruits", "French Fries", "Fountain", "Feteer",
    "Edible Grocery", "Dounts", "Delivery", "Crispy Sandwich", "Crispy Chicken", "Corn Dog",
    "Cookies", "Cold Cut", "Coffee", "Ck Ice Cream", "Cigarettes", "Candy", "Burger Offer",
    "Burger", "BreakFast", "Bakery", "AutoMotive"
};

            var Categorylist = await _AxdbContext.Ecorescategories
                .Where(x => categoriesToMatch.Contains(x.Name))
                .Select(g => g.Name)
                .Distinct()
                .ToListAsync();
            return Categorylist;
        }
        public async Task<List<ItemsData>>GetItems(string Branch, string CategoryId)
        {
            string[] storeVal = Branch.Split(':');
            var InventlocationId = _CkproUsersContext.Storeusers.Where(x => x.Storenumber == storeVal[0]).Select(x => x.Inventlocation).FirstOrDefault();
            List<ItemsData> Items = (from 
                                    // Pri in _AxdbContext.Pricedisctables
                                      Pu in _AxdbContext.Inventtables//! on Pri.Itemrelation equals Pu.Itemid
                                     join Inv in _AxdbContext.Inventsums! on Pu.Itemid equals Inv.Itemid
                                     join pr in _AxdbContext.Ecoresproducttranslations! on Pu.Product equals pr.Product
                                     join Li in _AxdbContext.Ecoresproductcategories! on pr.Product equals Li.Product
                                     join Cat in _AxdbContext.Ecorescategories! on Li.Category equals Cat.Recid
                                     where( Inv.Physicalinvent >0 &
                                     Inv.Inventlocationid == InventlocationId  &
                                    // Pri.Amount != 0 &Pri.Accountrelation =="Retail" &
                                     Cat.Name.ToString() == CategoryId)//"5637144670"
                                     select new ItemsData // Explicitly select the ItemsData type
                                     {
                                         ItemName = pr.Name,
                                         CategoryName = Cat.Name,
                                         ItemId = Pu.Itemid,
                                     Quantity=Inv.Physicalinvent})
                                     .Distinct().OrderByDescending(s => s.ItemName).ToList();
            return Items;
        }
        public async Task<ItemsData> GetPriceofItem(string Branch,string ItemId)
        {
            SalesParameters salesParameters = new SalesParameters();
            ItemsData itemsData = new ItemsData();
            string[] storeVal =  Branch.Split(':');
            var PriceCategory = _CkproUsersContext.Storeusers.Where(x => x.Storenumber == storeVal[0]).Select(x => x.PriceCategory).FirstOrDefault();
            if (storeVal[0] != "RMS")
            {
                var InventlocationId = _CkproUsersContext.Storeusers.Where(x => x.Storenumber == storeVal[0]).Select(x => x.Inventlocation).FirstOrDefault();
                itemsData.Quantity = 0;
                var physicalValue = (from Inv in _AxdbContext.Inventsums
                                     where Inv.Itemid == ItemId && Inv.Inventlocationid == InventlocationId
                                     select Inv.Physicalinvent).Distinct().FirstOrDefault();

                // Check if physicalValue is not null (nullable check)
                if (physicalValue != null)
                {
                    // Round and assign it to itemsData.Quantity
                    itemsData.Quantity = decimal.Round(physicalValue, 2);
                }
                else
                {
                    // Handle the case where no value is found (optional)
                    itemsData.Quantity = 0; // Or any other default value
                }
                var StoreName = _CkproUsersContext.Storeusers.Where(x => x.Storenumber == storeVal[0]).Select(x => x.Username).FirstOrDefault();
                if (StoreName == "HSC")
                {
                    itemsData.Price = decimal.Round(((from Pu in _AxdbContext.Pricedisctables
                                            join Inv in _AxdbContext.Inventsums! on Pu.Itemrelation equals Inv.Itemid
                                            where Pu.Itemrelation == ItemId & Pu.Todate.Date.ToString() == "1900-01-01" & Pu.Accountrelation == "HSC_New"
                                            select Pu.Amount).Distinct().First()), 2);
                }
                else if (PriceCategory == "B")
                {
                    itemsData.Price = decimal.Round(((from Pu in _AxdbContext.Pricedisctables
                                            where Pu.Itemrelation == ItemId & Pu.Todate.Date.ToString() == "1900-01-01" & Pu.Accountrelation == "NorthP"

                                            select Pu.Amount).Distinct().First()), 2);
                }
                else
                {
                    itemsData.Price = decimal.Round(((from Pu in _AxdbContext.Pricedisctables
                                            where Pu.Itemrelation == ItemId & Pu.Todate.Date.ToString() == "1900-01-01" & Pu.Accountrelation == "Retail"

                                            select Pu.Amount).Distinct().First()), 2);
                }
            }
            else
            {
                string sqlQuery = null;
                var username = _CkproUsersContext.Storeusers.Where(x => x.RmsstoNumber == storeVal[1]).Select(x => x.Username).FirstOrDefault();
                if (username == "RichCut" || username == "RichCut_No" || username == "RichCut_Zayed")
                {
                    sqlQuery = $" select Price,Itemlookupcode,ItemName,It.Quantity Qty FROM ({salesParameters.RichCutItemPrice})R where StoreId=SUBSTRING('{storeVal[1]}', 2, 1) and ItemLookupCode='{ItemId}'";
                }
                else
                {
                    sqlQuery = $"select Price,Itemlookupcode,ItemName,It.Quantity Qty FROM ({salesParameters.RmsRptItemPrice})R where StoreId={storeVal[1]} and ItemLookupCode='{ItemId}' ";
                }
                using (SqlConnection connection = new SqlConnection(salesParameters.RmsConnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        var vi = new List<RptSale>();
                        var ll = 0;
                        var test = command.ExecuteReader();
                        while (test.Read())
                        {
                            RptSale si = new RptSale();
                            //si.StoreName = test["Store Name"].ToString();
                            si.ItemLookupCode = test["Itemlookupcode"].ToString();
                            si.ItemName = test["ItemName"].ToString();
                            decimal price = Math.Round(Convert.ToDecimal(test["Price"]), 2);
                            si.Qty = Math.Round(Convert.ToDecimal(test["Qty"]), 2);
                            si.Price = price;
                            // si.Cost = 100;
                            // ViewBag.lname = ll;
                            vi.Add(si);
                        }
                        itemsData.Price = vi.Where(x => x.ItemLookupCode == ItemId).Select(x => x.Price).FirstOrDefault();
                        itemsData.Quantity = vi.Where(x => x.ItemLookupCode == ItemId).Select(x => x.Qty).FirstOrDefault();
                    }
                }
            }
            itemsData.Price = decimal.Round((itemsData.Price), 2);
            itemsData.Quantity = decimal.Round((itemsData.Quantity), 2);
            return itemsData;
        }
        public async Task<HsalesCode> GetSalesOrderByCodeAsync(int salesCode)
        {
            var header = await _TopSoftContext.HsalesCodes.FindAsync(salesCode);

            return header;
        }
        public async Task AddSalesOrderAsync(HsalesCode header, List<DsalesCode> details,string username)
        {
            try
            {
                var maxSalesCode = await _TopSoftContext.HsalesCodes
    .Where(h => h.SalesCode.HasValue) // Check only valid SalesCode values
    .OrderByDescending(h => h.SalesCode)
    .Select(h => h.SalesCode.Value)
    .FirstOrDefaultAsync();

                header.SalesCode = (maxSalesCode) + 1;
                header.Createddatetime = DateTime.Now;
                header.Updateddatetime = DateTime.Now;
                header.Createdby = username;
                header.SalesStatus=0;
                await _TopSoftContext.HsalesCodes.AddAsync(header);
                // Save changes
                await _TopSoftContext.SaveChangesAsync();
                // Assign the same SalesCode to all details
                foreach (var detail in details)
                {
                    detail.SalesCode = header.Serial;
                }
                foreach (var newDetail in details)
                {
                    _TopSoftContext.DsalesCodes.Add(newDetail);
                }
                // Save changes
                await _TopSoftContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (assumes a logger is set up)
               // _logger.LogError(ex, "Error creating sales order");

                // Optionally handle specific exceptions, e.g., validation errors, foreign key constraints, etc.
                throw;  // Re-throw or handle the exception as needed
            }
        }
        public async Task<SalesOrderRequest> GetSalesOrderAsync(int salesCode)
        {
            var salesOrderRequest = new SalesOrderRequest();

            // Fetch header data
            salesOrderRequest.Header = await _TopSoftContext.HsalesCodes
                .Include(h => h.DsalesCodes)
                .FirstOrDefaultAsync(h => h.Serial == salesCode);

            // Fetch details data
            if (salesOrderRequest.Header != null)
            {
                salesOrderRequest.Details = await _TopSoftContext.DsalesCodes
                    .Where(d => d.SalesCode == salesOrderRequest.Header.Serial)
                    .ToListAsync();
            }

            return salesOrderRequest;
        }
        public async Task UpdateSalesOrderAsync(HsalesCode header, List<DsalesCode> details)
        {
            var existingHeader = await _TopSoftContext.HsalesCodes
                    .Include(h => h.DsalesCodes)  // Include related details
                    .FirstOrDefaultAsync(h => h.Serial == header.Serial);
            if (existingHeader != null)
            {
                existingHeader.CustomerCode = header.CustomerCode;
                existingHeader.BranchCode = header.BranchCode;
                existingHeader.Notes = header.Notes;
                existingHeader.GrandTotal = header.GrandTotal;
                existingHeader.GrandTotalwithFees = header.GrandTotalwithFees;
                existingHeader.Createdby = header.Createdby;
                existingHeader.Updateddatetime = DateTime.Now;

                // Get existing detail Serials
                var existingDetailSerials = existingHeader.DsalesCodes.Select(d => d.Serial).ToHashSet();
                // Get new detail Serials
                var newDetailSerials = details.Select(d => d.Serial).ToHashSet();

                // Identify rows to delete
                var rowsToDelete = existingHeader.DsalesCodes
                    .Where(d => !newDetailSerials.Contains(d.Serial))
                    .ToList();

                // Remove rows that are no longer present in the new details
                foreach (var row in rowsToDelete)
                {
                    _TopSoftContext.DsalesCodes.Remove(row);
                }
                // Update or add new details
                foreach (var newDetail in details)
                {
                    var existingDetail = existingHeader.DsalesCodes
                        .FirstOrDefault(d => d.Serial == newDetail.Serial);

                    if (existingDetail != null)
                    {
                        // Update existing detail
                        existingDetail.ItemCode = newDetail.ItemCode;
                        existingDetail.Price = newDetail.Price;
                        existingDetail.Quantity = newDetail.Quantity;
                        existingDetail.Total = newDetail.Total;
                    }
                    else
                    {
                        // Add new detail
                        existingHeader.DsalesCodes.Add(newDetail);
                    }
                }

                await _TopSoftContext.SaveChangesAsync();
            }
        }
        public async Task DeleteSalesOrderAsync(int salesCode)
        {
            var header = await _TopSoftContext.HsalesCodes.FindAsync(salesCode);
            if (header != null)
            {
                var details = _TopSoftContext.DsalesCodes.Where(d => d.SalesCode == salesCode);
                _TopSoftContext.DsalesCodes.RemoveRange(details);
                await _TopSoftContext.SaveChangesAsync();
                _TopSoftContext.HsalesCodes.Remove(header);
                await _TopSoftContext.SaveChangesAsync();
            }
        }
        public async Task<List<SalesOrderDTO>> GetAllSalesOrder(string StoreId)
        {
            using (SqlConnection connection = new SqlConnection(_SalesData.TopSoftConnection))
            {
                string Query = "SELECT * from RptSalesOrder  ";
                if (!string.IsNullOrEmpty(StoreId))
                {
                    Query += "where BranchIdD=@StoreId ";
                }
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    connection.Open(); // Open the connection
                                       //Where
                    if (!string.IsNullOrEmpty(StoreId))
                    {
                        command.Parameters.AddWithValue("@StoreId", StoreId);
                    }                    //command.Parameters.Add(new SqlParameter("@Id", salesCode));
                    var vi = new List<SalesOrderDTO>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        SalesOrderDTO si = new SalesOrderDTO()
                        {
                            HSalesCode = test["HSalesCode"] == DBNull.Value ? "" : test["HSalesCode"].ToString(),
                            HSalesCode2 = test["HSalesCode2"] == DBNull.Value ? "" : test["HSalesCode2"].ToString(),
                            CustomerCode = test["CustomerCode"] == DBNull.Value ? "" : test["CustomerCode"].ToString(),
                            CategoryName = test["CategoryName"] == DBNull.Value ? string.Empty : test["CategoryName"].ToString(),
                            GrandTotal = test["GrandTotal"] == DBNull.Value ? "" : Convert.ToDouble(test["GrandTotal"]).ToString("#,##0.00"),
                            GrandTotalWithFees = test["GrandTotalWithFees"] == DBNull.Value ? "" : Convert.ToDouble(test["GrandTotalWithFees"]).ToString("#,##0.00"),
                            SalesOrderDate = test["SalesOrderDate"] == DBNull.Value ? "" : Convert.ToDateTime(test["SalesOrderDate"]).ToString("yyyy-MM-dd"),
                            CreatedDatetime = test["CreatedDatetime"] == DBNull.Value ? "" : Convert.ToDateTime(test["CreatedDatetime"]).ToString("yyyy-MM-dd"),
                            UpdatedDatetime = test["UpdatedDatetime"] == DBNull.Value ? "" : Convert.ToDateTime(test["UpdatedDatetime"]).ToString("yyyy-MM-dd"),
                            CreatedBy = test["CreatedBy"] == DBNull.Value ? string.Empty : test["CreatedBy"].ToString(),
                            Notes = test["Notes"] == DBNull.Value ? string.Empty : test["Notes"].ToString(),
                            DSalesCodeSerial = test["DSalesCode"] == DBNull.Value ? "" : test["DSalesCode"].ToString(),
                            DSalesCode = test["DSalesCode2"] == DBNull.Value ? "" : test["DSalesCode2"].ToString(),
                            ItemCode = test["ItemCode"] == DBNull.Value ? "" : test["ItemCode"].ToString(),
                            Price = test["Price"] == DBNull.Value ? "" : Convert.ToDouble(test["Price"]).ToString("#,##0.00"),
                            Quantity = test["Quantity"] == DBNull.Value ? "" : Convert.ToDouble(test["Quantity"]).ToString("#,##0.00"),
                            Total = test["Total"] == DBNull.Value ? "" : Convert.ToDouble(test["Total"]).ToString("#,##0.00"),
                            ItemName = test["ItemName"] == DBNull.Value ? string.Empty : test["ItemName"].ToString(),
                            CustomerName = test["CustomerName"] == DBNull.Value ? string.Empty : test["CustomerName"].ToString(),
                            Phone1 = test["Phone1"] == DBNull.Value ? string.Empty : test["Phone1"].ToString(),
                            Phone2 = test["Phone2"] == DBNull.Value ? string.Empty : test["Phone2"].ToString(),
                            Phone3 = test["Phone3"] == DBNull.Value ? string.Empty : test["Phone3"].ToString(),
                            Address1 = test["Address1"] == DBNull.Value ? string.Empty : test["Address1"].ToString(),
                            Address2 = test["Address2"] == DBNull.Value ? string.Empty : test["Address2"].ToString(),
                            ZoneCode = test["ZoneCode"] == DBNull.Value ? "" : test["ZoneCode"].ToString(),
                            ZoneCode2 = test["ZoneCode2"] == DBNull.Value ? "" : test["ZoneCode2"].ToString(),
                            ZoneName = test["ZoneName"] == DBNull.Value ? string.Empty : test["ZoneName"].ToString(),
                            AreaCode = test["AreaCode"] == DBNull.Value ? "" : test["AreaCode"].ToString(),
                            AreaCode2 = test["AreaCode2"] == DBNull.Value ? "" : test["AreaCode2"].ToString(),
                            AreaName = test["AreaName"] == DBNull.Value ? string.Empty : test["AreaName"].ToString(),
                            StreetCode = test["StreetCode"] == DBNull.Value ? "" : test["StreetCode"].ToString(),
                            StreetCode2 = test["StreetCode2"] == DBNull.Value ? "" : test["StreetCode2"].ToString(),
                            StreetName = test["StreetName"] == DBNull.Value ? string.Empty : test["StreetName"].ToString(),
                            DeliveryTime = test["DeliveryTime"] == DBNull.Value ? "" : test["DeliveryTime"].ToString(),
                            ServiceCost = test["ServiceCost"] == DBNull.Value ? "" : Convert.ToDouble(test["ServiceCost"]).ToString("#,##0.00"),
                            BranchCode = test["BranchCode"] == DBNull.Value ? "" : test["BranchCode"].ToString(),
                            BranchIdR = test["BranchIdR"] == DBNull.Value ? "" : test["BranchIdR"].ToString(),
                            BranchIdD = test["BranchIdD"] == DBNull.Value ? "" : test["BranchIdD"].ToString(),
                            BranchName = test["BranchName"] == DBNull.Value ? string.Empty : test["BranchName"].ToString(),
                        };

                        vi.Add(si);
                    }
                    return vi;
                }
            }
        }
        public async Task<List<SalesOrderDTO>> GetAllTransactions(string StoreId)
        {
            using (SqlConnection connection = new SqlConnection(_SalesData.TopSoftConnection))
            {
                string Query = "SELECT distinct BranchName,HSalesCode,Phone1,SalesOrderDate,SalesStatus,ZoneName,AreaName,StreetName,CustomerName,CreatedDatetime,GrandTotalWithFees,GrandTotal,ServiceCost from RptSalesOrder  ";
                if (!string.IsNullOrEmpty(StoreId))
                {
                    Query += "where BranchIdD=@StoreId and Salesstatus in (0,1,2) "; // 0 Call make // 1 Branch order,2 transit // 3 Done //4 Cancel
                }
                Query += " order by SalesOrderDate ";
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    connection.Open(); // Open the connection
                                       //Where
                    if (!string.IsNullOrEmpty(StoreId))
                    {
                        command.Parameters.AddWithValue("@StoreId", StoreId);
                    }                    //command.Parameters.Add(new SqlParameter("@Id", salesCode));
                    var vi = new List<SalesOrderDTO>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        SalesOrderDTO si = new SalesOrderDTO()
                        {
                            CreatedDatetime = test["CreatedDatetime"] == DBNull.Value ? "" : (test["CreatedDatetime"]).ToString(),
                            SalesOrderDate = test["SalesOrderDate"] == DBNull.Value ? "" : Convert.ToDateTime(test["SalesOrderDate"]).ToString("yyyy-MM-dd"),
                            HSalesCode = test["HSalesCode"] == DBNull.Value ? "" : test["HSalesCode"].ToString(),
                            Phone1 = test["Phone1"] == DBNull.Value ? string.Empty : test["Phone1"].ToString(),
                            SalesStatus = test["SalesStatus"] == DBNull.Value ? string.Empty : test["SalesStatus"].ToString(),
                            AreaName = test["AreaName"] == DBNull.Value ? string.Empty : test["AreaName"].ToString(),
                            ZoneName = test["ZoneName"] == DBNull.Value ? string.Empty : test["ZoneName"].ToString(),
                            StreetName = test["StreetName"] == DBNull.Value ? string.Empty : test["StreetName"].ToString(),
                            CustomerName = test["CustomerName"] == DBNull.Value ? string.Empty : test["CustomerName"].ToString(),
                            ServiceCost = test["ServiceCost"] == DBNull.Value ? "" : Convert.ToDouble(test["ServiceCost"]).ToString("#,##0.00"),
                            GrandTotal = test["GrandTotal"] == DBNull.Value ? "" : Convert.ToDouble(test["GrandTotal"]).ToString("#,##0.00"),
                            GrandTotalWithFees = test["GrandTotalWithFees"] == DBNull.Value ? "" : Convert.ToDouble(test["GrandTotalWithFees"]).ToString("#,##0.00"),
                            BranchName = test["BranchName"] == DBNull.Value ? string.Empty : test["BranchName"].ToString(),
                        };

                        vi.Add(si);
                    }
                    return vi;
                }
            }
        }
        public async Task<bool>BranchReply(HsalesCode header,string username)
        {
            header.CancelBy = username;
            //header.Message ??= "No Comment";
            try
            {
                using (SqlConnection connection = new SqlConnection(_SalesData.TopSoftConnection))
                {
                    connection.Open(); // Open the connection
                    string Query = @"UPDATE HSalesCode SET ";
                    if (header.SalesStatus == 4)
                        Query += " Message = @Message, ";
                    Query += "SalesStatus =@Status,Cancelby=@username where Serial=@Id ";
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        if (header.SalesStatus == 4)
                            command.Parameters.Add(new SqlParameter("@Message", header.Message));
                        command.Parameters.Add(new SqlParameter("@Id", header.Serial));
                        command.Parameters.Add(new SqlParameter("@Status", header.SalesStatus));
                        command.Parameters.Add(new SqlParameter("@username", header.CancelBy));
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;       
                    }
        }
    }
}
