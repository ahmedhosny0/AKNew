using System;
using System.Collections.Generic;

namespace CK.Models.AXDB;

public partial class Inventsum
{
    public decimal Arrived { get; set; }

    public decimal Availordered { get; set; }

    public decimal Availphysical { get; set; }

    public int Closed { get; set; }

    public int Closedqty { get; set; }

    public decimal Deducted { get; set; }

    public string Inventdimid { get; set; } = null!;

    public string Itemid { get; set; } = null!;

    public DateTime Lastupddateexpected { get; set; }

    public DateTime Lastupddatephysical { get; set; }

    public decimal Onorder { get; set; }

    public decimal Ordered { get; set; }

    public decimal Pdscwarrived { get; set; }

    public decimal Pdscwavailordered { get; set; }

    public decimal Pdscwavailphysical { get; set; }

    public decimal Pdscwdeducted { get; set; }

    public decimal Pdscwonorder { get; set; }

    public decimal Pdscwordered { get; set; }

    public decimal Pdscwphysicalinvent { get; set; }

    public decimal Pdscwpicked { get; set; }

    public decimal Pdscwpostedqty { get; set; }

    public decimal Pdscwquotationissue { get; set; }

    public decimal Pdscwquotationreceipt { get; set; }

    public decimal Pdscwreceived { get; set; }

    public decimal Pdscwregistered { get; set; }

    public decimal Pdscwreservordered { get; set; }

    public decimal Pdscwreservphysical { get; set; }

    public decimal Physicalinvent { get; set; }

    public decimal Physicalvalue { get; set; }

    public decimal PhysicalvalueseccurRu { get; set; }

    public decimal Picked { get; set; }

    public decimal Postedqty { get; set; }

    public decimal Postedvalue { get; set; }

    public decimal PostedvalueseccurRu { get; set; }

    public decimal Quotationissue { get; set; }

    public decimal Quotationreceipt { get; set; }

    public decimal Received { get; set; }

    public decimal Registered { get; set; }

    public decimal Reservordered { get; set; }

    public decimal Reservphysical { get; set; }

    public int Isexcludedfrominventoryvalue { get; set; }

    public string Dataareaid { get; set; } = null!;

    public long Partition { get; set; }

    public long Recid { get; set; }

    public int Recversion { get; set; }

    public DateTime Modifieddatetime { get; set; }

    public string Configid { get; set; } = null!;

    public string Inventbatchid { get; set; } = null!;

    public string Inventcolorid { get; set; } = null!;

    public string InventgtdidRu { get; set; } = null!;

    public string Inventlocationid { get; set; } = null!;

    public string InventowneridRu { get; set; } = null!;

    public string InventprofileidRu { get; set; } = null!;

    public string Inventserialid { get; set; } = null!;

    public string Inventsiteid { get; set; } = null!;

    public string Inventsizeid { get; set; } = null!;

    public string Inventstatusid { get; set; } = null!;

    public string Inventstyleid { get; set; } = null!;

    public string Licenseplateid { get; set; } = null!;

    public string Wmslocationid { get; set; } = null!;

    public string Wmspalletid { get; set; } = null!;

    public string Inventdimension1 { get; set; } = null!;

    public string Inventdimension2 { get; set; } = null!;

    public string Inventdimension3 { get; set; } = null!;

    public string Inventdimension4 { get; set; } = null!;

    public string Inventdimension5 { get; set; } = null!;

    public string Inventdimension6 { get; set; } = null!;

    public string Inventdimension7 { get; set; } = null!;

    public string Inventdimension8 { get; set; } = null!;

    public DateTime Inventdimension9 { get; set; }

    public int Inventdimension9tzid { get; set; }

    public decimal Inventdimension10 { get; set; }

    public string Inventversionid { get; set; } = null!;

    public string Inventdimension11 { get; set; } = null!;

    public string Inventdimension12 { get; set; } = null!;
}
