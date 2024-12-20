using System;
using System.Collections.Generic;

namespace CK.Models.AXDBTest;
public partial class InvoiceDetail
{
    public string? CustId { get; set; }

    public string? ItemCode { get; set; }

    public string? Location { get; set; }

    public string? Code { get; set; }

    public string? Depa { get; set; }

    public string? Descript { get; set; }

    public string? TransDate { get; set; }

    public double? Cost { get; set; }

    public double? Qty { get; set; }

    public double? CostA { get; set; }

    public double? Tax { get; set; }

    public string? SalesId { get; set; }

    public string? CreatedDate { get; set; }
}
