namespace CK.Models
{
    public class SalesParameters
    {
        public bool SalesOrder { get; set; }
        public bool BranchOwner { get; set; }
        public bool FollowBranches { get; set; }
        public bool VPerHour { get; set; }
        public string Certificate { get; set; }
        public string Messagefordev { get; set; }
        public string Remarks { get; set; }
        public string InsuranceCompany { get; set; }
        public string CarModel { get; set; }
        public string OwnedBy { get; set; }
        public float InsuranceValue { get; set; }
        public float InstallmentValue { get; set; }
        public string DriverPhone { get; set; }
        public int YearModel { get; set; }
        public bool IsManager { get; set; }
        public string DriverName { get; set; }
        public string ChassisNumber { get; set; }
        public string MotorNumber { get; set; }
        public string CarNumberDigits { get; set; }
        public string CarNumberLetters { get; set; }
        public string ManagerName { get; set; }
        public int ManagerId { get; set; }
        public int RequestStatus { get; set; }
        public int ModelTypeSerial { get; set; }
        public int RoleSerial { get; set; }
        public int ManagerSerial { get; set; }
        public int ModelId { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeSerial { get; set; }
        public string Job { get; set; }
        public string RoleName { get; set; }
        public string ModelTypeName { get; set; }
        public IFormFile CardPhoto { get; set; }
        public string CarNumber { get; set; }
        public bool Cat { get; set; }// Initialize your model property
        public bool Storef { get; set; }// Initialize your model property
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string Store { get; set; }
        public string Storeall { get; set; }
        public string StoreRM { get; set; }
        public string StoreDy { get; set; }
        public string StoreRich { get; set; }
        public string ToStore { get; set; }
        public List<string> SelectedStores { get; set; }
        public string Department { get; set; }
        public string? Supplier { get; set; }
        public bool ExportAfterClick { get; set; }
        public string[] SelectedItems { get; set; }
        public bool VPerDay { get; set; }
        public bool VPerMonYear { get; set; }
        public bool VPerMon { get; set; }
        public bool VPerYear { get; set; }
        public bool VQty { get; set; }
        public bool VPrice { get; set; }
        public bool VStoreName { get; set; }
        public bool VDepartment { get; set; }
        public bool VTotalSales { get; set; }
        public bool VTotalCost { get; set; }
        public bool VTotalTax { get; set; }
        public bool VTotalSalesTax { get; set; }
        public bool VTotalSalesWithoutTax { get; set; }
        public bool VTotalCostQty { get; set; }
        public bool VCost { get; set; }
        public bool VItemLookupCode { get; set; }
        public bool VItemName { get; set; }
        public bool VSupplierId { get; set; }
        public bool VSupplierName { get; set; }
        public string Franchise { get; set; }
        public bool VTransactionNumber { get; set; }
        public bool VFranchise { get; set; }
        public int? MonthToFilter { get; set; }
        public string ItemLookupCodeTxt { get; set; }
        public string ItemNameTxt { get; set; }
        public bool TMT { get; set; }
        public bool RMS { get; set; }
        public bool DBbefore { get; set; }
        public bool Yesterday { get; set; }
        public bool VTransactionCount { get; set; }
        public bool exportAfterClick { get; set; }
        public bool VStoreId { get; set; }
        public bool VDpId { get; set; }
        public bool VPaidtype { get; set; }
        public bool VDateInTime { get; set; }
        public bool Vbatch { get; set; }
        public bool SalesBranchPage { get; set; }
        public bool Shelf { get; set; }
        public bool Inventlocation { get; set; }
        public bool FoodCategory { get; set; }
        public bool Tenderfrombranch { get; set; }
        public bool isDmanager { get; set; }
        public bool isFmanager { get; set; }
        public bool isUsername { get; set; }
        public bool CompareData { get; set; }
        public bool StockFromBranch { get; set; }
        public double TotalSales { get; set; }
        public bool MainSales { get; set; }
         public bool MainStock { get; set; }
        public bool MainTender { get; set; }
        public bool Home { get; set; }
        public string DB { get; set; }
        public int actionValue { get; set; }
        public bool RichCut { get; set; }
        public bool SupplierofItem { get; set; }
        public bool Dashboard { get; set; }
        public bool SystemAnaysis { get; set; }
        public bool ItemMovements { get; set; }
        public bool OfferPage { get; set; }
        public bool Area { get; set; }
        public string Offer { get; set; }
        public string YearinDash { get; set; }
        public string InvoiceStatus { get; set; }
        public string TransferMessage { get; set; }
        public bool VYearVinDash { get; set; }
        public bool VCompany { get; set; }
        public int ManagerReply { get; set; }
        public bool Approve { get; set; }
        public bool UnApprove { get; set; }
        public bool VDateandtime { get; set; }
        public string Paidtypelist { get; set; }
        public bool DisplayUploadedInvoices { get; set; }
        public string RptTransfer = @"
SELECT distinct Trtable.INVENTLOCATIONIDFROM as FromTable, ca.Itemid AS Barcode,It.Name AS Item,
Trline.Qtytransfer AS Quantity,Trtable.INVENTLOCATIONIDTO AS ToTable, 
CONVERT(VARCHAR, Trline.Receivedate, 101) AS Day,Trline.CREATEDDATETIME, 
Trline.Transferid AS [Transaction],
CASE 
   WHEN Trline.Remainstatus = 2 THEN 'Created' 
   WHEN Trline.Remainstatus = 1 THEN 'Shipped' 
   ELSE 'Received' 
   END AS Status
FROM 
    AXDB.dbo.Inventtransferline AS Trline
INNER JOIN 
    AXDB.dbo.Inventtransfertable AS Trtable ON Trline.Transferid = Trtable.Transferid
INNER JOIN 
    AXDB.dbo.Inventtable AS ca ON Trline.Itemid = ca.Itemid
INNER JOIN 
    AXDB.dbo.Ecoresproducttranslation AS It ON ca.Product = It.Product
  where Trline.Transferid = Trtable.Transferid ";
        public string RptApprovedTrans = @"
select * from TransferDetails ";
        public string RptItemMovements2() {
            string Sql =
            @"
truncate  table axdbtest.dbo.ItemTransactions
insert into  axdbtest.dbo.ItemTransactions
(Store_Name,  Storefrom,  OpeningBalance,Sales,Purchase,Transfer,Counting,ItemName,ItemlookupCode
 ,Transactions,Franchise,Category ,Terminal,Account,Receivedate,Quantity,Price
 ,CurrentBalance
 )
select  
 Store_Name,  Storefrom,  OpeningBalance,Sales,Purchase,Transfer,Counting,ItemName,ItemlookupCode
 ,Transactions,Franchise,Category ,Terminal,Account,Receivedate,Quantity,Price
,SUM(Quantity) OVER (ORDER BY  Receivedate,Transactions) AS CurrentBalance
 --,SUM(Quantity) OVER (ORDER BY Receivedate,Transactions ROWS BETWEEN UNBOUNDED PRECEDING AND 1 PRECEDING) AS CurrentBalance
 --SUM(Quantity) OVER (ORDER BY Receivedate,Transactions asc ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS CurrentBalance
 --SUM(Quantity) OVER (ORDER BY  Receivedate,Transactions) AS CurrentBalance
    from (
	select distinct  Store_Name = 'HeadOffice',
                                   Storefrom ='',
								   OpeningBalance= DJou.qty,Sales=0,Purchase=0,Transfer=0,Counting=0 
								   ,Quantity=DJou.qty
                                   ,ItemName = CIt.Name,
                                   ItemlookupCode = ca.Itemid,
                                   Receivedate = Cast(DJou.transdate as date),
                                   Transactions = Hjou.JOURNALID,
                                   Franchise = '',
                                   Category = catn.Name ,
                                   Terminal = '',
								  Account= 'Opening Balance',0 Price

 FROM [AXDB].[dbo].[INVENTJOURNALTABLE] HJou
 inner join  INVENTJOURNALTRANS DJou on Hjou.JOURNALID=DJou.JOURNALID
  inner join   Inventtable ca on DJou.Itemid = ca.Itemid
                               inner join   Ecoresproducttranslation CIt on ca.Product = CIt.Product
                               inner join   Ecoresproductcategory cat on ca.Product = cat.Product 
                               inner join   Ecorescategory catn on cat.Category = catn.Recid 
   and INVENTLOCATIONID='HeadOffice'
   and HJou.DESCRIPTION='Open movement journal'
  -- and DJou.Itemid='0017165'
 -- and Cast(DJou.transdate  as date) >= @fromDate and Cast(DJou.transdate  as date) <= @toDate
union all
SELECt 
ITJ.INVENTLOCATIONIDTO Store_Name,
	Itj.INVENTLOCATIONIDFROM Storefrom
	,OpeningBalance= 0,Sales=0,Purchase=0,
	case when INVENTLOCATIONIDTO='HeadOffice' then -iTrnas.Qty  else iTrnas.Qty end as Transfer
	,Counting=0 ,
	case when INVENTLOCATIONIDTO='HeadOffice' then -iTrnas.Qty  else iTrnas.Qty end as Quantity,
	It.Name ItemName
	--,
	,iTrnas.Itemid ItemlookupCode
	,iTrnas.DateStatus,--DATEPHYSICAL,
	ITTr.REFERENCEID Transactions
 ,case when INVENTLOCATIONIDTO='HeadOffice' then 'Transfer order receive' else 'Transfer order shipment' end as Franchise
,Category = Catn.Name 
,Terminal = ''
,Account = 'Transfer' ,0 Price
--,JOBSTATUS
from INVENTTRANSORIGIN ITTr 
inner join  inventtrans iTrnas on ITTr.RecId=iTrnas.InventTransOrigin 
--and (ittr.REFERENCECATEGORY=22 and iTrnas.STATUSISSUE=1)
--inner join INVENTTRANSFERPARMTable ITT  on Ittr.REFERENCEID=itt.TRANSFERID
--left JOIN INVENTTRANSFERPARMLINE ITL ON  Ittr.REFERENCEID = ITL.TRANSFERID
left JOIN (select distinct INVENTLOCATIONIDFROM,INVENTLOCATIONIDTO,TRANSFERID from InventTransferJour
where  (INVENTLOCATIONIDFROM='HeadOffice' or INVENTLOCATIONIDTO='HeadOffice')) ITJ ON ITJ.TRANSFERID = Ittr.REFERENCEID
                       left  join  ( select distinct Itemid,product from Inventtable) ca on iTrnas.Itemid = ca.Itemid
                       left  join   Ecoresproducttranslation It on ca.Product = It.Product
                       left  join   Ecoresproductcategory cat on ca.Product = cat.Product 
                        left join   Ecorescategory catn on cat.Category = catn.Recid 
						where (INVENTLOCATIONIDFROM='HeadOffice' or INVENTLOCATIONIDTO='HeadOffice') 
						--and QTYSHIPNOW !=0 --and SHIPUPDATEQTY=1
						and
						(ittr.REFERENCECATEGORY=22 and iTrnas.STATUSISSUE=1) or iTrnas.STATUSISSUE=4
						--and iTrnas.Itemid='0017165'
						--and Cast(iTrnas.DateStatus  as date) >= @fromDate and Cast(iTrnas.DateStatus  as date) <= @toDate
						 --and (ITJ.updatetype=1 or ITJ.AutoReceiveQty=1)
						 union all
select distinct  Store_Name = Satable.Salesname,
                             Name = Satable.Inventlocationid,
							 OpeningBalance=0,Sales=Saline.Salesqty * -1,Purchase=0,Transfer=0,Counting=0 ,
                             Quantity=Saline.Salesqty * -1,
							 Item = It.Name,
                             Barcode = ca.Itemid,
                             Day = Cast(Saline.Confirmeddlv as date),
                             Transactions = Saline.Salesid,
                              Case when Saline.Salesstatus = 1 then 'Created' when  Saline.Salesstatus = 2 then 'Processing' when  Saline.Salesstatus = 3 then 'Delivered' 
							  when  Saline.Salesstatus = 4 then  'Invoiced' else 'UnKnown'
                             end Franchise,
							 Category = Catn.Name ,
                             Terminal = Saline.Inventtransid,
							 Account='Sales',0 Price
							 from  Salestable Satable
                         inner join   Salesline Saline on Saline.Salesid = Satable.Salesid
                         left join   Inventtable ca on Saline.Itemid = ca.Itemid
                         left join   Ecoresproducttranslation It on ca.Product = It.Product
                         left join   Ecoresproductcategory cat on ca.Product = cat.Product 
                         left join   Ecorescategory catn on cat.Category = catn.Recid 
                         where Satable.Inventlocationid in ('Headoffice', '') --And  ca.Itemid = '0017165'
						and Satable.SALESSTATUS=3
						-- And Saline.Salesid = '0017165'
						--and Cast(Saline.Confirmeddlv  as date) >= @fromDate and Cast(Saline.Confirmeddlv  as date) <= @toDate
						 --and Saline.Salesid='TMT-006941600'
--and Saline.Salesstatus = 4
union all
                         select distinct   Putable.Purchname Store_Name,Name = Putable.Inventlocationid,
						  OpeningBalance=0,Sales=0,Purchase=Puline.Purchqty,Transfer=0,Counting=0 ,
						  Quantity=Puline.Purchqty,
						 Item = It.Name,
                             Barcode = ca.Itemid,Day = Cast(Puline.Deliverydate as date),Transactions = Puline.Purchid,
							 Case when Puline.Purchstatus = 1 then 'Open Order' when Puline.Purchstatus = 2 then 'Invoiced' 
							 when Puline.Purchstatus = 4  then 'Canceled' else 'UnKnown' end  Franchise,
                             Category = catn.Name ,Terminal = Puline.Inventtransid,
							  Account = 'Purchase' ,puline.PURCHPRICE Price
							  from
							   Purchline Puline
                         inner join   Purchtable Putable on Puline.Purchid = Putable.Purchid
                         left join   Inventtable ca on Puline.Itemid = ca.Itemid
                         left join   Ecoresproducttranslation It on ca.Product = It.Product
                         left join   Ecoresproductcategory cat on ca.Product = cat.Product
                         left join   Ecorescategory catn on cat.Category = catn.Recid 
                         where Putable.Inventlocationid = 'Headoffice'
						 							--   And Puline.Purchid = '0017165'
                       --  and Cast(Puline.Deliverydate as date) >= @fromDate and Cast(Puline.Deliverydate as date) <= @toDate
						 -- and Puline.Purchstatus = 2
union all
 select distinct 
                                   Store_Name = '',
                                   Name = Dim.Inventlocationid,
								   OpeningBalance=0,Sales=0,Purchase=0,Transfer=0,Counting=CountJour.Countedqty - CountJour.Inventonhand ,
								   Quantity=CountJour.Countedqty - CountJour.Inventonhand,
                                   Item = It.Name,
                                   Barcode = ca.Itemid,
                                   Day = Cast(CountJour.Countdate as date),
                                   Transactions = '',
                                   Franchise = '',
                                   Category = catn.Name ,
                                   Terminal = CountJour.Inventdimid,
								   	 Account ='Counting'  ,0 Price
            from  Inventdim Dim
                               inner join   Inventcountjour CountJour on Dim.Inventdimid = CountJour.Inventdimid
                               left join   Inventtable ca on CountJour.Itemid = ca.Itemid
                               left join   Ecoresproducttranslation It on ca.Product = It.Product
                               left join   Ecoresproductcategory cat on ca.Product = cat.Product 
                               left join   Ecorescategory catn on cat.Category = catn.Recid 
                               where Dim.Inventlocationid = 'Headoffice' 
							  -- And CountJour.Itemid = '0017165'
							   --and Cast(CountJour.Countdate  as date) >= @fromDate and Cast(CountJour.Countdate  as date) <= @toDate
                               )T
							   where ItemlookupCode is not null
							   ";
        return Sql;
        }
        public string RptItemMovements = @"
  select  
 Store_Name,  Storefrom,  OpeningBalance,Sales,Purchase,Transfer,Counting,ItemName,ItemlookupCode
 ,Receivedate,Transactions,Franchise,Category ,Terminal,Account,Quantity,SUM(Quantity) OVER (ORDER BY  Receivedate,Transactions) AS CurrentBalance
    from (
	select distinct  Store_Name = 'HeadOffice',
                                   Storefrom ='',
								   OpeningBalance= DJou.qty,Sales=0,Purchase=0,Transfer=0,Counting=0 
								   ,Quantity=DJou.qty
                                   ,ItemName = CIt.Name,
                                   ItemlookupCode = ca.Itemid,
                                   Receivedate = Cast(DJou.transdate as date),
                                   Transactions = Hjou.JOURNALID,
                                   Franchise = '',
                                   Category = catn.Name ,
                                   Terminal = '',
								  Account= 'Opening Balance'
 FROM [AXDB].[dbo].[INVENTJOURNALTABLE] HJou
 inner join  INVENTJOURNALTRANS DJou on Hjou.JOURNALID=DJou.JOURNALID
  inner join   Inventtable ca on DJou.Itemid = ca.Itemid
                               inner join   Ecoresproducttranslation CIt on ca.Product = CIt.Product
                               inner join   Ecoresproductcategory cat on ca.Product = cat.Product 
                               inner join   Ecorescategory catn on cat.Category = catn.Recid 
   and INVENTLOCATIONID='HeadOffice'
   and HJou.DESCRIPTION='Open movement journal'
  and Cast(DJou.transdate  as date) >= @fromDate and Cast(DJou.transdate  as date) <= @toDate
union all
SELECt 
ITJ.INVENTLOCATIONIDTO Store_Name,
	Itj.INVENTLOCATIONIDFROM Storefrom
	,OpeningBalance= 0,Sales=0,Purchase=0,
	case when INVENTLOCATIONIDTO='HeadOffice' then -iTrnas.Qty  else iTrnas.Qty end as Transfer
	,Counting=0 ,
	case when INVENTLOCATIONIDTO='HeadOffice' then -iTrnas.Qty  else iTrnas.Qty end as Quantity,
	It.Name ItemName
	--,
	,iTrnas.Itemid ItemlookupCode
	,iTrnas.DateStatus,--DATEPHYSICAL,
	ITTr.REFERENCEID Transactions
 ,case when INVENTLOCATIONIDTO='HeadOffice' then 'Transfer order receive' else 'Transfer order shipment' end as Franchise
,Category = Catn.Name 
,Terminal = ''
,Account = 'Transfer' 
--,JOBSTATUS
from INVENTTRANSORIGIN ITTr 
inner join  inventtrans iTrnas on ITTr.RecId=iTrnas.InventTransOrigin 
--and (ittr.REFERENCECATEGORY=22 and iTrnas.STATUSISSUE=1)
--inner join INVENTTRANSFERPARMTable ITT  on Ittr.REFERENCEID=itt.TRANSFERID
--left JOIN INVENTTRANSFERPARMLINE ITL ON  Ittr.REFERENCEID = ITL.TRANSFERID
left JOIN (select distinct INVENTLOCATIONIDFROM,INVENTLOCATIONIDTO,TRANSFERID from InventTransferJour
where  (INVENTLOCATIONIDFROM='HeadOffice' or INVENTLOCATIONIDTO='HeadOffice')) ITJ ON ITJ.TRANSFERID = Ittr.REFERENCEID
                       left  join  ( select distinct Itemid,product from Inventtable) ca on iTrnas.Itemid = ca.Itemid
                       left  join   Ecoresproducttranslation It on ca.Product = It.Product
                       left  join   Ecoresproductcategory cat on ca.Product = cat.Product 
                        left join   Ecorescategory catn on cat.Category = catn.Recid 
						where (INVENTLOCATIONIDFROM='HeadOffice' or INVENTLOCATIONIDTO='HeadOffice') 
						--and QTYSHIPNOW !=0 --and SHIPUPDATEQTY=1
						and
						(ittr.REFERENCECATEGORY=22 and iTrnas.STATUSISSUE=1) or iTrnas.STATUSISSUE=4
						and Cast(iTrnas.DateStatus  as date) >= @fromDate and Cast(iTrnas.DateStatus  as date) <= @toDate
						 --and (ITJ.updatetype=1 or ITJ.AutoReceiveQty=1)
						 union all
select distinct  Store_Name = Satable.Salesname,
                             Name = Satable.Inventlocationid,
							 OpeningBalance=0,Sales=Saline.Salesqty * -1,Purchase=0,Transfer=0,Counting=0 ,
                             Quantity=Saline.Salesqty * -1,
							 Item = It.Name,
                             Barcode = ca.Itemid,
                             Day = Cast(Saline.Confirmeddlv as date),
                             Transactions = Saline.Salesid,
                              Case when Saline.Salesstatus = 1 then 'Created' when  Saline.Salesstatus = 2 then 'Processing' when  Saline.Salesstatus = 3 then 'Delivered' 
							  when  Saline.Salesstatus = 4 then  'Invoiced' else 'UnKnown'
                             end Franchise,
							 Category = Catn.Name ,
                             Terminal = Saline.Inventtransid,
							 Account='Sales'
							 from  Salestable Satable
                         inner join   Salesline Saline on Saline.Salesid = Satable.Salesid
                         left join   Inventtable ca on Saline.Itemid = ca.Itemid
                         left join   Ecoresproducttranslation It on ca.Product = It.Product
                         left join   Ecoresproductcategory cat on ca.Product = cat.Product 
                         left join   Ecorescategory catn on cat.Category = catn.Recid 
                         where Satable.Inventlocationid in ('Headoffice', '') --And  ca.Itemid = '0017165'
						and Satable.SALESSTATUS=3
						and Cast(Saline.Confirmeddlv  as date) >= @fromDate and Cast(Saline.Confirmeddlv  as date) <= @toDate
						 --and Saline.Salesid='TMT-006941600'
--and Saline.Salesstatus = 4
union all
                         select distinct   Putable.Purchname Store_Name,Name = Putable.Inventlocationid,
						  OpeningBalance=0,Sales=0,Purchase=Puline.Purchqty,Transfer=0,Counting=0 ,
						  Quantity=Puline.Purchqty,
						 Item = It.Name,
                             Barcode = ca.Itemid,Day = Cast(Puline.Deliverydate as date),Transactions = Puline.Purchid,
							 Case when Puline.Purchstatus = 1 then 'Open Order' when Puline.Purchstatus = 2 then 'Invoiced' 
							 when Puline.Purchstatus = 4  then 'Canceled' else 'UnKnown' end  Franchise,
                             Category = catn.Name ,Terminal = Puline.Inventtransid,
							  Account = 'Purchase'  
							  from
							   Purchline Puline
                         inner join   Purchtable Putable on Puline.Purchid = Putable.Purchid
                         left join   Inventtable ca on Puline.Itemid = ca.Itemid
                         left join   Ecoresproducttranslation It on ca.Product = It.Product
                         left join   Ecoresproductcategory cat on ca.Product = cat.Product
                         left join   Ecorescategory catn on cat.Category = catn.Recid 
                         where Putable.Inventlocationid = 'Headoffice'
                         and Cast(Puline.Deliverydate as date) >= @fromDate and Cast(Puline.Deliverydate as date) <= @toDate
						 -- and Puline.Purchstatus = 2
union all
 select distinct 
                                   Store_Name = '',
                                   Name = Dim.Inventlocationid,
								   OpeningBalance=0,Sales=0,Purchase=0,Transfer=0,Counting=CountJour.Countedqty - CountJour.Inventonhand ,
								   Quantity=CountJour.Countedqty - CountJour.Inventonhand,
                                   Item = It.Name,
                                   Barcode = ca.Itemid,
                                   Day = Cast(CountJour.Countdate as date),
                                   Transactions = '',
                                   Franchise = '',
                                   Category = catn.Name ,
                                   Terminal = CountJour.Inventdimid,
								   	 Account ='Counting'  
            from  Inventdim Dim
                               inner join   Inventcountjour CountJour on Dim.Inventdimid = CountJour.Inventdimid
                               left join   Inventtable ca on CountJour.Itemid = ca.Itemid
                               left join   Ecoresproducttranslation It on ca.Product = It.Product
                               left join   Ecoresproductcategory cat on ca.Product = cat.Product 
                               left join   Ecorescategory catn on cat.Category = catn.Recid 
                               where Dim.Inventlocationid = 'Headoffice' and Cast(CountJour.Countdate  as date) >= @fromDate and Cast(CountJour.Countdate  as date) <= @toDate
                               )T
							   where ItemlookupCode is not null 
 and Receivedate between @fromDate and @toDate
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
        public string RichCutStock = @"sELECT distinct s.Name StoreName,It.[Cost] ,It.[StoreID] , It.[Price] , It.[Quantity] Qty
                                 , It.[Description] ItemName ,It.[ItemLookupCode] 
                                ,dd.code DpId,Dep.[Name] DpName , Supp.SupplierName,s.DManager,s.FManager,s.Username,'SUB-FRANCHISE'StoreFranchise,Supp.Code SupplierCode
								from
                                  [RichCut_DATA_CENTER].[dbo].[Item] AS It 
                                    left JOIN [RichCut_DATA_CENTER].[dbo].[department] AS Dep ON It.DepartmentID = Dep.ID AND It.storeid = Dep.storeid
                                    left join (select Name,code  from DATA_CENTER.dbo.department)dd on dd.Name =Dep.Name
                                    left JOIN [RichCut_DATA_CENTER].[dbo].[SupplierList] AS SuppL ON It.storeid = SuppL.storeid AND It.SupplierID = SuppL.SupplierID AND It.ID = SuppL.ItemID
                                   left JOIN [RichCut_DATA_CENTER].[dbo].[Supplier] AS Supp ON SuppL.SupplierID = Supp.ID AND SuppL.storeid = Supp.storeid
								   inner join (select distinct DManager,FManager,BranchOwner,Username,Name,RMSstoNumber from CkproUsers.dbo.Storeuser where Name='RichCut' or Name = 'RichCut_No' or Name = 'RichCut_Zayed') s on (SUBSTRING(s.RMSstoNumber, 2, 1)=Convert(varchar(5),It.[StoreID]))
where It.[ItemLookupCode] LIKE '[0-9]%' ";
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
        //Stock
        public string linkRmsRptStoreView = @"
								SELECT distinct st.Franchise StoreFranchise,it.StoreID,sty.Username StoreName,
								Dep.code DpId, Dep.Name dpName,It.ItemLookupCode, It.Description ItemName,It.Cost--, It.Price 
								, It.Quantity Qty
								,Supp.Code SupplierCode ,Supp.SupplierName,sty.Username,sty.DManager,sty.FManager
								FROM [192.168.1.156].[DATA_CENTER].[dbo].[Item] AS It
								inner JOIN [192.168.1.156].[DATA_CENTER].[dbo].[department] AS Dep ON It.DepartmentID = Dep.ID AND It.storeid = Dep.storeid
								inner join  [192.168.1.156].DATA_CENTER.dbo.Supplier Supp on  Supp.storeid=It.STOREID and Supp.id=It.SupplierID
--left JOIN [192.168.1.156].[DATA_CENTER].[dbo].[SupplierList] AS SuppL ON It.storeid = SuppL.storeid 
								--AND It.SupplierID = SuppL.SupplierID AND It.ID = SuppL.ItemID
								--left JOIN [192.168.1.156].[DATA_CENTER].[dbo].[Supplier] AS Supp ON SuppL.SupplierID = Supp.ID AND SuppL.storeid = Supp.storeid
								left join  [192.168.1.156].CkproUsers.dbo.Storeuser sty on sty.RMSstoNumber =convert(varchar(10),it.storeid)
								left join [192.168.1.156].[DATA_CENTER].dbo.STORES st on st.STORE_ID =it.storeid
                                --left join [TopSoft].dbo.StoreStatus Store on Store.Storeid =it.storeid
								where st.Franchise='SUB-FRANCHISE' and sty.RMSstoNumber !='58'and sty.BranchStatus=1";
        public string linkDyRptStoreView()
        {
            string RptStore = @"select distinct
										Inv.Modifieddatetime Modified, 
										Inv.INVENTLOCATIONID StoreNameInDy, 
										Inv.Physicalinvent Qty, Inv.Itemid ItemLookupCode,--f.AMOUNT Price,
										It.Name ItemName, CateN.CODE DpId,CateN.Name dpName
										,ca.Primaryvendorid SupplierCode,st.Franchise StoreFranchise,st.storenumber StoreId,st.username StoreName,DManager,FManager,BranchOwner,Username";
            if (VCost)
            {
                RptStore += ",t.COSTPRICE Cost";
            }
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptStore += " ,W.SupplierName";
            }
            RptStore += @"
										 from  [192.168.1.210].AXDB.dbo.Inventsum Inv
										inner join  [192.168.1.210].AXDB.dbo.Inventtable ca on Inv.Itemid = ca.Itemid";
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptStore += " left join (Select distinct Code,SupplierName from DATA_CENTER.dbo.supplier) w on w.Code=ca.Primaryvendorid collate SQL_Latin1_General_CP1_CI_AS ";
            }
            RptStore += @"
											
                                            left join [192.168.1.210].AXDBTest.dbo.Storeusers st on st.Inventlocation Collate SQL_Latin1_General_CP1_CI_AS=Inv.INVENTLOCATIONID
                                            left join [192.168.1.210].AXDB.dbo.Ecoresproducttranslation It on ca.Product = It.Product
											left join [192.168.1.210].AXDB.dbo.Ecoresproductcategory Re on It.Product = Re.Product
											left join [192.168.1.210].AXDB.dbo.Ecorescategory CateN on Re.Category = CateN.Recid ";
            if (VCost)
            {
                RptStore += @"left join (select distinct ITEMNUMBER,UNITCOST COSTPRICE  from [192.168.1.210].AXDB.dbo.ECORESRELEASEDPRODUCTV2ENTITY s)t on t.ITEMNUMBER=inv.ITEMID";
              //  RptStore += @"left join (SELECT distinct s.COSTPRICE,s.ITEMID a FROM   [192.168.1.210].AXDB.dbo.Salesline s WHERE s.Confirmeddlv = (
									//	    SELECT  MAX(Confirmeddlv) FROM   [192.168.1.210].AXDB.dbo.Salesline where itemid=s.ITEMID) )t on t.a=inv.ITEMID ";
            }
            RptStore += @"
                                          --left join [192.168.1.210].AXDBTEST.dbo.StoreStatus Store on Store.Storeid Collate SQL_Latin1_General_CP1_CI_AS =st.storenumber
where ca.Dataareaid = 'tmt' and  st.BranchStatus=1 ";
            return RptStore;
        }
        public string RmsRptStoreView = @"
								SELECT distinct st.Franchise StoreFranchise,it.StoreID,sty.Username StoreName,
								Dep.code DpId, Dep.Name dpName,It.ItemLookupCode, It.Description ItemName,It.Cost--, It.Price 
								, It.Quantity Qty
								,Supp.Code SupplierCode ,Supp.SupplierName,sty.Username,sty.DManager,sty.FManager
								FROM [DATA_CENTER].[dbo].[Item] AS It
								inner JOIN [DATA_CENTER].[dbo].[department] AS Dep ON It.DepartmentID = Dep.ID AND It.storeid = Dep.storeid
                                inner join  DATA_CENTER.dbo.Supplier Supp on  Supp.storeid=It.STOREID and Supp.id=It.SupplierID
								--inner JOIN [DATA_CENTER].[dbo].[SupplierList] AS SuppL ON It.storeid = SuppL.storeid 
								--AND It.SupplierID = SuppL.SupplierID AND It.ID = SuppL.ItemID
								--inner JOIN [DATA_CENTER].[dbo].[Supplier] AS Supp ON SuppL.SupplierID = Supp.ID AND SuppL.storeid = Supp.storeid
								inner join CkproUsers.dbo.Storeuser sty on sty.RMSstoNumber =convert(varchar(10),it.storeid)
								inner join [DATA_CENTER].dbo.STORES st on st.STORE_ID =it.storeid
                               -- left join [192.168.1.208\New].[TopSoft].dbo.StoreStatus Store on Store.Storeid =it.storeid
								where st.Franchise='SUB-FRANCHISE' and sty.RMSstoNumber !='58' and sty.BranchStatus=1";
        public string DyRptStoreView()
        {
            string RptStore = @"select distinct
										Inv.Modifieddatetime Modified, 
										Inv.INVENTLOCATIONID StoreNameInDy, 
										Inv.Physicalinvent Qty, Inv.Itemid ItemLookupCode,--f.AMOUNT Price,
										It.Name ItemName, CateN.CODE DpId,CateN.Name dpName
										,ca.Primaryvendorid SupplierCode,st.Franchise StoreFranchise,st.storenumber StoreId,st.username StoreName,DManager,FManager,BranchOwner,Username";
            if (VCost)
            {
                RptStore += ",t.COSTPRICE Cost";
            }
            if (VSupplierId || VSupplierName||(Supplier != null && Supplier != "0"))
            {
                RptStore += " ,W.SupplierName";
            }
            RptStore += @"
										 from AXDB.dbo.Inventsum Inv
										inner join AXDB.dbo.Inventtable ca on Inv.Itemid = ca.Itemid";
            if (VSupplierId || VSupplierName||(Supplier != null && Supplier != "0"))
            {
                RptStore += " inner join (Select distinct Code,SupplierName from [192.168.1.156].DATA_CENTER.dbo.supplier) w on w.Code=ca.Primaryvendorid collate SQL_Latin1_General_CP1_CI_AS ";
            }
            RptStore += @"
											
                                            left join AXDBTEST.dbo.Storeusers st on st.Inventlocation collate SQL_Latin1_General_CP1_CI_AS=Inv.INVENTLOCATIONID
                                            left join AXDB.dbo.Ecoresproducttranslation It on ca.Product = It.Product
											left join AXDB.dbo.Ecoresproductcategory Re on It.Product = Re.Product
											left join AXDB.dbo.Ecorescategory CateN on Re.Category = CateN.Recid ";
            if (VCost)
            {
                RptStore += @"left join (select distinct ITEMNUMBER,UNITCOST COSTPRICE  from AXDB.dbo.ECORESRELEASEDPRODUCTV2ENTITY s)t on t.ITEMNUMBER=inv.ITEMID";
               // RptStore += @"left join (SELECT distinct s.COSTPRICE,s.ITEMID a FROM  AXDB.dbo.Salesline s WHERE s.Confirmeddlv = (
								//		    SELECT  MAX(Confirmeddlv) FROM  AXDB.dbo.Salesline where itemid=s.ITEMID) )t on t.a=inv.ITEMID ";
            }
            RptStore += @"
											--inner join (select distinct pr.Itemrelation,pr.Amount, pr.Accountrelation from Pricedisctable pr
											--where cast (pr.Todate as date) = '1900-01-01' and pr.Dataareaid = 'tmt'
											 --and (pr.Accountrelation = 'Retail' or pr.Accountrelation = 'HSC' or pr.Accountrelation = 'Northp'))f on inv.itemid = f.Itemrelation
                                           --  left join AXDBTEST.dbo.StoreStatus Store on Store.Storeid Collate SQL_Latin1_General_CP1_CI_AS =st.storenumber
where ca.Dataareaid = 'tmt' and  st.BranchStatus=1 ";
            return RptStore;
        }
        public string RptStockofBranchView = @"select  Inv.INVENTLOCATIONID StoreName,It.Name ItemName,Inv.ItemId ItemLookupcode,Cat.NAME dpName,Cat.CODE dpId, sum(Inv.Physicalinvent) Qty
										,''Username,''DManager,''FManager from AXDB.dbo.INVENTSUM Inv
										inner join AXDB.dbo.Inventtable Ca on Ca.Itemid=Inv.itemid
										inner join AXDB.dbo.Ecoresproducttranslation It on ca.Product = It.Product
										inner join .AXDB.dbo.Ecoresproductcategory Re on It.product =Re.Product
										left join AXDB.dbo.Ecorescategory CateN on Re.Category = CateN.Recid
										INNER JOIN  (Select distinct Cat.ReCId,Cat.CODE,Cat.NAME from AXDB.dbo.Ecorescategory Cat)Cat on Re.Category = Cat.Recid

										 where ca.Dataareaid = 'tmt' and  Inv.INVENTLOCATIONID is not null or Inv.INVENTLOCATIONID=''
										 group by Inv.INVENTLOCATIONID,It.Name,Inv.ItemId,Cat.NAME,Cat.CODE";
        public string RptStoreAll()
        {
            string RptStoreAll = @"
                SELECT StoreFranchise Collate SQL_Latin1_General_CP1_CI_AS StoreFranchise,Convert (varchar(10),StoreID)StoreId,Convert (varchar(10),StoreID) StoreIdR,''StoreIdD,StoreName Collate SQL_Latin1_General_CP1_CI_AS StoreName,--st.LOCATION StoreName,
                DpId Collate SQL_Latin1_General_CP1_CI_AS DpId, dpName Collate SQL_Latin1_General_CP1_CI_AS dpName,ItemLookupCode Collate SQL_Latin1_General_CP1_CI_AS ItemLookupCode, ItemName Collate SQL_Latin1_General_CP1_CI_AS ItemName
            ";
            if (VCost)
            {
                RptStoreAll += ",Cost";
            }
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptStoreAll += " ,SupplierName Collate SQL_Latin1_General_CP1_CI_AS SupplierName";
            }
                RptStoreAll+= @"
, Qty
                ,SupplierCode Collate SQL_Latin1_General_CP1_CI_AS SupplierCode 
                ,Username Collate SQL_Latin1_General_CP1_CI_AS Username,DManager Collate SQL_Latin1_General_CP1_CI_AS DManager,FManager,BranchOwner Collate SQL_Latin1_General_CP1_CI_AS FManager 
                from " + $"({RmsRptStoreView})RptStore" + @"
                union all
                SELECT StoreFranchise,StoreID,'',StoreID,StoreName,
                DpId, dpName,ItemLookupCode, ItemName";
            if (VCost)
            {
                RptStoreAll += ",Cost";
            }
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptStoreAll += " ,SupplierName";
            }
                RptStoreAll+= @"
, Qty
                ,SupplierCode 
                ,Username,DManager,FManager,BranchOwner
                from " + $"({linkDyRptStoreView()})RptAXDBStore";
            return RptStoreAll;
        }
        //Tender 
        //Dynamic
        public string linkDyRptTender()
        {
            string Sql = @"select
RS.Store StoreId,
RS.TERMINAL Terminal,
s.username StoreName,
RH.Name Paidtype
,RS.Amounttendered TotalSales
,RS.Transactionid TransactionNumber
,cast (RS.Transdate as date)Transdate
--,FORMAT(DATEADD(SECOND, RS.Transtime, 0), 'HH:mm:ss') AS DinTime
,s.username Username,s.DManager,s.FManager
            from [192.168.1.210].AXDB.dbo.Retailtransactionpaymenttrans RS
inner join [192.168.1.210].AXDB.dbo.Retailtendertypetable RH on RH.Tendertypeid=RS.TENDERTYPE
left join [192.168.1.210].AXDBTest.dbo.Storeusers s on s.Storenumber collate SQL_Latin1_General_CP1_CI_AS=RS.Store ";
            Sql += " where Rs.Ispaymentcaptured = 1";
            if (VItemLookupCode)
                Sql = @" select
RS.Store StoreId,
RS.TERMINAL Terminal,
s.username StoreName,
RH.Name Paidtype
,cast (RS.Transdate as date)Transdate
,s.username Username,''DManager,''FManager
,salesd.ItemLookupCode
,salesd.TransactionNumber
,salesd.TotalSales Total 
            from [192.168.1.210].AXDB.dbo.Retailtransactionpaymenttrans RS
inner join [192.168.1.210].AXDB.dbo.Retailtendertypetable RH on RH.Tendertypeid=RS.TENDERTYPE
left join (select distinct storenumber ,username from TopSoft.dbo.Storeuser) s on s.Storenumber collate SQL_Latin1_General_CP1_CI_AS=RS.Store 
 left JOIN(select distinct Transactionnumber, ItemLookupCode,TotalSales from TopSoft.dbo.AXDBSalesAll) SalesD ON RS.TRANSACTIONID = SalesD.TransactionNumber collate SQL_Latin1_General_CP1_CI_AS
where Rs.Ispaymentcaptured = 1 ";
            return Sql;
        }
        public string linkDyRptTenderbybatch = @"select   Rt.Terminalid,
Rt.STOREID StoreId,
Rt.Salestotal,
   FORMAT(DATEADD(hour, 2, Rt.Closedatetimeutc), 'yyyy-MM-dd HH:mm') AS Closeddate,
    FORMAT(DATEADD(hour, 2, Rt.Startdatetimeutc), 'yyyy-MM-dd HH:mm') AS Startdate,
Rt.Batchid,
 Ttable.Name Paidtype,
 Rline.Transamount TotalSales,s.Username,DManager,FManager,BranchOwner
 from [192.168.1.210].AXDB.dbo.Retailposbatchtable Rt
 inner join [192.168.1.210].AXDB.dbo.Retailposbatchline Rline on Rline.BATCHID=Rt.BATCHID and Rt.TERMINALID=Rline.Batchterminalid
 inner join [192.168.1.210].AXDB.dbo.Retailtendertypetable Ttable on Ttable.Tendertypeid=Rline.TENDERTYPEID
left join AXDBTest.dbo.Storeusers s on s.storenumber=rt.STOREID
   where  Rline.Transamount != '0' and Ttable.Tendertypeid != '4'";
        public string DyRptTenderwithItem()
        {
            string Sql = @"SELECT DISTINCT
        salesd.storeid AS StoreId,
		TERMINAL Terminal,
		sa.username StoreName,
		sa.username username,
		        Paidtype,
        salesd.TransactionNumber,
        CAST(salesd.Transdate AS DATE) AS Transdate,
        salesd.ItemLookupCode,
        salesd.TotalSales AS TotalSales,sa.DManager,sa.FManager
    FROM (
        SELECT storeid, Transdate, TransactionNumber, ItemLookupCode, SUM(totalsales) AS TotalSales
        FROM TopSoft.dbo.AXDBSalesAll
        GROUP BY storeid, Transdate, TransactionNumber, ItemLookupCode
    ) SalesD
    LEFT JOIN (
        SELECT DISTINCT TERMINAL ,TransactionNumber, paidtype
        FROM AXDBTender
    ) RS ON RS.TransactionNumber = SalesD.TransactionNumber
	left join TopSoft.dbo.Storeuser sa on sa.Storenumber =SalesD.StoreID  ";
            return Sql;
        }
        public string linkDyRptTenderwithItem()
        {
            string Sql = @"SELECT DISTINCT
        salesd.TransactionNumber,
        salesd.storeid AS StoreId,
        Paidtype,
        CAST(salesd.Transdate AS DATE) AS Transdate,
        salesd.ItemLookupCode,
		sa.username StoreName,
        salesd.TotalSales AS TotalSales,''DManager,''FManager
    FROM (
        SELECT storeid, Transdate, TransactionNumber, ItemLookupCode, SUM(totalsales) AS TotalSales
        FROM [192.168.1.208\New].TopSoft.dbo.AXDBSalesAll
        GROUP BY storeid, Transdate, TransactionNumber, ItemLookupCode
    ) SalesD
    LEFT JOIN (
        SELECT DISTINCT TransactionNumber, paidtype
        FROM AXDBTender
    ) RS ON RS.TransactionNumber = SalesD.TransactionNumber
	left join (select distinct storenumber ,username from [192.168.1.208\New].TopSoft.dbo.Storeuser) sa on sa.Storenumber =SalesD.StoreID  ";
            return Sql;
        }

        public string RmsRptTenderwithItem()
        {
            string Sql = @"select distinct
Tend.StoreId,
S.Name StoreName,
Tend.Description Paidtype,
Tend.amount TotalSales,
  Tend.TransactionNumber,
 Cast (Trans.Time as date)Transdate
 ,FORMAT(Trans.Time, 'HH:mm') AS DinTime,s.Username,DManager,FManager,BranchOwner
from [DATA_CENTER].[dbo].TenderEntry Tend
inner join [DATA_CENTER].[dbo].[Transaction] Trans on Trans.TransactionNumber= Tend.TransactionNumber and Trans.StoreID=Tend.StoreID
left join CkproUsers.dbo.Storeuser S on S.RMSstoNumber =Convert(varchar(20),Tend.StoreID)";
            return Sql;
        }
        public string DyRptTender()
        {
            string Sql = @"select
RS.Store StoreId,
RS.TERMINAL Terminal,
s.username StoreName,
RH.Name Paidtype
,RS.Amounttendered TotalSales
,RS.Transactionid TransactionNumber
,cast (RS.Transdate as date)Transdate
--,FORMAT(DATEADD(SECOND, RS.Transtime, 0), 'HH:mm:ss') AS DinTime
,s.username Username,s.DManager,s.FManager ,s.BranchOwner
                from AXDB.dbo.Retailtransactionpaymenttrans RS
inner join AXDB.dbo.Retailtendertypetable RH on RH.Tendertypeid=RS.TENDERTYPE
inner join AXDBTest.dbo.Storeusers s on s.Storenumber collate SQL_Latin1_General_CP1_CI_AS=RS.Store 
where Rs.Ispaymentcaptured = 1";
            if (VItemLookupCode)
                Sql = @" select
RS.Store StoreId,
RS.TERMINAL Terminal,
s.username StoreName,
RH.Name Paidtype
,cast (RS.Transdate as date)Transdate
,s.username Username,''DManager,''FManager
,salesd.ItemLookupCode
,salesd.TransactionNumber
,salesd.TotalSales TotalSales 
            from [192.168.1.210].AXDB.dbo.Retailtransactionpaymenttrans RS
inner join [192.168.1.210].AXDB.dbo.Retailtendertypetable RH on RH.Tendertypeid=RS.TENDERTYPE
left join (select distinct storenumber ,username from TopSoft.dbo.Storeuser) s on s.Storenumber collate SQL_Latin1_General_CP1_CI_AS=RS.Store 
 left JOIN(select distinct Transactionnumber, ItemLookupCode,TotalSales from TopSoft.dbo.AXDBSalesAll) SalesD ON RS.TRANSACTIONID = SalesD.TransactionNumber collate SQL_Latin1_General_CP1_CI_AS
where Rs.Ispaymentcaptured = 1 ";
            return Sql;
        }
        public string DyRptTenderbybatch = @"select Rt.Terminalid,
s.Name StoreName,
Rt.STOREID StoreId,
Rt.Salestotal,
   FORMAT(DATEADD(hour, 2, Rt.Closedatetimeutc), 'yyyy-MM-dd HH:mm') AS Closeddate,
    FORMAT(DATEADD(hour, 2, Rt.Startdatetimeutc), 'yyyy-MM-dd HH:mm') AS Startdate,
Rt.Batchid,
 Ttable.Name Paidtype,
 Rline.Transamount TotalSales,s.username Username,s.DManager,s.FManager
 from AXDB.dbo.Retailposbatchtable Rt
 inner join AXDB.dbo.Retailposbatchline Rline on Rline.BATCHID=Rt.BATCHID and Rt.TERMINALID=Rline.Batchterminalid
 inner join AXDB.dbo.Retailtendertypetable Ttable on Ttable.Tendertypeid=Rline.TENDERTYPEID
left join  AXDBTest.dbo.Storeusers s on s.Storenumber collate SQL_Latin1_General_CP1_CI_AS=Rt.STOREID
   where  Rline.Transamount != '0' and Ttable.Tendertypeid != '4' ";
        //RMS
        public string linkRmsRptTender = @"select distinct
Tend.StoreId,
S.Name StoreName,
Tend.Description Paidtype,
Tend.amount TotalSales,
  Tend.TransactionNumber,
 Cast (Trans.Time as date)Transdate
 ,FORMAT(Trans.Time, 'HH:mm') AS DinTime,s.Username,DManager,FManager,BranchOwner
from [192.168.1.156].[DATA_CENTER].[dbo].TenderEntry Tend
inner join [192.168.1.156].[DATA_CENTER].[dbo].[Transaction] Trans on Trans.TransactionNumber= Tend.TransactionNumber and Trans.StoreID=Tend.StoreID
left join [192.168.1.156].CkproUsers.dbo.Storeuser S on S.RMSstoNumber =Convert(varchar(20),Tend.StoreID)";
        public string RmsRptTender()
        {
            string Sql = @"
select distinct
Tend.StoreId,
s.Name StoreName,
Tend.Description Paidtype,
Tend.amount TotalSales,
  Tend.TransactionNumber,
 Cast (Trans.Time as date)Transdate
 --,FORMAT(Trans.Time, 'HH:mm') AS DinTime
 ,s.Username,s.DManager,s.FManager ";
            if (VItemLookupCode)
                Sql += ",Item.ItemLookupCode ItemlookupCode ";
            Sql +=@"
from [DATA_CENTER].[dbo].TenderEntry Tend
inner join [DATA_CENTER].[dbo].[Transaction] Trans on Trans.TransactionNumber= Tend.TransactionNumber and 
Trans.StoreID=Tend.StoreID
left join [CkproUsers].dbo.Storeuser 
s on s.RMSstoNumber=Convert (nvarchar(5),Tend.StoreID) ";
            if (VItemLookupCode)
               Sql +=" left join [DATA_CENTER].[dbo].Item on Trans.ItemID= Trans.ItemID and Convert (nvarchar(5),item.storeid)=s.RMSstoNumber ";
            return Sql;
        }
        //Richcut
        public string RichCutRptTender() {
            string Sql = @"
select distinct
Tend.StoreId,
s.Name StoreName,
Tend.Description Paidtype,
Tend.amount TotalSales,
  Tend.TransactionNumber,
 Cast (Trans.Time as date)Transdate
 --,FORMAT(Trans.Time, 'HH:mm') AS DinTime
 ,s.Username,s.DManager,s.FManager ";
            if (VItemLookupCode)
                Sql += ",Item.ItemLookupCode ItemlookupCode ";
            Sql += @"
                from[RichCut_DATA_CENTER].[dbo].TenderEntry Tend
inner join [RichCut_DATA_CENTER].[dbo].[Transaction] Trans on Trans.TransactionNumber= Tend.TransactionNumber and 
Trans.StoreID=Tend.StoreID
inner join (select distinct Franchise,DManager,FManager,BranchOwner,Username,Name,RMSstoNumber from CkproUsers.dbo.Storeuser where Name='RichCut' or Name = 'RichCut_No' or Name = 'RichCut_Zayed') s on (SUBSTRING(s.RMSstoNumber, 2, 1)=Convert(varchar(5),Trans.[StoreID])) ";
            if (VItemLookupCode)
               Sql +=" left join [RichCut_DATA_CENTER].[dbo].Item on Trans.ItemID= Trans.ItemID and Convert (nvarchar(5),item.storeid)=(SUBSTRING(s.RMSstoNumber, 2, 1) ";
            return Sql;
        }
    public string RptTenderAll()
        {
            string RptTenderAll = @"
            select Storeid StoreidR,''StoreIdD,Storename Collate SQL_Latin1_General_CP1_CI_AS Storename,Paidtype Collate SQL_Latin1_General_CP1_CI_AS Paidtype
            ,TotalSales,Convert(varchar(20),TransactionNumber)TransactionNumber ,Transdate ,Storeid,
--DinTime Collate SQL_Latin1_General_CP1_CI_AS DinTime,
            Username Collate SQL_Latin1_General_CP1_CI_AS Username,DManager Collate SQL_Latin1_General_CP1_CI_AS DManager,FManager,BranchOwner Collate SQL_Latin1_General_CP1_CI_AS FManager ";
            if (VItemLookupCode)
                RptTenderAll += ",ItemlookupCode Collate SQL_Latin1_General_CP1_CI_AS ItemlookupCode  ";
            RptTenderAll += @"
from " + $"({RmsRptTender()})" + @"RptTender
            union all
            select '',Storeid StoreIdD,Storename,Paidtype,TotalSales,TransactionNumber,Transdate,Storeid,
--DinTime,
Username,DManager,FManager,BranchOwner ";
            if (VItemLookupCode)
                RptTenderAll += " ,ItemlookupCode ";
            RptTenderAll += @"
           from ";
            if (VItemLookupCode)
            {
                RptTenderAll += $"({linkDyRptTenderwithItem()})" + @"RptAXTender ";
            }
            else
            {
                RptTenderAll += $"({linkDyRptTender()})" + @"RptAXTender ";
            }

            return RptTenderAll;
        }
        //Connection String 
        public string Server { get; set; }
        public string AxdbConnection = string.Format("Server=192.168.1.210;User ID=sa;Password=P@ssw0rd;Database=AXDB;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        public string RichCutConnection = string.Format("Server=192.168.111.2;User ID=sa;Password=P@ssw0rd;Database=Richcut;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        public string RmsConnection = string.Format("Server=192.168.1.156;User ID=saApp;Password=P@ssw0rd;Database=DATA_CENTER;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        public string RmsBeforeConnection = string.Format("Server=192.168.1.156;User ID=saApp;Password=P@ssw0rd;Database=DATA_CENTER_Prev_Yrs;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        public string TopSoftConnection = string.Format("Server=192.168.1.208\\NEW;User ID=sa;Password=P@ssw0rd;Database=TopSoft;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        public string EasySoftConnection = string.Format("Server=192.168.1.76\\sql2016;User ID=mohamed;Password=P@ssw0rd12345;Database=Easysoft;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        //link with Branch Server to Get data from it's Server
        public string Branchip()
        {
            string BranchConnection = string.Format("Server=" + Server + ";User ID=sa;Password=P@ssw0rd;Database=RetailChannelDatabase;Connect Timeout=7200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            return BranchConnection;
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
select storeid ,count(distinct TransactionNumber)Trans1 from [192.168.1.156].DATA_CENTER.dbo.[TransactionEntry]
where cast(TransactionTime as date) BETWEEN @fromDate AND @toDate
group by storeid 
)s)TransactionsCount";
            }
            if (VSupplierId || VSupplierName||(Supplier != null && Supplier != "0"))
            {
                RptSaleslinkView += ",Sub.SupplierName,Sub.Code SupplierCode";
            }
            RptSaleslinkView += @"
                                    ,s.DManager,s.Username,s.FManager,s.BranchOwner from [192.168.1.156].DATA_CENTER.dbo.[TransactionEntry] T 
                                    left join  [192.168.1.156].DATA_CENTER.dbo.Item on Item.ID=T.ItemID and T.StoreID=Item.storeid
                                    left join  [192.168.1.156].DATA_CENTER.dbo.department dp on dp.ID=Item.DepartmentID and dp.storeid=Item.storeid
                                    left join  [192.168.1.156].DATA_CENTER.dbo.Stores on (Stores.STORE_ID=Item.storeid or Item.storeid is null) and Stores.STORE_ID=T.StoreID
                                    left join  [192.168.1.156].CkproUsers.dbo.Storeuser s on s.RMSstoNumber=Convert (varchar(20),T.StoreID )";
            if (VSupplierId || VSupplierName||(Supplier != null && Supplier != "0"))
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
select storeid ,count(distinct TransactionNumber)Trans1 from DATA_CENTER.dbo.[TransactionEntry]
where cast(TransactionTime as date) BETWEEN @fromDate AND @toDate
group by storeid 
)s)TransactionsCount";
            }
            if (VSupplierId || VSupplierName||(Supplier != null && Supplier != "0"))
            {
                RptSalesView += ",Sub.SupplierName,Sub.Code SupplierCode";
            }
            RptSalesView += @"
                                    ,s.DManager,s.Username,s.FManager ,s.BranchOwner from DATA_CENTER.dbo.[TransactionEntry] T 
                                    left join DATA_CENTER.dbo.Item on Item.ID=T.ItemID and T.StoreID=Item.storeid
                                    left join DATA_CENTER.dbo.department dp on dp.ID=Item.DepartmentID and dp.storeid=Item.storeid
                                    left join DATA_CENTER.dbo.Stores on (Stores.STORE_ID=Item.storeid or Item.storeid is null) and Stores.STORE_ID=T.StoreID
                                    left join CkproUsers.dbo.Storeuser s on s.RMSstoNumber=Convert (varchar(20),T.StoreID )";
            if (VSupplierId || VSupplierName||(Supplier != null && Supplier != "0"))
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
end As StoreFranchise,DManager,Username,FManager,BranchOwner ,SalesH.TRANSTIME DateIntime,'TMT' Company ";
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
            if (VDateInTime || VDateandtime || VPerHour)
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
end As StoreFranchise,DManager,Username,FManager,BranchOwner ,SalesH.TRANSTIME DateIntime,'TMT' Company ";
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
            if (VDateInTime || VDateandtime || VPerHour)
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
            if (VDepartment ||VDpId ||((Department != null && Department != "0") && Department != "0"))
            {
                query += ",Cat.CODE DpId,Cat.NAME DpName,Cat.RECID CategoryId ";
               }
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0")||VItemLookupCode || VItemName || ItemNameTxt != null || ItemLookupCodeTxt != null)
            {
                query += @",Inv.Primaryvendorid SupplierName,INV.[Primaryvendorid] SupplierCode,It.[NAME] ItemName,Inv.PRODUCT ";
            }
            query +=@"
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
            string query = @"select  [TransactionNumber], [StoreID], [Cost], [StoreName], [ItemLookupCode], [Tax], [ByDay], [ByMonth], [ByYear], [TransTime], [TransDate], [Qty], [TotalCostQty], [Price], [TotalSales], [TotalSalesTax], [TotalSalesWithoutTax], [StoreFranchise], [DpId], [DpName], [SupplierName], [SupplierCode], [DateIntime], [SalesStatus], [lineNum], [TransStatus], [StaffId], [Terminal], [StaffName],Case when StoreId='143' then 'Trade Mix' else 'TMT' end As Company ,s.DManager,s.Username,s.FManager,s.BranchOwner ";
            if (VItemName)
            {
                query += ",CIT.Name [ItemName]";
            }
            if (VTransactionCount && (!VStoreId && !VStoreName && !VItemLookupCode && !VItemName && !VTransactionNumber && !VSupplierName && !VSupplierId && !VCompany))
            {
                query += @",(select sum(trans1)from
(
select storeid ,count(distinct TransactionNumber)Trans1 from AXDBSalesAll
where cast(TRANSDATE as date) BETWEEN @fromDate AND @toDate 
group by storeid 
)s)TransactionsCount";
            }
            if (VDateInTime ||VDateandtime ||VPerHour)
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
            query += @" from AXDBSalesAll sales left join Storeuser S on S.storenumber= Sales.StoreID ";
            if (VItemName)
            {
                query += @"inner join   (select distinct ItemId,Product from  [192.168.1.210].AXDB.dbo.Inventtable) ca 
on sales.ItemLookupCode collate SQL_Latin1_General_CP1_CI_AS = ca.Itemid
inner join   (select distinct Product,Name from  [192.168.1.210].AXDB.dbo.Ecoresproducttranslation ) CIt on ca.Product = CIt.Product ";
            }
            query += @" where sales.SalesStatus !='0' 
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
select storeid ,count(distinct TransactionNumber)Trans1 from DATA_CENTER_Prev_Yrs.dbo.[TransactionEntry]
where cast(TransactionTime as date) BETWEEN @fromDate AND @toDate
group by storeid 
)s)TransactionsCount";
            }
            if (VSupplierId || VSupplierName || (Supplier != null && Supplier != "0"))
            {
                RptSalesView += ",Sub.SupplierName,Sub.Code SupplierCode";
            }
            RptSalesView += @"
                                    ,s.DManager,s.Username,s.FManager ,s.BranchOwner from DATA_CENTER_Prev_Yrs.dbo.[TransactionEntry] T 
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
								   inner join (select distinct Franchise,DManager,FManager,BranchOwner,Username,Name,RMSstoNumber from CkproUsers.dbo.Storeuser where Name='RichCut' or Name = 'RichCut_No' or Name = 'RichCut_Zayed') s on (SUBSTRING(s.RMSstoNumber, 2, 1)=Convert(varchar(5),It.[StoreID])) 
where It.[ItemLookupCode] LIKE '[0-9]%' ";
        //Sales ALL
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
            if (VDateInTime || VDateandtime || VPerHour)
            {
                RptSalesAllView += ",Convert(nvarchar(20),Time)Time,Convert(nvarchar(20),Dateandtime)Dateandtime ";
            }
            RptSalesAllView += @",
                                    Cast(TransDate as date)TransDate,qty,price,TotalSales,TransactionNumber COLLATE SQL_Latin1_General_CP1_CI_AS TransactionNumber,Cost 
                                     ,ByMonth,ByYear,ByDay,0 As StoreIdD,Tax,TotalSalesTax,TotalSalesWithoutTax,(TotalCostQty)TotalCostQty
                                     ,DManager COLLATE SQL_Latin1_General_CP1_CI_AS DManager,FManager,BranchOwner COLLATE SQL_Latin1_General_CP1_CI_AS FManager,Username COLLATE SQL_Latin1_General_CP1_CI_AS Username,Company COLLATE SQL_Latin1_General_CP1_CI_AS Company ";
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
            if (VDateInTime || VDateandtime || VPerHour)
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
            RptSalesAllView += @"from ("+linkDyRptSaleslive()+ @")r 
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
            if (VDateInTime || VDateandtime || VPerHour)
            {
                RptSalesAllView += ",Convert(nvarchar(20),Time)Time,Convert(nvarchar(20),Dateandtime)Dateandtime ";
            }
            RptSalesAllView += @",
                                    Cast(TransDate as date)TransDate,qty,price,TotalSales,TransactionNumber COLLATE SQL_Latin1_General_CP1_CI_AS TransactionNumber,Cost 
                                     ,ByMonth,ByYear,ByDay,0 As StoreIdD,Tax,TotalSalesTax,TotalSalesWithoutTax,(TotalCostQty)TotalCostQty
                                     ,DManager COLLATE SQL_Latin1_General_CP1_CI_AS DManager,FManager COLLATE SQL_Latin1_General_CP1_CI_AS FManager,BranchOwner COLLATE SQL_Latin1_General_CP1_CI_AS BranchOwner,Username COLLATE SQL_Latin1_General_CP1_CI_AS Username,Company COLLATE SQL_Latin1_General_CP1_CI_AS Company ";
            if (VSupplierId || VSupplierName||(Supplier != null && Supplier != "0"))
            {
                RptSalesAllView += ",SupplierName,SupplierCode ";
            }
            RptSalesAllView += @"from(" + linkRmsRptSalesView();
            if (FoodCategory)
            {
                RptSalesAllView += "where dp.Name in ('Breakfast','Burger','Cookies','Pizza', 'Cold Cut','Corn Dog','Crispy Chicken','Bakery','Donuts','Salad','French Fries') or Item.ItemLookupCode='55888' ";
            }
            RptSalesAllView += ")RptAXDBSalesAll" +
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
select SELECT [TransactionNumber], [StoreID], [Cost], [StoreName], [ItemLookupCode], [Tax], [ByDay], [ByMonth], [ByYear], [TransTime], [TransDate], [Qty], [TotalCostQty], [Price], [TotalSales], [TotalSalesTax], [TotalSalesWithoutTax], [StoreFranchise], [DpId], [DpName], [SupplierName], [SupplierCode],  [DateIntime], [SalesStatus], [TransStatus], [StaffId], [Terminal], [StaffName],storeid ,count(distinct TransactionNumber)Trans1 ";
                if (VItemName)
                {
                    RptSalesAllView += ",CIT.Name [ItemName]";
                }

                RptSalesAllView += @"from AXDBSalesAll
where cast(TRANSDATE as date) BETWEEN @fromDate AND @toDate
group by storeid 
)s)TransactionsCount";
            }
            if (VDateInTime || VDateandtime || VPerHour)
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
                                    ,s.DManager,s.FManager,s.BranchOwner,s.Username,Case when StoreId='143' then 'Trade Mix' else 'TMT' end As Company ";
            if (VSupplierId || VSupplierName||(Supplier != null && Supplier != "0"))
            {
                RptSalesAllView += ",SupplierName,SupplierCode ";
            }
            RptSalesAllView += @"from AXDBSalesAll r
                                      left join Storeuser s on s.storenumber  =Convert (varchar(20),r.StoreID ) collate SQL_Latin1_General_CP1_CI_AS ";
            if (VItemName)
            {
                RptSalesAllView += @" inner join   (select distinct ItemId,Product from  [192.168.1.210].AXDB.dbo.Inventtable) ca 
on r.ItemLookupCode collate SQL_Latin1_General_CP1_CI_AS = ca.Itemid
inner join   (select distinct Product,Name from  [192.168.1.210].AXDB.dbo.Ecoresproducttranslation )CIt on ca.Product = CIt.Product";
            }
            RptSalesAllView += @" where r.SalesStatus!='0' 
";
            if (FoodCategory)
            {
                RptSalesAllView += " AND  (dpname in ('Breakfast','Burger','Cookies','Pizza', 'Cold Cut','Corn Dog','Crispy Chicken','Bakery','Donuts','Salad','French Fries') or ItemlookupCode='55888')";
            }
            return RptSalesAllView;
        }
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
        public string RptStockFrom()
        {
            string view = @"
Go
delete from AXDBTest.dbo.Store1
Go
delete from AXDBTest.dbo.Store
insert into AXDBTest.dbo.Store 
select distinct Inv.INVENTLOCATIONID Store,cast(max(Inv.Modifieddatetime) as date)Ddate from AXDB.dbo.INVENTSUM Inv group by INVENTLOCATIONID
insert into AXDBTest.dbo.Store1
select
Salesd.Item Item,sum(Salesd.Q1) Q2,sum(SalesD2.Q1)Q3,
SalesH.INVENTLOCATIONID Store
from
AXDB.dbo.Retailtransactiontable SalesH 
inner join (select distinct Transactionid,sum(QTY)Q1,ITEMID Item from AXDB.dbo.Retailtransactionsalestrans group by Transactionid,ITEMID) SalesD
on SalesH.Transactionid = SalesD.Transactionid
inner join (select distinct Transactionid,sum(QTY)Q1,ITEMID Item from AXDB.dbo.Retailtransactionsalestrans
where  Inventstatussales=0  group by Transactionid,ITEMID )SalesD2
on SalesH.Transactionid = SalesD2.Transactionid
left join (select distinct cast(Ddate as date)Ddate, Store from AXDBTest.dbo.Store Inv) Inv on 
Salesh.INVENTLOCATIONID=Inv.Store collate SQL_Latin1_General_CP1_CI_AS
where   SalesH.Type = 2  and SalesH.Entrystatus != 1 and cast(SalesH.TRANSDATE as date)>=cast(getdate() as date)
and Salesh.INVENTLOCATIONID="+Server+@"
group by Salesd.Item,SalesH.INVENTLOCATIONID
Go
select (-sum(Q1)) Qty1,(-sum(Q2)) Qty2,(-sum(Q3)) Qty3,(sum(Q4)) Qty4,
sum(Q4)-((-sum(Q1))-(-sum(Q2)))-(-sum(Q3)) Total, item,Store
,It.Name ItemName,Cat.NAME dpName,Cat.CODE dpId from (
select
ZSalesd.Item collate SQL_Latin1_General_CP1_CI_AS Item ,sum(ZSalesd.Qty) Q1,0 Q2,0 Q3,0 Q4,
ZSalesH.INVENTLOCATIONID collate SQL_Latin1_General_CP1_CI_AS Store
from
[192.168.44.222\new].RetailChannelDatabase.ax.Retailtransactiontable ZSalesH 
inner join (select distinct Transactionid,sum(QTY)Qty,ITEMID Item from [192.168.44.222\new].RetailChannelDatabase.ax.Retailtransactionsalestrans group by Transactionid,ITEMID) zSalesD
on ZSalesH.Transactionid = ZSalesD.Transactionid
where   zSalesH.Type = 2  and zSalesH.Entrystatus != 1 and cast(zSalesH.TRANSDATE as date)>=cast(getdate() as date)
group by ZSalesd.Item,ZSalesH.INVENTLOCATIONID
union all
select Item ,0,sum(Q1)Q2,sum(Q2)Q3,0,Store from AXDBTest.dbo.Store1
group by Item,Store
union all 
select  Inv.ItemId Item,0,0,0, sum(Inv.Physicalinvent)Q4,Inv.INVENTLOCATIONID Store
from AXDB.dbo.INVENTSUM Inv
inner join AXDB.dbo.Inventtable Ca on Ca.Itemid=Inv.itemid
 where ca.Dataareaid = 'tmt' and  Inv.INVENTLOCATIONID is not null 
 group by Inv.INVENTLOCATIONID,Inv.ItemId
)R
inner join AXDB.dbo.Inventtable Ca on Ca.Itemid=R.Item
inner join AXDB.dbo.Ecoresproducttranslation It on ca.Product = It.Product
inner join AXDB.dbo.Ecoresproductcategory Re on It.product =Re.Product
left join AXDB.dbo.Ecorescategory CateN on Re.Category = CateN.Recid
INNER JOIN  (Select distinct Cat.ReCId,Cat.CODE,Cat.NAME from AXDB.dbo.Ecorescategory Cat)Cat on Re.Category = Cat.Recid
inner join (select Category,Barcode from [192.168.1.156].CkproUsers.dbo.Catlimit)C on C.Category=Cat.Name and C.barcode=R.Item
 where ca.Dataareaid = 'tmt'
group by Item,Store,It.Name,Cat.NAME,Cat.CODE";
            return view;
        }
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
        public string RptAXDBSalesViewCreate
= @"
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
    }

}
//change suppliercode, transactionnumber,storeid,dpid to string in rptsales and 2 and all 
// Views
