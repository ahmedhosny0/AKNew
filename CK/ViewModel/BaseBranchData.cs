namespace CK.ViewModel
{
    public class BaseBranchData : ConnectionDB
    {
        public bool TMT { get; set; }
        public bool RMS { get; set; }
        public bool DBbefore { get; set; }
        public bool RichCut { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public string? Store { get; set; } = "0";
        public string? Storeall { get; set; } = "0";
        public string? StoreRM { get; set; } = "0";
        public string? StoreDy { get; set; } = "0";
        public string? StoreRich { get; set; } = "0";
        public string? ToStore { get; set; } = "0";
        public bool VPerDay { get; set; }
        public bool VStoreId { get; set; }
        public bool VStoreName { get; set; }
        public bool VDepartment { get; set; }
        public string? Department { get; set; }
        public bool VQty { get; set; }
        public bool VCost { get; set; }
        public bool VPrice { get; set; }
        public bool VTotalSales { get; set; }
        public bool VTransactionNumber { get; set; }
        public bool VItemLookupCode { get; set; }
        public bool VItemName { get; set; }
        public string? Supplier { get; set; }
        public bool VSupplierId { get; set; }
        public bool VSupplierName { get; set; }
        public double TotalSales { get; set; }
        public bool VDpId { get; set; }
        public bool isDmanager { get; set; }
        public bool isFmanager { get; set; }
        public bool isUsername { get; set; }
        public int actionValue { get; set; }
        public string? Franchise { get; set; }
        public bool VFranchise { get; set; }
        public string? ItemLookupCodeTxt { get; set; }
        public string? ItemNameTxt { get; set; }
        public bool exportAfterClick { get; set; }
    }
}
