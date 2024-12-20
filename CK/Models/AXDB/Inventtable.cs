using System;
using System.Collections.Generic;

namespace CK.Models.AXDB;

public partial class Inventtable
{
    public string Itemid { get; set; } = null!;

    public int Abccontributionmargin { get; set; }

    public int Abcrevenue { get; set; }

    public int Abctieup { get; set; }

    public int Abcvalue { get; set; }

    public string AlcoholmanufactureridRu { get; set; } = null!;

    public string AlcoholproductiontypeidRu { get; set; } = null!;

    public decimal AlcoholstrengthRu { get; set; }

    public string Altconfigid { get; set; } = null!;

    public string Altinventcolorid { get; set; } = null!;

    public string Altinventsizeid { get; set; } = null!;

    public string Altinventstyleid { get; set; } = null!;

    public string Altitemid { get; set; } = null!;

    public decimal ApproxtaxvalueBr { get; set; }

    public string AssetgroupidRu { get; set; } = null!;

    public string AssetidRu { get; set; } = null!;

    public int Autoreportfinished { get; set; }

    public int Batchmergedatecalculationmethod { get; set; }

    public string Batchnumgroupid { get; set; } = null!;

    public string Bomcalcgroupid { get; set; } = null!;

    public int Bomlevel { get; set; }

    public int Bommanualreceipt { get; set; }

    public string Bomunitid { get; set; } = null!;

    public string BrandcodeidMx { get; set; } = null!;

    public string Commissiongroupid { get; set; } = null!;

    public string Costgroupid { get; set; } = null!;

    public int Costmodel { get; set; }

    public long CustomsexporttariffcodetableIn { get; set; }

    public long CustomsimporttariffcodetableIn { get; set; }

    public long Defaultdimension { get; set; }

    public decimal Density { get; set; }

    public decimal Depth { get; set; }

    public string ExceptioncodeBr { get; set; } = null!;

    public long ExcisetariffcodesIn { get; set; }

    public long EximproductgrouptableIn { get; set; }

    public int Fiscallifoavoidcalc { get; set; }

    public decimal Fiscallifonormalvalue { get; set; }

    public int Fiscallifonormalvaluecalc { get; set; }

    public int Forecastdmpinclude { get; set; }

    public decimal Grossdepth { get; set; }

    public decimal Grossheight { get; set; }

    public decimal Grosswidth { get; set; }

    public decimal Height { get; set; }

    public int IcmsonserviceBr { get; set; }

    public long Intrastatcommodity { get; set; }

    public int Intrastatexclude { get; set; }

    public string IntrastatprocidCz { get; set; } = null!;

    public long Inventfiscallifogroup { get; set; }

    public string InventproducttypeBr { get; set; } = null!;

    public string Itembuyergroupid { get; set; } = null!;

    public int Itemdimcostprice { get; set; }

    public string Itempricetolerancegroupid { get; set; } = null!;

    public int Itemtype { get; set; }

    public string MarkupcodeRu { get; set; } = null!;

    public int Matchingpolicy { get; set; }

    public decimal Minimumpalletquantity { get; set; }

    public string Namealias { get; set; } = null!;

    public decimal Netweight { get; set; }

    public long NgpcodestableFr { get; set; }

    public string NrtaxgroupLv { get; set; } = null!;

    public string Origcountryregionid { get; set; } = null!;

    public string Origcountyid { get; set; } = null!;

    public string Origstateid { get; set; } = null!;

    public string Packaginggroupid { get; set; } = null!;

    public string PackingRu { get; set; } = null!;

    public string Pdsbaseattributeid { get; set; } = null!;

    public int Pdsbestbefore { get; set; }

    public decimal Pdscwwmsminimumpalletqty { get; set; }

    public decimal Pdscwwmsqtyperlayer { get; set; }

    public decimal Pdscwwmsstandardpalletqty { get; set; }

    public string Pdsfreightallocationgroupid { get; set; } = null!;

    public string Pdsitemrebategroupid { get; set; } = null!;

    public int Pdspotencyattribrecording { get; set; }

    public int Pdsshelfadvice { get; set; }

    public int Pdsshelflife { get; set; }

    public decimal Pdstargetfactor { get; set; }

    public int Pdsvendorcheckitem { get; set; }

    public int Phantom { get; set; }

    public string PkwiucodePl { get; set; } = null!;

    public string Pmfplanningitemid { get; set; } = null!;

    public int Pmfproducttype { get; set; }

    public decimal Pmfyieldpct { get; set; }

    public string Primaryvendorid { get; set; } = null!;

    public int Prodflushingprincip { get; set; }

    public string Prodgroupid { get; set; } = null!;

    public string Prodpoolid { get; set; } = null!;

    public long Product { get; set; }

    public string Projcategoryid { get; set; } = null!;

    public string Propertyid { get; set; } = null!;

    public int Purchmodel { get; set; }

    public decimal Qtyperlayer { get; set; }

    public string Reqgroupid { get; set; } = null!;

    public string SadratecodePl { get; set; } = null!;

    public decimal Salescontributionratio { get; set; }

    public int Salesmodel { get; set; }

    public decimal Salespercentmarkup { get; set; }

    public int Salespricemodelbasic { get; set; }

    public decimal Scrapconst { get; set; }

    public decimal Scrapvar { get; set; }

    public string Serialnumgroupid { get; set; } = null!;

    public long ServicecodetableIn { get; set; }

    public int SkipintracompanysyncRu { get; set; }

    public int Sortcode { get; set; }

    public string Standardconfigid { get; set; } = null!;

    public string Standardinventcolorid { get; set; } = null!;

    public string Standardinventsizeid { get; set; } = null!;

    public string Standardinventstyleid { get; set; } = null!;

    public decimal Standardpalletquantity { get; set; }

    public decimal Statisticsfactor { get; set; }

    public decimal Taraweight { get; set; }

    public int TaxationoriginBr { get; set; }

    public string TaxfiscalclassificationBr { get; set; } = null!;

    public decimal Taxpackagingqty { get; set; }

    public string TaxservicecodeBr { get; set; } = null!;

    public decimal Unitvolume { get; set; }

    public int Usealtitemid { get; set; }

    public decimal Width { get; set; }

    public int Wmsarrivalhandlingtime { get; set; }

    public string Wmspallettypeid { get; set; } = null!;

    public int Wmspickingqtytime { get; set; }

    public int DsaIn { get; set; }

    public int ExciserecordtypeIn { get; set; }

    public string SatcodeidMx { get; set; } = null!;

    public string SattarifffractionMx { get; set; } = null!;

    public long HsncodetableIn { get; set; }

    public long ServiceaccountingcodetableIn { get; set; }

    public int ExemptIn { get; set; }

    public string Productlifecyclestateid { get; set; } = null!;

    public int ScaleindicatorBr { get; set; }

    public string CnpjBr { get; set; } = null!;

    public string Dataareaid { get; set; } = null!;

    public long Partition { get; set; }

    public long Recid { get; set; }

    public int Recversion { get; set; }

    public DateTime Modifieddatetime { get; set; }

    public string Modifiedby { get; set; } = null!;

    public DateTime Createddatetime { get; set; }

    public string Createdby { get; set; } = null!;

    public int NongstIn { get; set; }

    public long Taxratetype { get; set; }

    public string Revrecdefaultrevenuerecognitionschedule { get; set; } = null!;

    public int Revrecexcludefromcarveout { get; set; }

    public int Revrecmedianprice { get; set; }

    public decimal Revrecmedianpricemaximumtolerance { get; set; }

    public decimal Revrecmedianpriceminimumtolerance { get; set; }

    public int Revrecrevenuerecognitionenabled { get; set; }

    public int Revrecrevenuetype { get; set; }

    public int Revrecbundle { get; set; }

    public string Altinventversionid { get; set; } = null!;

    public string Standardinventversionid { get; set; } = null!;

    public decimal Intrastatchargeperkg { get; set; }

    public int Hmimindicator { get; set; }

    public int Coodualuseproduct { get; set; }

    public string Coodualusecode { get; set; } = null!;

    public int Costbomlevel { get; set; }

    public string FreenotesgroupIt { get; set; } = null!;

    public string Itmarrivalgroupid { get; set; } = null!;

    public string Itmcommoditycodeid { get; set; } = null!;

    public string Itmcustomsdescid { get; set; } = null!;

    public string Itmoverundertolerancegroupid { get; set; } = null!;

    public string Itmcosttypegroupid { get; set; } = null!;

    public string Itmcosttransfergroupid { get; set; } = null!;

    public int DisplayhazardMx { get; set; }
}
