namespace CK.DTO
{
    public class SalesOrderDTO
    {
        public string HSalesCode { get; set; } // HSales.Serial
        public string HSalesCode2 { get; set; } // HSales.SalesCode
        public string CustomerCode { get; set; } // HSales.CustomerCode
        public string CategoryName { get; set; } // HSales.CategoryName
        public string GrandTotal { get; set; } // CAST(HSales.GrandTotal AS NVARCHAR)
        public string GrandTotalWithFees { get; set; } // CAST(HSales.GrandTotalwithFees AS NVARCHAR)
        public string SalesOrderDate { get; set; } // CAST(HSales.SalesOrderDate AS NVARCHAR)
        public string CreatedDatetime { get; set; } // CAST(HSales.Createddatetime AS NVARCHAR)
        public string UpdatedDatetime { get; set; } // CAST(HSales.Updateddatetime AS NVARCHAR)
        public string CreatedBy { get; set; } // HSales.Createdby
        public string Notes { get; set; } // HSales.Notes
        public string DSalesCodeSerial { get; set; } // DSales.Serial
        public string DSalesCode { get; set; } // DSales.SalesCode
        public string ItemCode { get; set; } // DSales.ItemCode
        public string Price { get; set; } // CAST(DSales.Price AS NVARCHAR)
        public string Quantity { get; set; } // CAST(DSales.Quantity AS NVARCHAR)
        public string Total { get; set; } // CAST(DSales.Total AS NVARCHAR)
        public string ItemName { get; set; } // CIT.Name
        public string CustomerName { get; set; } // Cust.CustomerName
        public string Phone1 { get; set; } // Cust.Phone1
        public string Phone2 { get; set; } // Cust.Phone2
        public string Phone3 { get; set; } // Cust.Phone3
        public string Address1 { get; set; } // Cust.Address1
        public string Address2 { get; set; } // Cust.Address2
        public string ZoneCode { get; set; } // Zone.Serial
        public string ZoneCode2 { get; set; } // Zone.Code
        public string ZoneName { get; set; } // Zone.Name
        public string AreaCode { get; set; } // Area.Serial
        public string AreaCode2 { get; set; } // Area.Code
        public string AreaName { get; set; } // Area.Name
        public string StreetCode { get; set; } // Street.Serial
        public string StreetCode2 { get; set; } // Street.Code
        public string StreetName { get; set; } // Street.Name
        public string DeliveryTime { get; set; } // Street.DeliveryTime
        public string ServiceCost { get; set; } // Street.ServiceCost
        public string BranchCode { get; set; } // Branch.Serial
        public string BranchIdR { get; set; } // Branch.BranchIdR
        public string BranchIdD { get; set; } // Branch.BranchIdD
        public string BranchName { get; set; } // Branch.BranchName
        public string SalesStatus { get; set; }
    }
}

