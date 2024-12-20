using System;
using System.Collections.Generic;

namespace CK.Models.AXDB;

public partial class Ecoresproductcategory
{
    public long Category { get; set; }

    public long Categoryhierarchy { get; set; }

    public long Product { get; set; }

    public long Partition { get; set; }

    public long Recid { get; set; }

    public int Recversion { get; set; }

    public DateTime Modifieddatetime { get; set; }

    public decimal Displayorder { get; set; }
}
