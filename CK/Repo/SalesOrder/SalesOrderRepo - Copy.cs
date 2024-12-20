//using CK.Models.AXDBTest;
//using CK.Models.TopSoft;
//using CK.Models;
//using CK.ViewModel;
//using Microsoft.EntityFrameworkCore;
//using DocumentFormat.OpenXml.Spreadsheet;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Org.BouncyCastle.Bcpg.OpenPgp;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Microsoft.EntityFrameworkCore.Metadata;
//using CK.DTO;
//using CK.Models.AXDB;

//namespace CK.Repo.SalesOrder
//{
//    public class SalesOrderRepo : ISalesOrderRepo
//    {
//        private readonly SalesData _SalesData;
//        private readonly AxdbContext _AxdbContext;
//        private readonly CkproUsersContext _CkproUsersContext;
//        private readonly TopSoftContext _TopSoftContext;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        public SalesOrderRepo(SalesData salesData
//, CkproUsersContext ckproUsersContext, TopSoftContext topSoftContext, IHttpContextAccessor httpContextAccessor, AxdbContext axdbContext
//            )
//        {
//            _SalesData = salesData;
//            _CkproUsersContext = ckproUsersContext;
//            _TopSoftContext = topSoftContext;
//            _httpContextAccessor = httpContextAccessor;
//            _AxdbContext = axdbContext; 
//        }
//        public async Task<int> GetMaxCode()
//        {
//            int maxSalesCode = await _TopSoftContext.HsalesCodes
//            .Where(h => h.SalesCode.HasValue) // Select only valid SalesCode values
//            .OrderByDescending(h => h.SalesCode.Value) // Order descending to find max value
//            .Select(h => h.SalesCode.Value) // Select the actual value
//            .FirstOrDefaultAsync(); // Fetch the first value
//            return maxSalesCode+1;
//        }
//        public async Task<List<CustomerCode>> GetCustomerList()
//        {
//            List<CustomerCode> data = (from
//Cust in _TopSoftContext.CustomerCodes
//                                      select new CustomerCode // Explicitly select the ItemsData type
//                                      {
//                                          CustomerCode1 = Cust.CustomerCode1,
//                                          CustomerName = Cust.CustomerName
//                                      })
//                         .Distinct().OrderByDescending(s => s.CustomerName).ToList();
//            return data;
//        }
//        public async Task<List<BranchDTO>> GetBranchesList(int Customer)
//        {
//            List<BranchDTO> data = (from
//Cust in _TopSoftContext.CustomerCodes
//                                       join st in _TopSoftContext.StreetCodes!
//                                       on new { Cust.ZoneSerial,Cust.AreaSerial, StreetSerial = Cust.StreetSerial ?? 0 }
//                                       equals new { st.ZoneSerial,st.AreaSerial, StreetSerial = st.Serial }
//                                       join br in _TopSoftContext.BranchData!
//                                       on st.BranchSerial equals br.Serial
//                                       where
//                                       Cust.CustomerCode1 == Customer//"5637144670"
//                                       select new BranchDTO // Explicitly select the ItemsData type
//                                       {
//                                           BranchId = br.BranchIdD + ":" +br.BranchIdR,
//                                           BranchName = br.BranchName,
//                                           ServiceCost = st.ServiceCost
//                                       })
//                         .Distinct().OrderByDescending(s => s.BranchName).ToList();
//            return data;
//        }
//        public async Task<List<BranchDatum>> GetBranchesList(string username, string Isuser)
//        {
//                IQueryable<BranchDatum> query;
//                query = _TopSoftContext.BranchData;
            
//            var Brancheslist = await query
//    .GroupBy(m => m.BranchName)
//    .Select(group => new CK.Models.TopSoft.BranchDatum
//    {
//        BranchIdD = group.First().BranchIdD + ":" + group.First().BranchIdR,
//        BranchName = group.Key
//    })
//    .OrderBy(m => m.BranchName)
//    .ToListAsync();
//            return Brancheslist;
//        }
//        public async Task<List<string>> GetCategoryList()
//        {
//            var categoriesToMatch = new List<string>
//{
//    "Smoking Accessories", "Shawarma", "Service Charge", "Salty Snacks", "Salad",
//    "Rich Cut & Fries", "Rich Cut", "Protein Bar", "POP Corn & Cotton Candy", "Pizza",
//    "Phone Accessories", "Package Sweet Snacks", "Package Ice Cream", "Package Beverage",
//    "Other Tobacco", "Oriental Pastry", "Offers Rich Cut", "Offers - Coffe & Croissant",
//    "Offers", "Non Edible Grocery", "Kahwetek", "Juice", "Hot Meal", "Healthy Beauty Care",
//    "General Merchandise", "Fresh Fruits", "French Fries", "Fountain", "Feteer",
//    "Edible Grocery", "Dounts", "Delivery", "Crispy Sandwich", "Crispy Chicken", "Corn Dog",
//    "Cookies", "Cold Cut", "Coffee", "Ck Ice Cream", "Cigarettes", "Candy", "Burger Offer",
//    "Burger", "BreakFast", "Bakery", "AutoMotive"
//};

//            var Categorylist = await _AxdbContext.Ecorescategories
//                .Where(x => categoriesToMatch.Contains(x.Name))
//                .Select(g => g.Name)
//                .Distinct()
//                .ToListAsync();
//            return Categorylist;
//        }
//        public async Task<List<ItemsData>>GetItems(string Branch, string CategoryId)
//        {
//            string[] storeVal = Branch.Split(':');
//            var InventlocationId = _CkproUsersContext.Storeusers.Where(x => x.Storenumber == storeVal[0]).Select(x => x.Inventlocation).FirstOrDefault();
//            List<ItemsData> Items = (from 
//                                    // Pri in _AxdbContext.Pricedisctables
//                                      Pu in _AxdbContext.Inventtables//! on Pri.Itemrelation equals Pu.Itemid
//                                     join Inv in _AxdbContext.Inventsums! on Pu.Itemid equals Inv.Itemid
//                                     join pr in _AxdbContext.Ecoresproducttranslations! on Pu.Product equals pr.Product
//                                     join Li in _AxdbContext.Ecoresproductcategories! on pr.Product equals Li.Product
//                                     join Cat in _AxdbContext.Ecorescategories! on Li.Category equals Cat.Recid
//                                     where( Inv.Physicalinvent >0 &
//                                     Inv.Inventlocationid == InventlocationId  &
//                                    // Pri.Amount != 0 &Pri.Accountrelation =="Retail" &
//                                     Cat.Name.ToString() == CategoryId)//"5637144670"
//                                     select new ItemsData // Explicitly select the ItemsData type
//                                     {
//                                         ItemName = pr.Name,
//                                         ItemId = Pu.Itemid,
//                                     Quantity=Inv.Physicalinvent})
//                                     .Distinct().OrderByDescending(s => s.ItemName).ToList();
//            return Items;
//        }
//        public async Task<ItemsData> GetPriceofItem(string Branch,string ItemId)
//        {
//            SalesParameters salesParameters = new SalesParameters();
//            ItemsData itemsData = new ItemsData();
//            string[] storeVal = Branch.Split(':');
//            var PriceCategory = _CkproUsersContext.Storeusers.Where(x => x.Storenumber == storeVal[0]).Select(x => x.PriceCategory).FirstOrDefault();
//            if (storeVal[0] != "RMS")
//            {
//                var InventlocationId = _CkproUsersContext.Storeusers.Where(x => x.Storenumber == storeVal[0]).Select(x => x.Inventlocation).FirstOrDefault();
//                itemsData.Quantity = 0;
//                var physicalValue = (from Inv in _AxdbContext.Inventsums
//                                     where Inv.Itemid == ItemId && Inv.Inventlocationid == InventlocationId
//                                     select Inv.Physicalinvent).Distinct().FirstOrDefault();

//                // Check if physicalValue is not null (nullable check)
//                if (physicalValue != null)
//                {
//                    // Round and assign it to itemsData.Quantity
//                    itemsData.Quantity = decimal.Round(physicalValue, 2);
//                }
//                else
//                {
//                    // Handle the case where no value is found (optional)
//                    itemsData.Quantity = 0; // Or any other default value
//                }
//                var StoreName = _CkproUsersContext.Storeusers.Where(x => x.Storenumber == storeVal[0]).Select(x => x.Username).FirstOrDefault();
//                if (StoreName == "HSC")
//                {
//                    itemsData.Price = decimal.Round(((from Pu in _AxdbContext.Pricedisctables
//                                            join Inv in _AxdbContext.Inventsums! on Pu.Itemrelation equals Inv.Itemid
//                                            where Pu.Itemrelation == ItemId & Pu.Todate.Date.ToString() == "1900-01-01" & Pu.Accountrelation == "HSC_New"
//                                            select Pu.Amount).Distinct().First()), 2);
//                }
//                else if (PriceCategory == "B")
//                {
//                    itemsData.Price = decimal.Round(((from Pu in _AxdbContext.Pricedisctables
//                                            where Pu.Itemrelation == ItemId & Pu.Todate.Date.ToString() == "1900-01-01" & Pu.Accountrelation == "NorthP"

//                                            select Pu.Amount).Distinct().First()), 2);
//                }
//                else
//                {
//                    itemsData.Price = decimal.Round(((from Pu in _AxdbContext.Pricedisctables
//                                            where Pu.Itemrelation == ItemId & Pu.Todate.Date.ToString() == "1900-01-01" & Pu.Accountrelation == "Retail"

//                                            select Pu.Amount).Distinct().First()), 2);
//                }
//            }
//            else
//            {
//                string sqlQuery = null;
//                var username = _CkproUsersContext.Storeusers.Where(x => x.RmsstoNumber == storeVal[1]).Select(x => x.Username).FirstOrDefault();
//                if (username == "RichCut" || username == "RichCut_No" || username == "RichCut_Zayed")
//                {
//                    sqlQuery = $" select Price,Itemlookupcode,ItemName,It.Quantity Qty FROM ({salesParameters.RichCutItemPrice})R where StoreId=SUBSTRING('{storeVal[1]}', 2, 1) and ItemLookupCode='{ItemId}'";
//                }
//                else
//                {
//                    sqlQuery = $"select Price,Itemlookupcode,ItemName,It.Quantity Qty FROM ({salesParameters.RmsRptItemPrice})R where StoreId={storeVal[1]} and ItemLookupCode='{ItemId}' ";
//                }
//                using (SqlConnection connection = new SqlConnection(salesParameters.RmsConnection))
//                {
//                    connection.Open();
//                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
//                    {
//                        var vi = new List<RptSale>();
//                        var ll = 0;
//                        var test = command.ExecuteReader();
//                        while (test.Read())
//                        {
//                            RptSale si = new RptSale();
//                            //si.StoreName = test["Store Name"].ToString();
//                            si.ItemLookupCode = test["Itemlookupcode"].ToString();
//                            si.ItemName = test["ItemName"].ToString();
//                            decimal price = Math.Round(Convert.ToDecimal(test["Price"]), 2);
//                            si.Qty = Math.Round(Convert.ToDecimal(test["Qty"]), 2);
//                            si.Price = price;
//                            // si.Cost = 100;
//                            // ViewBag.lname = ll;
//                            vi.Add(si);
//                        }
//                        itemsData.Price = vi.Where(x => x.ItemLookupCode == ItemId).Select(x => x.Price).FirstOrDefault();
//                        itemsData.Quantity = vi.Where(x => x.ItemLookupCode == ItemId).Select(x => x.Qty).FirstOrDefault();
//                    }
//                }
//            }
//            itemsData.Price = decimal.Round((itemsData.Price), 2);
//            itemsData.Quantity = decimal.Round((itemsData.Quantity), 2);
//            return itemsData;
//        }
//        public async Task<IEnumerable<HsalesCode>> GetAllSalesOrdersAsync()
//        {
//            return await _TopSoftContext.HsalesCodes.ToListAsync();
//        }
//        public async Task<HsalesCode> GetSalesOrderByCodeAsync(int salesCode)
//        {
//            var header = await _TopSoftContext.HsalesCodes.FindAsync(salesCode);

//            return header;
//        }
//        public async Task AddSalesOrderAsync(HsalesCode header, List<DsalesCode> details,string username)
//        {
//            var maxSalesCode = await _TopSoftContext.HsalesCodes
//            .Where(h => h.SalesCode.HasValue) // Select only valid SalesCode values
//            .OrderByDescending(h => h.SalesCode.Value) // Order descending to find max value
//            .Select(h => h.SalesCode.Value) // Select the actual value
//            .FirstOrDefaultAsync(); // Fetch the first value

//            // Assign SalesCode to header as maxSalesCode + 1
//            header.SalesCode = maxSalesCode + 1;
//            header.Createddatetime = DateTime.Now;
//            header.Updateddatetime = DateTime.Now;
//            header.Createdby = username;
//            // Assign the same SalesCode to all details
//            foreach (var detail in details)
//            {
//                detail.SalesCode = header.SalesCode;
//                GetPriceofItem(header.BranchCode, detail.ItemCode);
//            }
//            await _TopSoftContext.HsalesCodes.AddAsync(header);
//            await _TopSoftContext.DsalesCodes.AddRangeAsync(details);
//            await _TopSoftContext.SaveChangesAsync();
//        }
//        public async Task<List<SalesOrderDTO>> GetSalesOrderAsync(int salesCode)
//        {
//            using (SqlConnection connection = new SqlConnection(_SalesData.TopSoftConnection))
//            {
//                using (SqlCommand command = new SqlCommand("SELECT * from RptSalesOrder where HSalesCode2=@Id ", connection))
//                {
//                    connection.Open(); // Open the connection
//                    //command.Parameters.Add(new SqlParameter("@Alert", Message));
//                    command.Parameters.Add(new SqlParameter("@Id", salesCode));
//                    var vi = new List<SalesOrderDTO>();
//                    var test = command.ExecuteReader();
//                    while (test.Read())
//                    {
//                        SalesOrderDTO si = new SalesOrderDTO
//                        {
//                            HSalesCode = Convert.ToInt32(test["HSalesCode"]),
//                            HSalesCode2 = Convert.ToInt32(test["HSalesCode2"]),
//                            CustomerCode = Convert.ToInt32(test["CustomerCode"]),
//                            CategoryCode = (test["CategoryCode"]).ToString(),
//                            GrandTotal = Convert.ToDecimal(test["GrandTotal"]),
//                            GrandTotalWithFees = Convert.ToDecimal(test["GrandTotalWithFees"]),
//                            SalesOrderDate = (DateTime)test["SalesOrderDate"],
//                            CreatedDatetime = (DateTime)test["CreatedDatetime"],
//                            UpdatedDatetime = (DateTime?)test["UpdatedDatetime"],
//                            CreatedBy = test["CreatedBy"].ToString(),
//                            Notes = test["Notes"].ToString(),
//                            DSalesCodeSerial = Convert.ToInt32(test["DSalesCode"]),
//                            DSalesCode = Convert.ToInt32(test["DSalesCode2"]),
//                            ItemCode = Convert.ToInt32(test["ItemCode"]),
//                            Price = Convert.ToDecimal(test["Price"]),
//                            Quantity = Convert.ToInt32(test["Quantity"]),
//                            Total = Convert.ToDecimal(test["Total"]),
//                            ItemName = test["ItemName"].ToString()
//                        };
//                        vi.Add(si);
//                    }
//                    return vi;
//                }
//                //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
//            }
//            //        // Fetch header data
//            //        salesOrderRequest.Header = await _TopSoftContext.HsalesCodes
//            //    .Include(h => h.DsalesCodes)
//            //    .FirstOrDefaultAsync(); 

//            //// Fetch details data
//            //if (salesOrderRequest.Header != null)
//            //{
//            //    salesOrderRequest.Details = await _TopSoftContext.DsalesCodes
//            //        .Where(d => d.SalesCode == salesOrderRequest.Header.SalesCode)
//            //        .ToListAsync();
//            //}

//        }
//        public async Task<List<HsalesCode>> GetAllSalesOrder()
//        {
//            var data= await _TopSoftContext.HsalesCodes
//        .Include(h => h.DsalesCodes) // Include related DsalesCodes
//        .ToListAsync(); // Fetch all records as a list
//            return data;
//        }
//        public async Task UpdateSalesOrderAsync(HsalesCode header, List<DsalesCode> details)
//        {
//            var existingHeader = await _TopSoftContext.HsalesCodes
//                .FirstOrDefaultAsync(h => h.SalesCode == header.SalesCode);

//            if (existingHeader != null)
//            {
//                existingHeader.CustomerCode = header.CustomerCode;
//                existingHeader.BranchCode = header.BranchCode;
//                existingHeader.CategoryCode = header.CategoryCode;
//                existingHeader.Notes = header.Notes;
//                existingHeader.Updateddatetime = DateTime.Now;

//                // Update details: remove old ones and add new
//                var existingDetails = _TopSoftContext.DsalesCodes
//                    .Where(d => d.SalesCode == header.SalesCode);

//                _TopSoftContext.DsalesCodes.RemoveRange(existingDetails);
//                await _TopSoftContext.DsalesCodes.AddRangeAsync(details);

//                await _TopSoftContext.SaveChangesAsync();
//            }
//        }
//        public async Task DeleteSalesOrderAsync(int salesCode)
//        {
//            var header = await _TopSoftContext.HsalesCodes.FindAsync(salesCode);
//            if (header != null)
//            {
//                var details = _TopSoftContext.DsalesCodes.Where(d => d.SalesCode == salesCode);
//                _TopSoftContext.HsalesCodes.Remove(header);
//                _TopSoftContext.DsalesCodes.RemoveRange(details);
//                await _TopSoftContext.SaveChangesAsync();
//            }
//        }


//    }
//}
