using System;
using System.Collections.Generic;

namespace CK.Models.AXDB;

public partial class Ecoresproducttranslation
{
    public string Description { get; set; } = null!;

    public string Languageid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public long Product { get; set; }

    public long Partition { get; set; }

    public long Recid { get; set; }

    public int Recversion { get; set; }

    public string Modifiedby { get; set; } = null!;
}
