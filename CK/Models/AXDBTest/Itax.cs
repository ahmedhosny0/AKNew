using System;
using System.Collections.Generic;

namespace CK.Models.AXDBTest;

public partial class Itax
{
    public int Id { get; set; }

    public string? ItemCode { get; set; }

    public int? Tax { get; set; }
}
