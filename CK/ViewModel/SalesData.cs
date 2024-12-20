using CK.Models;
using System.Security.Cryptography.X509Certificates;

namespace CK.ViewModel
{
    public class SalesData : BaseBranchData
    {
        public List<string> SelectedStores { get; set; }
        public bool Yesterday { get; set; }
        public bool VPerMonYear { get; set; }
        public bool VPerMon { get; set; }
        public bool VPerYear { get; set; }
        public bool VTotalCost { get; set; }
        public bool VTotalTax { get; set; }
        public bool VTotalSalesTax { get; set; }
        public bool VTotalSalesWithoutTax { get; set; }
        public bool VTotalCostQty { get; set; }
        public bool VTransactionCount { get; set; }
        public bool MainSales { get; set; }
        public bool SalesBranchPage { get; set; }
        public bool VDateInTime { get; set; }
        public bool Storef { get; set; }// Initialize your model property
        public bool FoodCategory { get; set; }
        public bool VCompany { get; set; }
        public bool VDateandtime { get; set; }
        public bool Area { get; set; }
        public bool SystemAnaysis { get; set; }
        public bool ItemMovements { get; set; }
        public string Messagefordev { get; set; }
        public bool Home { get; set; }
        public bool Dashboard { get; set; }
        public bool CompareData { get; set; }
        public string Offer { get; set; }
        //
        public bool Cat { get; set; }// Initialize your model property
        public bool Shelf { get; set; }
        public bool Inventlocation { get; set; }
        public string DB { get; set; }
        public bool OfferPage { get; set; }
        public string? YearinDash { get; set; }
        public string InvoiceStatus { get; set; }
        public string TransferMessage { get; set; }
        public bool VYearVinDash { get; set; }
        public int ManagerReply { get; set; }
        public bool Approve { get; set; }
        public bool UnApprove { get; set; }
        public string RptSalesAlllive()
        {
            string RptSalesAllView = @"select DpID COLLATE SQL_Latin1_General_CP1_CI_AS DpID ,
                                    DpName COLLATE SQL_Latin1_General_CP1_CI_AS DpName,
                                    convert(varchar(20),StoreId) StoreId,
                                    convert(varchar(20),StoreId) StoreIdR,
                                    StoreName COLLATE SQL_Latin1_General_CP1_CI_AS StoreName
                                    ,StoreFranchise COLLATE SQL_Latin1_General_CP1_CI_AS StoreFranchise,ItemLookupCode,
                                    ItemName COLLATE SQL_Latin1_General_CP1_CI_AS ItemName ";
            if (VTransactionCount && (!VStoreId && !VStoreName))
            {
                RptSalesAllView += @",TransactionsCount";
            }
            if (VDateInTime || VDateandtime)
            {
                RptSalesAllView += ",Convert(nvarchar(20),Time)Time,Convert(nvarchar(20),Dateandtime)Dateandtime ";
            }
            RptSalesAllView += @",
                                    Cast(TransDate as date)TransDate,qty,price,TotalSales,TransactionNumber COLLATE SQL_Latin1_General_CP1_CI_AS TransactionNumber,Cost 
                                     ,ByMonth,ByYear,ByDay,0 As StoreIdD,Tax,TotalSalesTax,TotalSalesWithoutTax,(TotalCostQty)TotalCostQty
                                     ,DManager COLLATE SQL_Latin1_General_CP1_CI_AS DManager,FManager COLLATE SQL_Latin1_General_CP1_CI_AS FManager,Username COLLATE SQL_Latin1_General_CP1_CI_AS Username,Company COLLATE SQL_Latin1_General_CP1_CI_AS Company ";
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptSalesAllView += ",SupplierName,SupplierCode ";
            }
            RptSalesAllView += @"from(" + RmsRptSalesView() + ")RptAXDBSalesAll" +
                                      @"
                                    union all
                                    select DpId,
                                    DpName COLLATE SQL_Latin1_General_CP1_CI_AS DpName,
                                    StoreID as StoreID,
                                    '0' as StoreID,
                                    StoreName StoreNameR,
                                    StoreFranchise ,
                                    ItemLookupCode COLLATE SQL_Latin1_General_CP1_CI_AS,ItemName COLLATE SQL_Latin1_General_CP1_CI_AS ItemName ";
            if (VDateInTime || VDateandtime)
            {
                RptSalesAllView += @" ,Time,DateandTime ";
            }
            RptSalesAllView += @"
            ,Cast(TransDate as date)TransDate,Qty,Price,TotalSales,TransactionNumber,Cost
                                    ,ByMonth,ByYear,ByDay,convert(varchar(20),StoreId) StoreIdD,Tax,TotalSalesTax,TotalSalesWithoutTax,(TotalCostQty)TotalCostQty
                                    ,s.DManager,s.FManager,s.Username,Case when StoreId='143' then 'Trade Mix' else 'TMT' end As Company ";
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptSalesAllView += ",SupplierName,SupplierCode ";
            }
            RptSalesAllView += @"from (" + linkDyRptSaleslive() + @")r 
                                      left join CkproUsers.dbo.Storeuser s on s.storenumber  =Convert (varchar(20),r.StoreID ) collate SQL_Latin1_General_CP1_CI_AS
";
            return RptSalesAllView;
        }

        public string RptSalesAll()
        {
            string RptSalesAllView = @"select DpID COLLATE SQL_Latin1_General_CP1_CI_AS DpID ,
                                    DpName COLLATE SQL_Latin1_General_CP1_CI_AS DpName,
                                    convert(varchar(20),StoreId) StoreId,
                                    convert(varchar(20),StoreId) StoreIdR,
                                    StoreName COLLATE SQL_Latin1_General_CP1_CI_AS StoreName
                                    ,StoreFranchise COLLATE SQL_Latin1_General_CP1_CI_AS StoreFranchise,ItemLookupCode,
                                    ItemName COLLATE SQL_Latin1_General_CP1_CI_AS ItemName ";
            if (VTransactionCount && (!VStoreId && !VStoreName))
            {
                RptSalesAllView += @",TransactionsCount";
            }
            if (VDateInTime || VDateandtime)
            {
                RptSalesAllView += ",Convert(nvarchar(20),Time)Time,Convert(nvarchar(20),Dateandtime)Dateandtime ";
            }
            RptSalesAllView += @",
                                    Cast(TransDate as date)TransDate,qty,price,TotalSales,TransactionNumber COLLATE SQL_Latin1_General_CP1_CI_AS TransactionNumber,Cost 
                                     ,ByMonth,ByYear,ByDay,0 As StoreIdD,Tax,TotalSalesTax,TotalSalesWithoutTax,(TotalCostQty)TotalCostQty
                                     ,DManager COLLATE SQL_Latin1_General_CP1_CI_AS DManager,FManager COLLATE SQL_Latin1_General_CP1_CI_AS FManager,Username COLLATE SQL_Latin1_General_CP1_CI_AS Username,Company COLLATE SQL_Latin1_General_CP1_CI_AS Company ";
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptSalesAllView += ",SupplierName,SupplierCode ";
            }
            RptSalesAllView += @"from(" + linkRmsRptSalesView() + ")RptAXDBSalesAll" +
                                      @"
                                    union all
                                    select DpId,
                                    DpName COLLATE SQL_Latin1_General_CP1_CI_AS DpName,
                                    StoreID as StoreID,
                                    '0' as StoreID,
                                    StoreName StoreNameR,
                                    StoreFranchise ,
                                    ItemLookupCode COLLATE SQL_Latin1_General_CP1_CI_AS,ItemName COLLATE SQL_Latin1_General_CP1_CI_AS ItemName ";
            if (VTransactionCount && (!VStoreId && !VStoreName && !VItemLookupCode && !VItemName && !VTransactionNumber && !VSupplierName && !VSupplierId && !VCompany))
            {
                RptSalesAllView += @",(select sum(trans1)from
(
select storeid ,count(distinct TransactionNumber)Trans1 from AXDBSalesAll
where cast(TRANSDATE as date) BETWEEN @fromDate AND @toDate
group by storeid 
)s)TransactionsCount";
            }
            if (VDateInTime || VDateandtime)
            {
                RptSalesAllView += @",RIGHT('0' + CONVERT(VARCHAR, DATEPART(HOUR, DATEADD(SECOND, Dateintime, Dateintime))), 2)
    + ':'
    + RIGHT('0' + CONVERT(VARCHAR, DATEPART(MINUTE, DATEADD(SECOND,Dateintime, Dateintime))), 2)
    + ' '
    + CASE 
        WHEN DATEPART(HOUR, DATEADD(SECOND,Dateintime, Dateintime)) >= 12 THEN 'PM'
        ELSE 'AM' END as Time,Concat(cast(TransDate as date),'  ',RIGHT('0' + CONVERT(VARCHAR, DATEPART(HOUR, DATEADD(SECOND, Dateintime, Dateintime))), 2)
    + ':'
    + RIGHT('0' + CONVERT(VARCHAR, DATEPART(MINUTE, DATEADD(SECOND,Dateintime, Dateintime))), 2)
    + ' '
    + CASE 
        WHEN DATEPART(HOUR, DATEADD(SECOND,Dateintime, Dateintime)) >= 12 THEN 'PM'
        ELSE 'AM' END)DateandTime ";
            }
            RptSalesAllView += @"
            ,Cast(TransDate as date)TransDate,Qty,Price,TotalSales,TransactionNumber,Cost
                                    ,ByMonth,ByYear,ByDay,convert(varchar(20),StoreId) StoreIdD,Tax,TotalSalesTax,TotalSalesWithoutTax,(TotalCostQty)TotalCostQty
                                    ,s.DManager,s.FManager,s.Username,Case when StoreId='143' then 'Trade Mix' else 'TMT' end As Company ";
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptSalesAllView += ",SupplierName,SupplierCode ";
            }
            RptSalesAllView += @"from AXDBSalesAll r
                                      left join Storeuser s on s.storenumber  =Convert (varchar(20),r.StoreID ) collate SQL_Latin1_General_CP1_CI_AS
where r.SalesStatus!='0' 
";
            return RptSalesAllView;
        }
        //Sales Views
        //RMS
        public string linkRmsRptSalesView()
        {
            string RptSaleslinkView = @"
                                    select 
                                    dp.code DpID,dp.ID GroupId, dp.Name DpName,
                                    Stores.ID StoreCode ,T.StoreID StoreID,Stores.LOCATION StoreName,Stores.FRANCHISE StoreFranchise,
                                    T.ItemID ItemID,Item.Description ItemName,Item.ItemLookupCode,
                                    T.TransactionTime Time,T.TransactionTime DateandTime,
                                    Day(T.TransactionTime) ByDay,Month(T.TransactionTime) ByMonth,Year(T.TransactionTime)ByYear, Cast(T.TransactionTime as date) TransDate,T.Quantity Qty,T.Price Price,(T.Quantity * T.Price) TotalSales,
                                    convert(varchar(20),T.TransactionNumber)TransactionNumber,T.Cost,-((T.Cost)*-(T.Quantity)) TotalCostQty,((T.Quantity*T.Price)-(T.Cost*T.Quantity))Profit,T.SalesTax Tax,
                                    (T.Quantity*T.Price)TotalSalesTax,((T.Quantity*T.Price)-T.SalesTax)TotalSalesWithoutTax,((T.Quantity*T.Cost)-T.SalesTax)TotalCostWithoutTax
                                    ,Convert (varchar(20),item.SupplierId )SupplierId,s.Franchise Company";
            if (VTransactionCount && (!VStoreId && !VStoreName && !VItemLookupCode && !VItemName && !VTransactionNumber && !VSupplierName && !VSupplierId && !VCompany))
            {
                RptSaleslinkView += @",(select sum(trans1)from
(
select storeid ,count(distinct TransactionNumber)Trans1 from [192.168.1.156].DATA_CENTER.dbo.TransactionEntry
where cast(TransactionTime as date) BETWEEN @fromDate AND @toDate
group by storeid 
)s)TransactionsCount";
            }
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptSaleslinkView += ",Sub.SupplierName,Sub.Code SupplierCode";
            }
            RptSaleslinkView += @"
                                    ,s.DManager,s.Username,s.FManager from [192.168.1.156].DATA_CENTER.dbo.TransactionEntry T 
                                    left join  [192.168.1.156].DATA_CENTER.dbo.Item on Item.ID=T.ItemID and T.StoreID=Item.storeid
                                    left join  [192.168.1.156].DATA_CENTER.dbo.department dp on dp.ID=Item.DepartmentID and dp.storeid=Item.storeid
                                    left join  [192.168.1.156].DATA_CENTER.dbo.Stores on (Stores.STORE_ID=Item.storeid or Item.storeid is null) and Stores.STORE_ID=T.StoreID
                                    left join  [192.168.1.156].CkproUsers.dbo.Storeuser s on s.RMSstoNumber=Convert (varchar(20),T.StoreID )";
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptSaleslinkView += " left join  [192.168.1.156].DATA_CENTER.dbo.Supplier Sub on  Sub.storeid=stores.STORE_ID and Sub.id=item.SupplierID";
            }
            return RptSaleslinkView;
        }
        public string RmsRptSalesView()
        {
            string RptSalesView = @"
                                    select 
                                    dp.code DpID,dp.ID GroupId, dp.Name DpName,
                                    Stores.ID StoreCode ,Convert (varchar(20),T.StoreID) StoreID,Stores.LOCATION StoreName,Stores.FRANCHISE StoreFranchise,
                                    T.ItemID ItemID,Item.Description ItemName,Item.ItemLookupCode,
                                    T.TransactionTime Time,T.TransactionTime DateandTime,
                                    Day(T.TransactionTime) ByDay,Month(T.TransactionTime) ByMonth,Year(T.TransactionTime)ByYear, Cast(T.TransactionTime as date) TransDate,T.Quantity Qty,T.Price Price,(T.Quantity * T.Price) TotalSales,
                                    convert(varchar(20),T.TransactionNumber)TransactionNumber,T.Cost,-((T.Cost)*-(T.Quantity)) TotalCostQty,((T.Quantity*T.Price)-(T.Cost*T.Quantity))Profit,T.SalesTax Tax,
                                    (T.Quantity*T.Price)TotalSalesTax,((T.Quantity*T.Price)-T.SalesTax)TotalSalesWithoutTax,((T.Quantity*T.Cost)-T.SalesTax)TotalCostWithoutTax
                                    ,Convert (varchar(20),item.SupplierId )SupplierId,s.Franchise Company";
            if (VTransactionCount && (!VStoreId && !VStoreName && !VItemLookupCode && !VItemName && !VTransactionNumber && !VSupplierName && !VSupplierId && !VCompany))
            {
                RptSalesView += @",(select sum(trans1)from
(
select storeid ,count(distinct TransactionNumber)Trans1 from DATA_CENTER.dbo.TransactionEntry
where cast(TransactionTime as date) BETWEEN @fromDate AND @toDate
group by storeid 
)s)TransactionsCount";
            }
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptSalesView += ",Sub.SupplierName,Sub.Code SupplierCode";
            }
            RptSalesView += @"
                                    ,s.DManager,s.Username,s.FManager from DATA_CENTER.dbo.TransactionEntry T 
                                    left join DATA_CENTER.dbo.Item on Item.ID=T.ItemID and T.StoreID=Item.storeid
                                    left join DATA_CENTER.dbo.department dp on dp.ID=Item.DepartmentID and dp.storeid=Item.storeid
                                    left join DATA_CENTER.dbo.Stores on (Stores.STORE_ID=Item.storeid or Item.storeid is null) and Stores.STORE_ID=T.StoreID
                                    left join CkproUsers.dbo.Storeuser s on s.RMSstoNumber=Convert (varchar(20),T.StoreID )";
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptSalesView += " left join DATA_CENTER.dbo.Supplier Sub on  Sub.storeid=stores.STORE_ID and Sub.id=item.SupplierID";
            }
            return RptSalesView;
        }
        public string linkDyRptSaleslive()
        {
            string query = @"
Select SalesD.TRANSACTIONID TransactionNumber,SalesH.STORE StoreID,SalesD.costamount Cost,Store.username StoreName
,SalesD.ITEMID ItemLookupCode,-TAXAMOUNT Tax,
Day(SalesH.TRANSDATE) ByDay,Month(SalesH.TRANSDATE) ByMonth,Year(SalesH.TRANSDATE)ByYear,
SalesH.TRANSTIME TransTime,SalesH.TRANSDATE As TransDate,-SalesD.Qty Qty,-(SalesD.COSTAMOUNT*SalesD.Qty)TotalCostQty,
SalesD.Price Price,-(SalesD.NETAMOUNTINCLTAX) TotalSales,-(SalesD.NETAMOUNTINCLTAX) TotalSalesTax, -(SalesD.NETAMOUNT) TotalSalesWithoutTax,Case when SalesH.STORE='143' then 'SUB-FRANCHISE' else 'TMT'
end As StoreFranchise,DManager,Username,FManager ,SalesH.TRANSTIME DateIntime,'TMT' Company ";
            if (VTransactionCount && (!VStoreId && !VStoreName && !VItemLookupCode && !VItemName && !VTransactionNumber && !VSupplierName && !VSupplierId && !VCompany))
            {
                query += @",(select sum(trans1)from
(
select SalesH.STORE storeid ,count(distinct SalesD.TRANSACTIONID)Trans1 from AXDB.dbo.RetailTransactiontable SalesH
INNER JOIN AXDB.dbo.RETAILTRANSACTIONSALESTRANS SalesD ON SalesH.TRANSACTIONID = SalesD.TRANSACTIONID
where cast(SalesH.TRANSDATE as date) BETWEEN @fromDate AND @toDate and SalesH.ENTRYSTATUS!=1  and SalesH.TYPE=2
group by STORE 
)s)TransactionsCount";
            }
            if (VDateInTime || VDateandtime)
            {
                query += @",RIGHT('0' + CONVERT(VARCHAR, DATEPART(HOUR, DATEADD(SECOND, SalesH.TRANSTIME, SalesH.TRANSTIME))), 2)
    + ':'
    + RIGHT('0' + CONVERT(VARCHAR, DATEPART(MINUTE, DATEADD(SECOND,SalesH.TRANSTIME, SalesH.TRANSTIME))), 2)
    + ' '
    + CASE 
        WHEN DATEPART(HOUR, DATEADD(SECOND,SalesH.TRANSTIME, SalesH.TRANSTIME)) >= 12 THEN 'PM'
        ELSE 'AM' END as Time,Concat(cast(SalesH.TransDate as date),'  ',RIGHT('0' + CONVERT(VARCHAR, DATEPART(HOUR, DATEADD(SECOND, SalesH.TRANSTIME, SalesH.TRANSTIME))), 2)
    + ':'
    + RIGHT('0' + CONVERT(VARCHAR, DATEPART(MINUTE, DATEADD(SECOND,SalesH.TRANSTIME, SalesH.TRANSTIME))), 2)
    + ' '
    + CASE 
        WHEN DATEPART(HOUR, DATEADD(SECOND,SalesH.TRANSTIME, SalesH.TRANSTIME)) >= 12 THEN 'PM'
        ELSE 'AM' END)DateandTime ";
            }
            query += ",Cat.CODE DpId,Cat.NAME DpName,Cat.RECID CategoryId,It.[NAME] ItemName,Inv.PRODUCT ";
            query += @",Inv.Primaryvendorid SupplierName,INV.[Primaryvendorid] SupplierCode ";
            query += @"
from [192.168.1.210].AXDB.dbo.RetailTransactiontable SalesH
INNER JOIN [192.168.1.210].AXDB.dbo.RETAILTRANSACTIONSALESTRANS SalesD ON SalesH.TRANSACTIONID = SalesD.TRANSACTIONID
INNER JOIN  (Select distinct INV.PRODUCT,INV.ITEMID,INV.[Primaryvendorid] from [192.168.1.210].AXDB.dbo.[Inventtable] Inv where INV.[DATAAREAID] = 'tmt')Inv on SalesD.ITEMID = INV.ITEMID
inner JOIN  (Select distinct It.PRODUCT,It.[NAME] from [192.168.1.210].AXDB.dbo.[Ecoresproducttranslation] as It)It on INV.PRODUCT = It.PRODUCT";
            query += " INNER JOIN  (Select distinct Cat.ReCId,Cat.CODE,Cat.NAME from [192.168.1.210].AXDB.dbo.EcoResCategory Cat)Cat ON Cat.ReCId = SalesD.Categoryid ";
            query += @"   
left JOIN [192.168.1.210].AXDBTest.dbo.Storeusers Store ON Store.storenumber collate SQL_Latin1_General_CP1_CI_AS = SalesH.Store
where SalesH.ENTRYSTATUS!=1  and SalesH.TYPE=2 and salesd.INVENTSTATUSSALES<>'0' 
";
            return query;
        }
        public string DyRptSaleslive()
        {
            string query = @"
Select SalesD.TRANSACTIONID TransactionNumber,SalesH.STORE StoreID,SalesD.costamount Cost,Store.username StoreName
,SalesD.ITEMID ItemLookupCode,-TAXAMOUNT Tax,
Day(SalesH.TRANSDATE) ByDay,Month(SalesH.TRANSDATE) ByMonth,Year(SalesH.TRANSDATE)ByYear,
SalesH.TRANSTIME TransTime,SalesH.TRANSDATE As TransDate,-SalesD.Qty Qty,-(SalesD.COSTAMOUNT*SalesD.Qty)TotalCostQty,
SalesD.Price Price,-(SalesD.NETAMOUNTINCLTAX) TotalSales,-(SalesD.NETAMOUNTINCLTAX) TotalSalesTax, -(SalesD.NETAMOUNT) TotalSalesWithoutTax,Case when SalesH.STORE='143' then 'SUB-FRANCHISE' else 'TMT'
end As StoreFranchise,DManager,Username,FManager ,SalesH.TRANSTIME DateIntime,'TMT' Company ";
            if (VTransactionCount && (!VStoreId && !VStoreName && !VItemLookupCode && !VItemName && !VTransactionNumber && !VSupplierName && !VSupplierId && !VCompany))
            {
                query += @",(select sum(trans1)from
(
select SalesH.STORE storeid ,count(distinct SalesD.TRANSACTIONID)Trans1 from AXDB.dbo.RetailTransactiontable SalesH
INNER JOIN AXDB.dbo.RETAILTRANSACTIONSALESTRANS SalesD ON SalesH.TRANSACTIONID = SalesD.TRANSACTIONID
where cast(SalesH.TRANSDATE as date) BETWEEN @fromDate AND @toDate and SalesH.ENTRYSTATUS!=1  and SalesH.TYPE=2
group by STORE 
)s)TransactionsCount";
            }
            if (VDateInTime || VDateandtime)
            {
                query += @",RIGHT('0' + CONVERT(VARCHAR, DATEPART(HOUR, DATEADD(SECOND, Dateintime, Dateintime))), 2)
    + ':'
    + RIGHT('0' + CONVERT(VARCHAR, DATEPART(MINUTE, DATEADD(SECOND,Dateintime, Dateintime))), 2)
    + ' '
    + CASE 
        WHEN DATEPART(HOUR, DATEADD(SECOND,Dateintime, Dateintime)) >= 12 THEN 'PM'
        ELSE 'AM' END as Time,Concat(cast(TransDate as date),'  ',RIGHT('0' + CONVERT(VARCHAR, DATEPART(HOUR, DATEADD(SECOND, Dateintime, Dateintime))), 2)
    + ':'
    + RIGHT('0' + CONVERT(VARCHAR, DATEPART(MINUTE, DATEADD(SECOND,Dateintime, Dateintime))), 2)
    + ' '
    + CASE 
        WHEN DATEPART(HOUR, DATEADD(SECOND,Dateintime, Dateintime)) >= 12 THEN 'PM'
        ELSE 'AM' END)DateandTime ";
            }
            if (VDepartment || VDpId || ((Department != null && Department != "0") && Department != "0"))
            {
                query += ",Cat.CODE DpId,Cat.NAME DpName,Cat.RECID CategoryId ";
            }
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0") || VItemLookupCode || VItemName || ItemNameTxt != null || ItemLookupCodeTxt != null)
            {
                query += @",Inv.Primaryvendorid SupplierName,INV.[Primaryvendorid] SupplierCode,It.[NAME] ItemName,Inv.PRODUCT ";
            }
            query += @"
from AXDB.dbo.RetailTransactiontable SalesH
INNER JOIN AXDB.dbo.RETAILTRANSACTIONSALESTRANS SalesD ON SalesH.TRANSACTIONID = SalesD.TRANSACTIONID ";
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0") || VItemLookupCode || VItemName || ItemNameTxt != null || ItemLookupCodeTxt != null)
            {
                query += @"INNER JOIN  (Select distinct INV.PRODUCT,INV.ITEMID,INV.[Primaryvendorid] from AXDB.dbo.[Inventtable] Inv where INV.[DATAAREAID] = 'tmt')Inv on SalesD.ITEMID = INV.ITEMID
inner JOIN  (Select distinct It.PRODUCT,It.[NAME] from AXDB.dbo.[Ecoresproducttranslation] as It)It on INV.PRODUCT = It.PRODUCT ";
            }
            if (VDepartment || VDpId || (Department != null && Department != "0"))
            {
                query += " INNER JOIN  (Select distinct Cat.ReCId,Cat.CODE,Cat.NAME from AXDB.dbo.EcoResCategory Cat)Cat ON Cat.ReCId = SalesD.Categoryid ";
            }
            query += @"   
left JOIN AXDBTest.dbo.Storeusers Store ON Store.storenumber collate SQL_Latin1_General_CP1_CI_AS = SalesH.Store
--left JOIN (Select Code ,SupplierName from [192.168.1.156].DATA_CENTER.dbo.Supplier) Sub ON Sub.Code collate SQL_Latin1_General_CP1_CI_AS=INV.[Primaryvendorid]
--left join SALESLINE on SalesLine.ITEMID=SalesD.ITEMID and SALESLINE.Confirmeddlv=SalesH.TRANSDATE
where SalesH.ENTRYSTATUS!=1  and SalesH.TYPE=2 and salesd.INVENTSTATUSSALES<>'0' 
";
            return query;
        }
        //Dynamic
        public string DyRptAXDBSalesView()
        {
            string query = @"select sales.*,Case when StoreId='143' then 'Trade Mix' else 'TMT' end As Company ,s.DManager,s.Username,s.FManager ";
            if (VTransactionCount && (!VStoreId && !VStoreName && !VItemLookupCode && !VItemName && !VTransactionNumber && !VSupplierName && !VSupplierId && !VCompany))
            {
                query += @",(select sum(trans1)from
(
select storeid ,count(distinct TransactionNumber)Trans1 from AXDBSalesAll
where cast(TRANSDATE as date) BETWEEN @fromDate AND @toDate 
group by storeid 
)s)TransactionsCount";
            }
            if (VDateInTime || VDateandtime)
            {
                query += @",RIGHT('0' + CONVERT(VARCHAR, DATEPART(HOUR, DATEADD(SECOND, Dateintime, Dateintime))), 2)
    + ':'
    + RIGHT('0' + CONVERT(VARCHAR, DATEPART(MINUTE, DATEADD(SECOND,Dateintime, Dateintime))), 2)
    + ' '
    + CASE 
        WHEN DATEPART(HOUR, DATEADD(SECOND,Dateintime, Dateintime)) >= 12 THEN 'PM'
        ELSE 'AM' END as Time,Concat(cast(TransDate as date),'  ',RIGHT('0' + CONVERT(VARCHAR, DATEPART(HOUR, DATEADD(SECOND, Dateintime, Dateintime))), 2)
    + ':'
    + RIGHT('0' + CONVERT(VARCHAR, DATEPART(MINUTE, DATEADD(SECOND,Dateintime, Dateintime))), 2)
    + ' '
    + CASE 
        WHEN DATEPART(HOUR, DATEADD(SECOND,Dateintime, Dateintime)) >= 12 THEN 'PM'
        ELSE 'AM' END)DateandTime ";
            }
            query += @" from AXDBSalesAll sales left join Storeuser S on S.storenumber= Sales.StoreID
where sales.SalesStatus !='0' 
";
            return query;
        }
        //RMS Before
        public string RmsbeforeRptSalesView()
        {
            string RptSalesView = @"
                                    select 
                                    dp.code DpID,dp.ID GroupId, dp.Name DpName,
                                    Stores.ID StoreCode ,Convert (varchar(20),T.StoreID) StoreID,Stores.LOCATION StoreName,Stores.FRANCHISE StoreFranchise,
                                    T.ItemID ItemID,Item.Description ItemName,Item.ItemLookupCode,
                                    T.TransactionTime Time,T.TransactionTime DateandTime,
                                    Day(T.TransactionTime) ByDay,Month(T.TransactionTime) ByMonth,Year(T.TransactionTime)ByYear, Cast(T.TransactionTime as date) TransDate,T.Quantity Qty,T.Price Price,(T.Quantity * T.Price) TotalSales,
                                    convert(varchar(20),T.TransactionNumber)TransactionNumber,T.Cost,-((T.Cost)*-(T.Quantity)) TotalCostQty,((T.Quantity*T.Price)-(T.Cost*T.Quantity))Profit,T.SalesTax Tax,
                                    (T.Quantity*T.Price)TotalSalesTax,((T.Quantity*T.Price)-T.SalesTax)TotalSalesWithoutTax,((T.Quantity*T.Cost)-T.SalesTax)TotalCostWithoutTax
                                    ,Convert (varchar(20),item.SupplierId )SupplierId,s.Franchise Company";
            if (VTransactionCount && (!VStoreId && !VStoreName && !VItemLookupCode && !VItemName && !VTransactionNumber && !VSupplierName && !VSupplierId && !VCompany))
            {
                RptSalesView += @",(select sum(trans1)from
(
select storeid ,count(distinct TransactionNumber)Trans1 from DATA_CENTER_Prev_Yrs.dbo.TransactionEntry
where cast(TransactionTime as date) BETWEEN @fromDate AND @toDate
group by storeid 
)s)TransactionsCount";
            }
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptSalesView += ",Sub.SupplierName,Sub.Code SupplierCode";
            }
            RptSalesView += @"
                                    ,s.DManager,s.Username,s.FManager from DATA_CENTER_Prev_Yrs.dbo.TransactionEntry T 
                                    left join DATA_CENTER_Prev_Yrs.dbo.Item on Item.ID=T.ItemID and T.StoreID=Item.storeid
                                    left join DATA_CENTER_Prev_Yrs.dbo.department dp on dp.ID=Item.DepartmentID and dp.storeid=Item.storeid
                                    left join DATA_CENTER_Prev_Yrs.dbo.Stores on (Stores.STORE_ID=Item.storeid or Item.storeid is null) and Stores.STORE_ID=T.StoreID
                                    left join CkproUsers.dbo.Storeuser s on s.RMSstoNumber=Convert (varchar(20),T.StoreID )";
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptSalesView += " left join DATA_CENTER_Prev_Yrs.dbo.Supplier Sub on  Sub.storeid=stores.STORE_ID and Sub.id=item.SupplierID";
            }
            return RptSalesView;
        }
        //RichCut
        public string RichCutRptSales = @"
SELECT Day(TransactionTime) ByDay,Month(TransactionTime) ByMonth,Year(TransactionTime)ByYear,s.Name StoreName, TRANS.[Cost] ,s.RMSstoNumber [StoreID] ,[TransactionNumber] ,TRANS.[Price] ,TRANS.[Quantity] Qty,(TRANS.Price * TRANS.Quantity)TotalSales
                                     , Cast(TransactionTime as date)Transdate , It.[Description] ItemName, It.[ItemLookupCode],TransactionTime Time ,TransactionTime DateandTime
                                   ,Dep.[Name] DpName ,Supp.Code SupplierCode, Supp.SupplierName,s.DManager,s.FManager,s.Username,'SUB-FRANCHISE' StoreFranchise,
								   -((TRANS.Cost)*-(TRANS.Quantity)) TotalCostQty,((TRANS.Quantity*TRANS.Price)-(TRANS.Cost*TRANS.Quantity))Profit,TRANS.SalesTax Tax,
                                    (TRANS.Quantity*TRANS.Price)TotalSalesTax,((TRANS.Quantity*TRANS.Price)-TRANS.SalesTax)TotalSalesWithoutTax,((TRANS.Quantity*TRANS.Cost)-TRANS.SalesTax)TotalCostWithoutTax
                                    ,Convert (varchar(20),It.SupplierId )SupplierId ,s.Franchise Company 
									FROM [RichCut_DATA_CENTER].[dbo].[TransactionEntry] AS TRANS
                                    left JOIN [RichCut_DATA_CENTER].[dbo].[Item] AS It ON It.ID = TRANS.ItemID AND It.storeid = TRANS.StoreID
                                    left JOIN [RichCut_DATA_CENTER].[dbo].[department] AS Dep ON It.DepartmentID = Dep.ID AND It.storeid = Dep.storeid
                                    left JOIN [RichCut_DATA_CENTER].[dbo].[SupplierList] AS SuppL ON It.storeid = SuppL.storeid AND It.SupplierID = SuppL.SupplierID AND It.ID = SuppL.ItemID
                                   left JOIN [RichCut_DATA_CENTER].[dbo].[Supplier] AS Supp ON SuppL.SupplierID = Supp.ID AND SuppL.storeid = Supp.storeid
								   inner join (select distinct Franchise,DManager,FManager,Username,Name,RMSstoNumber from CkproUsers.dbo.Storeuser where Name='RichCut' or Name = 'RichCut_No' or Name = 'RichCut_Zayed') s on (SUBSTRING(s.RMSstoNumber, 2, 1)=Convert(varchar(5),It.[StoreID])) 
where It.[ItemLookupCode] LIKE '[0-9]%' ";

        public string RptAXDBSalesViewCreate = @"
Create View [dbo].[RptAXSales] As
--drop view [RptAXSales]
Select  SalesD.TRANSACTIONID TransactionNumber,SalesH.STORE StoreID,SalesD.costamount Cost,SalesH.STORE StoreName
,SalesD.ITEMID ItemLookupCode,-TAXAMOUNT Tax,
Day(SalesH.TRANSDATE) ByDay,Month(SalesH.TRANSDATE) ByMonth,Year(SalesH.TRANSDATE)ByYear,
SalesH.TRANSTIME TransTime,SalesH.TRANSDATE As TransDate,-SalesD.Qty Qty,-(SalesD.COSTAMOUNT*SalesD.Qty)TotalCostQty,
SalesD.Price Price,-(SalesD.NETAMOUNTINCLTAX) TotalSales,-(SalesD.NETAMOUNTINCLTAX) TotalSalesTax, -(SalesD.NETAMOUNT) TotalSalesWithoutTax,Case when SalesH.STORE='143' then 'SUB-FRANCHISE' else 'TMT' end As StoreFranchise ,
Cat.CODE DpId,Cat.NAME DpName ,[Primaryvendorid] SupplierName
, INV.[Primaryvendorid] SupplierCode
,It.[NAME] ItemName,SalesH.TRANSTIME DateIntime
 ,Inv.PRODUCT,Cat.RECID CategoryId,salesd.INVENTSTATUSSALES SalesStatus,Salesd.lineNum,SALESD.transactionStatus
from  [192.168.1.210].AXDB.dbo.RetailTransactiontable SalesH
INNER JOIN [192.168.1.210].AXDB.dbo.RETAILTRANSACTIONSALESTRANS SalesD ON SalesH.TRANSACTIONID = SalesD.TRANSACTIONID
INNER JOIN  (Select distinct INV.PRODUCT,INV.ITEMID,INV.[Primaryvendorid] from [192.168.1.210].AXDB.dbo.[Inventtable] Inv where INV.[DATAAREAID] = 'tmt')Inv on SalesD.ITEMID = INV.ITEMID
inner JOIN  (Select distinct It.PRODUCT,It.[NAME] from [192.168.1.210].AXDB.dbo.[Ecoresproducttranslation] as It)It on INV.PRODUCT = It.PRODUCT  
INNER JOIN  (Select distinct Cat.ReCId,Cat.CODE,Cat.NAME from [192.168.1.210].AXDB.dbo.EcoResCategory Cat)Cat ON Cat.ReCId = SalesD.Categoryid
--left JOIN [192.168.1.156].CkproUsers.dbo.Storeuser Store ON Store.storenumber = SalesH.Store
--left JOIN (Select Code ,SupplierName from [192.168.1.156].DATA_CENTER.dbo.Supplier) Sub ON Sub.Code collate SQL_Latin1_General_CP1_CI_AS=INV.[Primaryvendorid]
--left join SALESLINE on SalesLine.ITEMID=SalesD.ITEMID and SALESLINE.Confirmeddlv=SalesH.TRANSDATE
where SalesH.ENTRYSTATUS!=1  and SalesH.TYPE=2  --and salesd.INVENTSTATUSSALES<>'0'--and SalesLine.[DATAAREAID] = 'tmt'
";

        // Arrange this
        //Sales ALL
        public string RptSystemAnalysis = @"
Select * from RptBranchesAnalysis ";
        //Price
        public string RichCutItemPrice = @"select distinct Item.StoreId, Item.Description ItemName,max(Item.Price)Price,Item.ItemLookupCode,''Dmanager,''username,''Fmanager from RichCut_DATA_CENTER.dbo.Item
group by Item.storeid, Item.Description,ItemLookupCode ";
        public string RmsRptItemPrice = @"select distinct Item.StoreId, Item.Description ItemName,max(Item.Price)Price,Item.ItemLookupCode,''Dmanager,''username,''Fmanager from Item
group by Item.storeid, Item.Description,ItemLookupCode";
        public string DyRptItemPrice = @"select Pr.ACCOUNTRELATION Dep,Eco.NAME ItemName, Itable.itemid Itemlookupcode,Pr.Price,''Dmanager,''username,''Fmanager from Inventtable Itable
inner join Ecoresproducttranslation Eco on Eco.PRODUCT=Itable.PRODUCT
inner join (Select distinct ITEMRELATION,Pr.AMOUNT Price,Accountrelation from PRICEDISCTABLE Pr where pr.Todate= '1900-01-01' 
and pr.Dataareaid = 'tmt')Pr on Pr.ITEMRELATION=Itable.ITEMID
";

        //Offer
        public string RptOffer = @" select distinct OfferName,Tid,Dealprice,Discountamount,tr.Transdate,
 Periodicdiscountofferid OfferId, Validfrom, Validto ,tr.Qty,tr.Periodicdiscamount Discount,tr.Itemid ItemLookupCode,tr.Netamountincltax TotalSales,tr.Transactionid TransactionNumber,it.Name ItemName,
tr.Store StoreId,s.username Storename
from 
Retailtransactionsalestrans tr
inner join  Retailtransactiontable re on tr.Transactionid = re.Transactionid
left join  Inventtable inv on tr.Itemid = inv.Itemid
left join Ecoresproducttranslation  it  on inv.Product = it.Product
left join (select distinct storenumber,username from  AXDBTest.dbo.storeusers) s on s.storenumber collate SQL_Latin1_General_CP1_CI_AS=tr.store
inner join (
 select  distinct OffName.Name OfferName,disc.Terminalid Tid, disc.Transactionid, disc.Dealprice, disc.Discountamount, it.Name, 
 disc.Periodicdiscountofferid, OffName.Validfrom, OffName.Validto  
from  Retailtransactiondiscounttrans disc
inner join  Retailperiodicdiscount OffName on disc.Periodicdiscountofferid = OffName.Offerid
left join  Retailperiodicdiscountline  it  on disc.Periodicdiscountofferid = it.Offerid
)R1 on R1.Transactionid=tr.Transactionid and R1.Name=it.Name
 where re.Entrystatus != 1 and re.Type = 2 and tr.Periodicdiscamount != 0 and 
cast(tr.Transdate as date) between  @fromDate and @toDate ";

        public string RptItemMovements = @"
  select  distinct Store_Name,  Storefrom,  Quantity,ItemName,ItemlookupCode,Receivedate,Transactions,Franchise,Category ,Terminal,Account
   from (
 select  distinct Trtable.Inventlocationidto Store_Name, Trtable.Inventlocationidfrom Storefrom,  Trline.Qtyreceived*-1 Quantity,
It.Name ItemName,ca.Itemid ItemlookupCode,Trline.Receivedate,Trline.Transferid Transactions,
Case when Trline.Remainstatus = 0 then 'RECEIVED' else 'CREATED' end Franchise,
                            Category = catn.Name ,
                          Terminal = Trline.Inventtransid,
                           Account = 'Transfer'     
							from  Inventtransferline Trline
                        inner  join   Inventtransfertable Trtable on Trline.Transferid = Trtable.Transferid
                        left  join   Inventtable ca on Trline.Itemid = ca.Itemid
                        left  join   Ecoresproducttranslation It on ca.Product = It.Product
                        left  join   Ecoresproductcategory cat on ca.Product = cat.Product 
                        left join   Ecorescategory catn on cat.Category = catn.Recid 
                        where (Trtable.Inventlocationidfrom = 'Headoffice' or Trtable.Inventlocationidto = 'Headoffice')
						and Cast(Trline.Receivedate as date) >= @fromDate and Cast(Trline.Receivedate as date) <= @toDate
union all
select distinct Store_Name = Satable.Salesname,
                             Name = Satable.Inventlocationid,
                             Quantity = Saline.Salesqty * -1,
                             Item = It.Name,
                             Barcode = ca.Itemid,
                             Day = Cast(Saline.Confirmeddlv as date),
                             Transactions = Saline.Salesid,
                              Case when Saline.Salesstatus = 1 then 'Created' when  Saline.Salesstatus = 2 then 'Processing' when  Saline.Salesstatus = 3 then 'Delivered' 
							  when  Saline.Salesstatus = 4 then  'Invoiced' else 'UnKnown'
                             end Franchise,
							 Category = Catn.Name ,
                             Terminal = Saline.Inventtransid,
                             Account = 'Sales'            
							 from  Salesline Saline
                         inner join   Salestable Satable on Saline.Salesid = Satable.Salesid
                         left join   Inventtable ca on Saline.Itemid = ca.Itemid
                         left join   Ecoresproducttranslation It on ca.Product = It.Product
                         left join   Ecoresproductcategory cat on ca.Product = cat.Product 
                         left join   Ecorescategory catn on cat.Category = catn.Recid 
                         where Satable.Inventlocationid = 'Headoffice' and Cast(Saline.Confirmeddlv  as date) >= @fromDate and Cast(Saline.Confirmeddlv  as date) <= @toDate
union all
                         select distinct   Putable.Purchname Store_Name,Name = Putable.Inventlocationid,Quantity = Puline.Purchqty,Item = It.Name,
                             Barcode = ca.Itemid,Day = Cast(Puline.Deliverydate as date),Transactions = Puline.Purchid,
							 Case when Puline.Purchstatus = 1 then 'Open Order' when Puline.Purchstatus = 2 then 'Invoiced' 
							 when Puline.Purchstatus = 4  then 'Canceled' else 'UnKnown' end  Franchise,
                             Category = catn.Name ,Terminal = Puline.Inventtransid,Account = 'Purchase' from
							   Purchline Puline
                         inner join   Purchtable Putable on Puline.Purchid = Putable.Purchid
                         left join   Inventtable ca on Puline.Itemid = ca.Itemid
                         left join   Ecoresproducttranslation It on ca.Product = It.Product
                         left join   Ecoresproductcategory cat on ca.Product = cat.Product
                         left join   Ecorescategory catn on cat.Category = catn.Recid 
                         where Putable.Inventlocationid = 'Headoffice'
                          and Cast(Puline.Deliverydate as date) >= @fromDate and Cast(Puline.Deliverydate as date) <= @toDate
union all
 select distinct 
                                   Store_Name = '',
                                   Name = Dim.Inventlocationid,
                                   Quantity = CountJour.Countedqty - CountJour.Inventonhand,
                                   Item = It.Name,
                                   Barcode = ca.Itemid,
                                   Day = Cast(CountJour.Countdate as date),
                                   Transactions = '',
                                   Franchise = '',
                                   Category = catn.Name ,
                                   Terminal = CountJour.Inventdimid,
                                   Account = 'InventCounting'
            from  Inventdim Dim
                               inner join   Inventcountjour CountJour on Dim.Inventdimid = CountJour.Inventdimid
                               left join   Inventtable ca on CountJour.Itemid = ca.Itemid
                               left join   Ecoresproducttranslation It on ca.Product = It.Product
                               left join   Ecoresproductcategory cat on ca.Product = cat.Product 
                               left join   Ecorescategory catn on cat.Category = catn.Recid 
                               where Dim.Inventlocationid = 'Headoffice' and Cast(CountJour.Countdate  as date) >= @fromDate and Cast(CountJour.Countdate  as date) <= @toDate
                               )T
							   where ItemlookupCode is not null 
 ";
        public string RptSyslog = @"  select * from RptSyslog ";
        public string EasySoftUploadold = @"
-- delete from [192.168.1.208\New].[TopSoft].[dbo].[Sales_InvTrans]
  --insert into  [192.168.1.208\New].[TopSoft].[dbo].[Sales_InvTrans]
  insert into  [192.168.1.76\sql2016].[Easysoft].[dbo].[Sales_InvTrans]
  select (select top 1 sor.SortFi+1 from  [192.168.1.76\sql2016].Easysoft.dbo.Sales_Invoice sor order by sor.SortFi desc)
  'In','1',1,'1',s.Location,ROW_NUMBER() OVER (ORDER BY s.Location) AS Sn,0,S.Descript,'',S.Descript,'','1',0,0,0,0,S.cost,s.costa,0,0,0,0,S.Tax,0,0,((S.cost*s.costa)*(1+(tax*0.01))),'1',0,'1900-01-01','1900-01-01','',1,S.cost,0,0,0,0,0,'','',0,'','','',0,'1',0,0

  from (
  select R.ItemCode i,SUBSTRING(st.Salesid, 7, LEN(st.Salesid)) Location,Ra.Code, st.Custaccount Depa, st.Itemid Descript, 
 st.Modifieddatetime TransDate,st.Qtyordered Cost
, st.Lineamount Qty, st.Salesprice CostA,t.tax Tax
from SALESLINE st
inner join (
 select  distinct ItemCode from [192.168.1.156].AXDBTest.dbo.itax
)R on R.ItemCode=st.Itemid
inner join (
select tacu.Code from [192.168.1.156].AXDBTest.dbo.CustInv tacu
)Ra on Ra.Code=st.Custaccount
left join 
(select ta.Tax,ItemCode from  [192.168.1.156].AXDBTest.dbo.itax ta ) t on  st.Itemid = t.ItemCode

--
--
where st.Salesid =  @SalesId 
--and cast(st.Createddatetime as date) = Cast(@CDate as date) and cast(st.Modifieddatetime as date) =cast(getdate() as date)
 and
st.Custgroup = 'Third Part' and st.Recversion != 1 
)S
-- insert into  [192.168.1.208\New].[TopSoft].[dbo].[Sales_invoice]
 insert into  [192.168.1.76\sql2016].[Easysoft].[dbo].[Sales_invoice]
  select  (select top 1 sor.SortFi+1 from  [192.168.1.76\sql2016].Easysoft.dbo.Sales_Invoice sor order by sor.SortFi desc),
  	'1',1,'1',	s.Location,	'', 	0,	'',	Cast(getdate() as date),'1900-01-01',2,0,0,0,Cast(getdate() as date),0,0,'48101',0,Custno,'','1',1,'0',sum((S.cost*s.costa)*(1+(tax*0.01)))Total,'',0,0,0,0,0,0,'',0,0,'0',0,'C','',0,'','','',0,0,'1','','',0,0,'','',0
  from (
  select Ra.No Custno,SUBSTRING(st.Salesid, 7, LEN(st.Salesid)) Location,st.Custaccount Depa, st.Itemid Descript, 
 st.Modifieddatetime TransDate,st.Qtyordered Cost
, st.Lineamount Qty, st.Salesprice CostA,t.tax Tax
from SALESLINE st
inner join (
select tacu.Code,tacu.No from [192.168.1.156].AXDBTest.dbo.CustInv tacu
)Ra on Ra.Code=st.Custaccount
left join 
(select ta.Tax,ItemCode from  [192.168.1.156].AXDBTest.dbo.itax ta ) t on  st.Itemid = t.ItemCode
inner join (
 select  distinct ItemCode from [192.168.1.156].AXDBTest.dbo.itax
)R on R.ItemCode=st.Itemid
--
--
where st.Salesid =  @SalesId 
--and cast(st.Createddatetime as date) = Cast(@CDate as date) and cast(st.Modifieddatetime as date) =cast(getdate() as date)
 and
st.Custgroup = 'Third Part' and st.Recversion != 1 
)S
group by s.Location,Custno
";
        //Analysis
        public string Analysis = @"Select * from RptAnalysis ";
        //Compare Data
        public string CompareData_Ax_Top = @"
delete from SalesSums
    INSERT INTO SalesSums (Source, TotalSales)
    SELECT 'SalesData', SUM(totalsales)
    FROM AXDBSalesAll
    WHERE transdate BETWEEN @fromDate AND @toDate
and (SalesStatus !='0') 
;

    INSERT INTO SalesSums (Source, TotalSales)
    SELECT 'AAArptsales', SUM(totalsales)
    FROM (
Select 
SalesH.TRANSDATE As TransDate,-SalesD.Qty Qty,
-(SalesD.NETAMOUNTINCLTAX) TotalSales
from  [192.168.1.210].AXDB.dbo.RetailTransactiontable SalesH
INNER JOIN  [192.168.1.210].AXDB.dbo.RETAILTRANSACTIONSALESTRANS SalesD ON SalesH.TRANSACTIONID = SalesD.TRANSACTIONID
where SalesH.ENTRYSTATUS!=1  and SalesH.TYPE=2 and salesd.INVENTSTATUSSALES<>'0'
)s
where transdate BETWEEN @fromDate AND @toDate;
select Source 'Store Name',TotalSales from SalesSums

";
        //Connection String 
        //link with Branch Server to Get data from it's Server

        //change suppliercode, transactionnumber,storeid,dpid to string in rptsales and 2 and all 
        // Views

    }
}
