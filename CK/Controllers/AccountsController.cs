using CK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using CK.Models.AXDBTest;
using System.Text.RegularExpressions;
using CK.Models.AXDB;
namespace CK.Controllers
{
    //Accounts
    public class AccountsController : BaseController
    {
        private readonly AxdbtestContext _AxdbtestContext;
        public AccountsController(AxdbtestContext context)
        {
            _AxdbtestContext = context;
        }
        AxdbContext Axdb = new AxdbContext();
        CkproUsersContext CC = new CkproUsersContext();
        EasysoftContext ES = new EasysoftContext();
        [HttpPost]
        public ActionResult AESGetInv(SalesParameters Parobj, DateTime start, string PO)
        {
            string PoID = "TMT-00" + PO;
            string P = PO.Remove(0, 6);
            var CheckItem = (
     from sl in Axdb.Saleslines
     where sl.Salesid == PO
     let taxValue = Regex.Match(sl.Taxitemgroup, @"\d+").Success ? Regex.Match(sl.Taxitemgroup, @"\d+").Value : ""
     select new
     {
         sl.Itemid,
         Tax = taxValue
     }
 ).Distinct().ToList();
            // string data = string.Join(",",CheckItem);
            //  TempData["Warnning"] = data;
            // return RedirectToAction("AESGet");
            //return RedirectToAction("Index", "Accounts");
            var ItemsNotInItaxes = (
                from item in CheckItem
                where !_AxdbtestContext.Itaxes.Any(it => it.ItemCode == item.Itemid)
                select new
                {
                    ItemId = item.Itemid
                }
            ).ToList();
            if (ItemsNotInItaxes.Any())
            {
                var warningMessage = $"The following items are not in TopSoft:{string.Join(" , ", ItemsNotInItaxes.Select(i => i.ItemId))}";
                TempData["Warnning"] = warningMessage;
                return RedirectToAction("Index", "Accounts");
            }
            var TaxChanged = (
                from item in CheckItem
                where !_AxdbtestContext.Itaxes.Any(it => it.ItemCode == item.Itemid && it.Tax.ToString() == item.Tax)
                select new
                {
                    item.Itemid,
                    item.Tax,
                }
            ).ToList();
            if (TaxChanged.Count > 0)
            {
                var warningMessage = $"The following items Tax not Same in TopSoft:{string.Join(" , ", TaxChanged.Select(i => i.Itemid))}";

                TempData["Warnning"] = warningMessage;
                // return RedirectToAction("AESGet");
                return RedirectToAction("Index", "Accounts");
            }
            var CheckCustomer = (
from sl in Axdb.Saleslines
where sl.Salesid == PO
select new
{
    sl.Custaccount
}
).Distinct().ToList();
            var custNotInCustomer = (
                from Cust in CheckCustomer
                where !_AxdbtestContext.CustInvs.Any(it => it.Code == Cust.Custaccount)
                select Cust
            ).ToList();
            if (custNotInCustomer.Count > 0)
            {
                var warningMessage = $"The following Customers not in TopSoft:{string.Join(" , ", custNotInCustomer.Select(i => i.Custaccount))}";
                TempData["Warnning"] = "Theses Customers " + warningMessage + " Not Exist in TopSoft";
                // return RedirectToAction("AESGet");
                return RedirectToAction("Index", "Accounts");
            }
            string? checkpo = (from INVE in ES.SalesInvoices
                               where INVE.DocNo == P
                               select INVE.DocNo).FirstOrDefault();

            if (checkpo != null)
            {
                TempData["Warnning"] = "PO already Exists in EasySoft";
                return RedirectToAction("Index", "Accounts");

            }
            //var story = (from st in Axdb.Saleslines
            //             where st.Createddatetime.Date == start.Date //& st.Modifieddatetime.Date == DateTime.Now.Date
            //                                                         &
            //             st.Custgroup == "Third Part" & st.Recversion != 1 &
            //             st.Salesid == PO
            //             select st.Salesid).Distinct().ToList();
            if (PO!=null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(Parobj.AxdbConnection))
                    {
                        string SqlQuery = @" 
 delete from AXDBTest.dbo.[InvoiceDetails] where Salesid =  @SalesId  
 insert into  AXDBTest.dbo.[InvoiceDetails]
select Ra.No CustId,t.ItemCode,SUBSTRING(st.Salesid, 7, LEN(st.Salesid)) Location,Ra.Code, st.Custaccount Depa, st.Itemid Descript, 
 Cast(st.Modifieddatetime as date) TransDate,st.Qtyordered Cost
, st.Lineamount Qty, st.Salesprice CostA,t.tax Tax,st.Salesid,cast(st.Createddatetime as date)CreatedDate
from SALESLINE st
inner join (
select tacu.Code,tacu.No from AXDBTest.dbo.CustInv tacu
)Ra on Ra.Code collate SQL_Latin1_General_CP1_CI_AS=st.Custaccount
inner join 
(select distinct ta.Tax,ItemCode from  AXDBTest.dbo.itax ta ) t on  st.Itemid = t.ItemCode collate SQL_Latin1_General_CP1_CI_AS
where  st.Salesid =  @SalesId 
and st.Custgroup = 'Third Part' and st.Recversion != 1
 delete from [192.168.1.76\sql2016].[Easysoft].dbo.[Sales_InvTrans] where DocNo =  SUBSTRING(@SalesId, 7, LEN(@SalesId)) 
 insert into  [192.168.1.76\sql2016].[Easysoft].dbo.[Sales_InvTrans]
  --insert into  [192.168.1.76\sql2016].[Easysoft].[dbo].[Sales_InvTrans]
  select (select top 1 sor.SortFi+1 from  [192.168.1.76\sql2016].Easysoft.dbo.Sales_Invoice sor order by sor.SortFi desc)
  'In','1',1,'1',Location,ROW_NUMBER() OVER (ORDER BY Location) AS Sn,0,Descript,'',Descript,'','1',0,0,0,0,cost,costa,0,0,0,0,Tax,0,0,((cost*costa)*(1+(tax*0.01))),'1',0,'1900-01-01','1900-01-01','',1,cost,0,0,0,0,0,'','',0,'','','',0,'1',0,0
  from AXDBTest.dbo.[InvoiceDetails]
where Salesid =  @SalesId 
--and cast(CreatedDate as date) = Cast(@CDate as date) --and cast(st.Modifieddatetime as date) =cast(getdate() as date)
  delete from [192.168.1.76\sql2016].[Easysoft].dbo.[Sales_invoice] where DocNo =  SUBSTRING(@SalesId, 7, LEN(@SalesId)) 
 insert into  [192.168.1.76\sql2016].[Easysoft].dbo.[Sales_invoice]
-- insert into  [192.168.1.76\sql2016].[Easysoft].[dbo].[Sales_invoice]
  select  (select top 1 sor.SortFi+1 from  [192.168.1.76\sql2016].Easysoft.dbo.Sales_Invoice sor order by sor.SortFi desc),
  	'1',1,'1',	Location,	'', 	0,	'',	Cast(getdate() as date),'1900-01-01',2,0,0,0,Cast(getdate() as date),0,0,'48101',0,CustId,'','1',1,'0',sum((cost*costa)*(1+(tax*0.01)))Total,'',0,0,0,0,0,0,'',0,0,'0',0,'C','',0,'','','',0,0,'1','','',0,0,'','',0
  from 
AXDBTest.dbo.[InvoiceDetails]

where Salesid =  @SalesId 
--and cast(CreatedDate as date) = Cast(@CDate as date) --and cast(st.Modifieddatetime as date) =cast(getdate() as date)
group by Location,CustId
";
                        using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                        {
                            //command.Parameters.AddWithValue("@CDate", start);
                            command.Parameters.AddWithValue("@SalesId", PO);

                            connection.Open(); // Open the connection
                            command.ExecuteNonQuery(); // Execute the command
                            TempData["Warnning"] = "Good";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Accounts");
                }
            }
            else
            {
                try
                {
                    string cdate = (from st in Axdb.Saleslines
                                    where //st.Createddatetime.Date == start.Date & st.Modifieddatetime.Date == end.Date &
                                    st.Custgroup == "Third Part" & st.Recversion != 1 &
                                    st.Salesid == PO
                                    select st.Createddatetime).First().ToShortDateString()!;
                    string mdate = (from st in Axdb.Saleslines
                                    where //st.Createddatetime.Date == start.Date & st.Modifieddatetime.Date == end.Date &
                                    st.Custgroup == "Third Part" & st.Recversion != 1 &
                                    st.Salesid == PO
                                    select st.Modifieddatetime).First().ToShortDateString()!;
                    TempData["Warnning"] = "PO Not Found Created Date: " + cdate + " ,Confirmed Date: " + mdate;
                }
                catch
                {
                    return RedirectToAction("Index", "Accounts");
                }

            }
            return RedirectToAction("Index", "Accounts");
        }
        public IActionResult Index()
        {
            var warningMessage = TempData["Warnning"];
            ViewBag.WarningMessage = warningMessage;
            return View();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////Invoices        

        // Insert Easysoft Invoice Products

        CkproUsersContext Ck = new CkproUsersContext();
        // GET: New Customer / Add
        public IActionResult AInvNewCust(SalesParameters Parobj)
        {
            CkproUsersContext Ckpro = new CkproUsersContext();
            using (SqlConnection connection = new SqlConnection(Parobj.AxdbConnection))
            {
                using (SqlCommand command = new SqlCommand("select Code,No,Name from  AXDBTest.dbo.CustInv order by Code asc ", connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.StoreName = test["Code"].ToString();
                        ViewBag.Branches = si.StoreName;
                        si.Username = test["Name"].ToString();
                        ViewBag.Users = si.Username;
                        si.TotalSales = Convert.ToDouble(test["No"]);
                        ViewBag.No = si.TotalSales;
                        vi.Add(si);
                    }
                    //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
                    ViewBag.Data = vi;
                    return View("AInvNewCust");

                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewItem(SalesParameters Parobj, string ItemCode, string Tax)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Parobj.AxdbConnection))
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO  AXDBTest.dbo.[ITax] (ItemCode, Tax) VALUES (@ItemCode, @Tax)", connection))
                    {

                        command.Parameters.AddWithValue("@ItemCode", ItemCode);
                        command.Parameters.AddWithValue("@Tax", Tax);
                        connection.Open(); // Open the connection
                        command.ExecuteNonQuery(); // Execute the command
                    }
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction("AInvNewItem", "Accounts");
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewCust(SalesParameters Parobj, int NO, string Name, string Code)
        {
            CkproUsersContext Ckpro = new CkproUsersContext();
            int maxId;
            bool success = int.TryParse(Ckpro.CustInvs.Max(x => x.No).ToString(), out maxId);
            int x = 0;
            if (success)
            {
                x = maxId + 1;
            }
            //NO = x.ToString();
            // NO = ViewBag.MaxId;
            if (Name != null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(Parobj.AxdbConnection))
                    {
                        using (SqlCommand command = new SqlCommand("INSERT INTO  AXDBTest.dbo.CustInv (No,Name,Code) VALUES (@No,@Name,@Code)", connection))
                        {

                            command.Parameters.AddWithValue("@No", NO);
                            command.Parameters.AddWithValue("@Name", Name);
                            command.Parameters.AddWithValue("@Code", Code);
                            connection.Open(); // Open the connection
                            command.ExecuteNonQuery(); // Execute the command
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            return RedirectToAction("AInvNewCust", "Accounts");
        }
        public IActionResult AInvNewItem(SalesParameters Parobj)
        {
            CkproUsersContext Ckpro = new CkproUsersContext();
            using (SqlConnection connection = new SqlConnection(Parobj.AxdbConnection))
            {
                using (SqlCommand command = new SqlCommand("SELECT [ItemCode],[Tax] FROM AXDBTest.dbo.[ITax] ", connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.StoreName = test["ItemCode"].ToString();
                        ViewBag.Branches = si.StoreName;
                        si.Username = test["Tax"].ToString();
                        ViewBag.Users = si.Username;
                        vi.Add(si);
                    }
                    //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
                    ViewBag.Data = vi;
                    return View("AInvNewItem");

                }
            }
        }
        public IActionResult DisplayUploadedInvoices(SalesParameters Parobj)
        {
            Parobj.DisplayUploadedInvoices = true;
            CkproUsersContext Ckpro = new CkproUsersContext();
            using (SqlConnection connection = new SqlConnection(Parobj.EasySoftConnection))
            {// where cast(DueDate as date) >=cast(getdate()-7 as date)
                using (SqlCommand command = new SqlCommand("select  SortFi,DocNo,DocDate,DueDate,AccCode,CustId,TotAmt,UUID,Status from Sales_Invoice  where cast(DueDate as date) >='2024-06-01' order by SortFi desc ", connection))
                {
                    connection.Open(); // Open the connection
                    double TotalSales = 0;
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.StoreName = test["SortFi"].ToString();
                        si.Username = test["DocNo"].ToString();
                        si.Dmanager = test["DocDate"].ToString();
                        DateTime parsedDate;
                        if (DateTime.TryParse(si.Dmanager, out parsedDate))
                        {
                            si.Dmanager = parsedDate.ToString("yyyy-MM-dd");
                        }
                        si.Fmanager = test["DueDate"].ToString();
                        DateTime parsedDate2;
                        if (DateTime.TryParse(si.Fmanager, out parsedDate))
                        {
                            si.Fmanager = parsedDate.ToString("yyyy-MM-dd");
                        }
                        si.DpName = test["AccCode"].ToString();
                        si.SupplierName = test["CustId"].ToString();
                        si.TotalSales = Convert.ToDouble(test["TotAmt"]);
                        TotalSales = Convert.ToDouble(si.TotalSales);
                        si.ItemName = TotalSales.ToString("#,##0.00");
                        si.StoreFranchise = test["UUID"].ToString();
                        si.StoreId = test["Status"].ToString();
                        vi.Add(si);
                    }
                    //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
                    ViewBag.Data = vi;
                    return View("DisplayUploadedInvoices");
                }
            }
        }
        // GET: Edit Item 
        [HttpGet]
        public IActionResult AInvEditItem(SalesParameters Parobj, string itemId)
        {
            CkproUsersContext Ckpro = new CkproUsersContext();
            using (SqlConnection connection = new SqlConnection(Parobj.AxdbConnection))
            {
                using (SqlCommand command = new SqlCommand("SELECT [ItemCode],[Tax] FROM AXDBTest.dbo.[ITax] where Itemcode=@ItemCode", connection))
                {
                    connection.Open(); // Open the connection
                    command.Parameters.Add(new SqlParameter("@ItemCode", itemId));
                    double TotalSales = 0;
                    var vi = new List<RptSale>();
                    var test = command.ExecuteReader();
                    while (test.Read())
                    {
                        RptSale si = new RptSale();
                        si.StoreName = test["ItemCode"].ToString();
                        ViewBag.Branches = si.StoreName;
                        si.Username = test["Tax"].ToString();
                        ViewBag.Users = si.Username;
                        TempData["id"] = si.StoreName;
                        vi.Add(si);
                    }
                    //var orderedVi = vi.OrderBy(sale => sale.StoreName).ToList();
                    ViewBag.Data = vi;
                    return View();

                }
            }
        }
        public IActionResult DeleteItem(SalesParameters Parobj, string ItemCode)
        {
            using (SqlConnection connection = new SqlConnection(Parobj.AxdbConnection))
            {
                using (SqlCommand command = new SqlCommand("delete from AXDBTest.dbo.[ITax] where Itemcode=@ItemCode", connection))
                {
                    connection.Open(); // Open the connection
                    command.Parameters.Add(new SqlParameter("@ItemCode", ItemCode));
                    var test = command.ExecuteReader();
                    return RedirectToAction("AInvNewItem", "Accounts");
                }
            }
        }
        public IActionResult DeleteInvoice(SalesParameters Parobj, string Id)
        {
            using (SqlConnection connection = new SqlConnection(Parobj.EasySoftConnection))
            {
                using (SqlCommand command = new SqlCommand("delete from Sales_Invoice where DocNo=@DocNo delete  from Sales_InvTrans where DocNo=@DocNo", connection))
                {
                    connection.Open(); // Open the connection
                    command.Parameters.Add(new SqlParameter("@DocNo", Id));
                    var test = command.ExecuteReader();
                    return RedirectToAction("DisplayUploadedInvoices", "Accounts");
                }
            }
        }
        [HttpPost]
        public IActionResult AInvEditItem(string itemId, Models.AXDBTest.Itax itax, SalesParameters Parobj, string ItemCode, int Tax)
        {
            var id = TempData["id"];
            var ExistingItem = _AxdbtestContext.Itaxes.FirstOrDefault(x => x.ItemCode == id);
            ExistingItem.ItemCode = ItemCode;
            ExistingItem.Tax = Tax;
            _AxdbtestContext.Itaxes.Update(ExistingItem);
            _AxdbtestContext.SaveChanges();
            return RedirectToAction("AInvNewItem", "Accounts");
        }
    }
}

