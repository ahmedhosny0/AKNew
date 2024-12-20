﻿using System;
using System.Collections.Generic;
using CK.Models;
using Microsoft.EntityFrameworkCore;

namespace CK.Models;

public partial class DataCenterContext : DbContext
{
    public DataCenterContext()
    {
    }

    public DataCenterContext(DbContextOptions<DataCenterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<R> Rs { get; set; }

    public virtual DbSet<RptAxstore> RptAxstores { get; set; }

    public virtual DbSet<RptSale> RptSales { get; set; }

    public virtual DbSet<RptSalesAll> RptSalesAlls { get; set; }

    public virtual DbSet<RptSalesAxt> RptSalesAxts { get; set; }


    public virtual DbSet<RptStoreAll> RptStoreAlls { get; set; }

    public virtual DbSet<RptSupplier> RptSuppliers { get; set; }

    public virtual DbSet<SalesPdf> SalesPdfs { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Supplierpage> Supplierpages { get; set; }

    public virtual DbSet<TransactionEntry> TransactionEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.1.156;User ID=saApp;Password=P@ssw0rd;Database=DATA_CENTER;Connect Timeout=150;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Storeid }).HasName("PK_department_1");

            entity.ToTable("department");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Storeid).HasColumnName("storeid");
            entity.Property(e => e.Code)
                .HasMaxLength(17)
                .HasColumnName("code");
            entity.Property(e => e.DbtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("DBTimeStamp");
            entity.Property(e => e.Hqid).HasColumnName("HQID");
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => new { e.Storeid, e.Id, e.ItemLookupCode }).IsClustered(false);

            entity.ToTable("Item", tb => tb.HasTrigger("TR_ITEM_LOG"));

            entity.HasIndex(e => e.BinLocation, "IX_BinLocation").HasFillFactor(90);

            entity.HasIndex(e => e.CategoryId, "IX_CategoryID").HasFillFactor(90);

            entity.HasIndex(e => e.DepartmentId, "IX_DepartmentID").HasFillFactor(90);

            entity.HasIndex(e => e.Description, "IX_Description").HasFillFactor(90);

            entity.HasIndex(e => e.Hqid, "IX_HQID").HasFillFactor(90);

            entity.HasIndex(e => new { e.Storeid, e.DepartmentId }, "IX_Item").HasFillFactor(90);

            entity.HasIndex(e => e.ItemLookupCode, "IX_ItemLookupCode").HasFillFactor(90);

            entity.HasIndex(e => e.DbtimeStamp, "IX_Item_TimeStamp")
                .IsUnique()
                .IsClustered()
                .HasFillFactor(90);

            entity.HasIndex(e => e.SupplierId, "IX_SupplierID").HasFillFactor(90);

            entity.Property(e => e.Storeid).HasColumnName("storeid");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ItemLookupCode)
                .HasMaxLength(25)
                .HasDefaultValue("");
            entity.Property(e => e.BinLocation)
                .HasMaxLength(20)
                .HasDefaultValue("");
            entity.Property(e => e.BlockSalesAfterDate).HasColumnType("datetime");
            entity.Property(e => e.BlockSalesBeforeDate).HasColumnType("datetime");
            entity.Property(e => e.BlockSalesReason)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.BlockSalesScheduleId).HasColumnName("BlockSalesScheduleID");
            entity.Property(e => e.BuydownPrice).HasColumnType("money");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CommissionAmount).HasColumnType("money");
            entity.Property(e => e.CommissionMaximum).HasColumnType("money");
            entity.Property(e => e.Content)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DbtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("DBTimeStamp");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.ExtendedDescription)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.Hqid).HasColumnName("HQID");
            entity.Property(e => e.LastCost).HasColumnType("money");
            entity.Property(e => e.LastCounted).HasColumnType("datetime");
            entity.Property(e => e.LastReceived).HasColumnType("datetime");
            entity.Property(e => e.LastSold).HasColumnType("datetime");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.Msrp)
                .HasColumnType("money")
                .HasColumnName("MSRP");
            entity.Property(e => e.Notes)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.NumberFormat).HasMaxLength(20);
            entity.Property(e => e.PictureName)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.PriceA).HasColumnType("money");
            entity.Property(e => e.PriceB).HasColumnType("money");
            entity.Property(e => e.PriceC).HasColumnType("money");
            entity.Property(e => e.PriceLowerBound).HasColumnType("money");
            entity.Property(e => e.PriceUpperBound).HasColumnType("money");
            entity.Property(e => e.QuantityDiscountId).HasColumnName("QuantityDiscountID");
            entity.Property(e => e.ReplacementCost).HasColumnType("money");
            entity.Property(e => e.SaleEndDate).HasColumnType("datetime");
            entity.Property(e => e.SalePrice).HasColumnType("money");
            entity.Property(e => e.SaleScheduleId).HasColumnName("SaleScheduleID");
            entity.Property(e => e.SaleStartDate).HasColumnType("datetime");
            entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");
            entity.Property(e => e.SubDescription1)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.SubDescription2)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.SubDescription3)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.TaxId).HasColumnName("TaxID");
            entity.Property(e => e.Taxable).HasDefaultValue(true);
            entity.Property(e => e.TenderId).HasColumnName("TenderID");
            entity.Property(e => e.UnitOfMeasure)
                .HasMaxLength(4)
                .HasDefaultValue("");
            entity.Property(e => e.UsuallyShip)
                .HasMaxLength(255)
                .HasDefaultValue("");
        });

        modelBuilder.Entity<R>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RS");

            entity.Property(e => e.Barcode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATETIME");
            entity.Property(e => e.Day)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FromTable)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Item)
                .HasMaxLength(60)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Quantity).HasColumnType("numeric(32, 6)");
            entity.Property(e => e.Status)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.ToTable)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Transaction)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<RptAxstore>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RptAXStore");

            entity.Property(e => e.Cost).HasColumnType("numeric(32, 6)");
            entity.Property(e => e.Dmanager)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("DManager");
            entity.Property(e => e.DpId)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.DpName)
                .HasMaxLength(254)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("dpName");
            entity.Property(e => e.Fmanager)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("FManager");
            entity.Property(e => e.ItemLookupCode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.ItemName)
                .HasMaxLength(60)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.Qty).HasColumnType("numeric(32, 6)");
            entity.Property(e => e.StoreFranchise)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreId)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreName)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreNameInDy)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.SupplierCode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.SupplierName).HasMaxLength(30);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<RptSale>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RptSales");

            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.Dmanager)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("DManager");
            entity.Property(e => e.DpId)
                .HasMaxLength(17)
                .HasColumnName("DpID");
            entity.Property(e => e.DpName).HasMaxLength(30);
            entity.Property(e => e.Fmanager)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("FManager");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ItemLookupCode).HasMaxLength(25);
            entity.Property(e => e.ItemName).HasMaxLength(30);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.StoreFranchise).HasMaxLength(50);
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.StoreName).HasMaxLength(50);
            entity.Property(e => e.Tax).HasColumnType("money");
            entity.Property(e => e.TransTime).HasColumnType("datetime");
            entity.Property(e => e.TransactionNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<RptSalesAll>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RptSalesAll");

            entity.Property(e => e.Dmanager)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("DManager");
            entity.Property(e => e.DpId)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("DpID");
            entity.Property(e => e.DpName)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Fmanager)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("FManager");
            entity.Property(e => e.ItemLookupCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.StoreFranchise)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StoreIdR)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StoreName)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.TransTime).HasColumnType("datetime");
            entity.Property(e => e.TransactionNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<RptSalesAxt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RptSalesAxt");

            entity.Property(e => e.ItemLookupCode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.ItemName)
                .HasMaxLength(60)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<RptStoreAll>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RptStoreAll");

            entity.Property(e => e.Cost).HasColumnType("numeric(32, 6)");
            entity.Property(e => e.Dmanager)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("DManager");
            entity.Property(e => e.DpId)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.DpName)
                .HasMaxLength(254)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("dpName");
            entity.Property(e => e.Fmanager)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("FManager");
            entity.Property(e => e.ItemLookupCode)
                .HasMaxLength(25)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.ItemName)
                .HasMaxLength(60)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreFranchise)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.StoreIdD)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreName)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.SupplierCode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<RptSupplier>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RptSupplier");

            entity.Property(e => e.SupplierName).HasMaxLength(30);
        });

        modelBuilder.Entity<SalesPdf>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("SalesPdf");

            entity.Property(e => e.Barcode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATETIME");
            entity.Property(e => e.Day)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FromTable)
                .HasMaxLength(60)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Item)
                .HasMaxLength(60)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Quantity).HasColumnType("numeric(32, 6)");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ToTable)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Transaction)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK_STORES_1");

            entity.ToTable("STORES");

            entity.Property(e => e.StoreId)
                .ValueGeneratedNever()
                .HasColumnName("STORE_ID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Closed).HasColumnName("CLOSED");
            entity.Property(e => e.DatabaseName)
                .HasMaxLength(50)
                .HasColumnName("DATABASE_NAME");
            entity.Property(e => e.FirstTransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("FIRST_TRANSACTION_DATE");
            entity.Property(e => e.Franchise)
                .HasMaxLength(50)
                .HasColumnName("FRANCHISE");
            entity.Property(e => e.Government)
                .HasMaxLength(50)
                .HasColumnName("GOVERNMENT");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.InsideTown).HasColumnName("INSIDE_TOWN");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .HasColumnName("IP");
            entity.Property(e => e.LastTransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("LAST_TRANSACTION_DATE");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("LAST_UPDATE_DATE");
            entity.Property(e => e.Latitude).HasColumnType("decimal(28, 10)");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .HasColumnName("LOCATION");
            entity.Property(e => e.Longitude).HasColumnType("decimal(28, 10)");
            entity.Property(e => e.PriceCategory)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("PRICE_CATEGORY");
            entity.Property(e => e.Seasonal).HasColumnName("SEASONAL");
            entity.Property(e => e.StoreNameD365).HasMaxLength(50);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Storeid }).IsClustered(false);

            entity.ToTable("Supplier");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Storeid).HasColumnName("storeid");
            entity.Property(e => e.AccountNumber).HasMaxLength(20);
            entity.Property(e => e.Address1).HasMaxLength(30);
            entity.Property(e => e.Address2).HasMaxLength(30);
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Code).HasMaxLength(17);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(20);
            entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");
            entity.Property(e => e.CustomDate1).HasColumnType("datetime");
            entity.Property(e => e.CustomDate2).HasColumnType("datetime");
            entity.Property(e => e.CustomDate3).HasColumnType("datetime");
            entity.Property(e => e.CustomDate4).HasColumnType("datetime");
            entity.Property(e => e.CustomDate5).HasColumnType("datetime");
            entity.Property(e => e.CustomText1).HasMaxLength(30);
            entity.Property(e => e.CustomText2).HasMaxLength(30);
            entity.Property(e => e.CustomText3).HasMaxLength(30);
            entity.Property(e => e.CustomText4).HasMaxLength(30);
            entity.Property(e => e.CustomText5).HasMaxLength(30);
            entity.Property(e => e.DbtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("DBTimeStamp");
            entity.Property(e => e.EmailAddress).HasMaxLength(255);
            entity.Property(e => e.FaxNumber).HasMaxLength(30);
            entity.Property(e => e.Hqid).HasColumnName("HQID");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasColumnType("ntext");
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            entity.Property(e => e.State).HasMaxLength(20);
            entity.Property(e => e.SupplierName).HasMaxLength(30);
            entity.Property(e => e.TaxNumber).HasMaxLength(20);
            entity.Property(e => e.Terms).HasMaxLength(50);
            entity.Property(e => e.WebPageAddress).HasMaxLength(255);
            entity.Property(e => e.Zip).HasMaxLength(20);
        });

        modelBuilder.Entity<Supplierpage>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("supplierpage");

            entity.Property(e => e.ItemLookupCode).HasMaxLength(25);
            entity.Property(e => e.ItemName).HasMaxLength(30);
            entity.Property(e => e.StoreId).HasMaxLength(10);
            entity.Property(e => e.SupplierName).HasMaxLength(30);
        });

        modelBuilder.Entity<TransactionEntry>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.Id }).IsClustered(false);

            entity.ToTable("TransactionEntry");

            entity.HasIndex(e => new { e.StoreId, e.ItemId }, "<Name of Missing Index, sysname,>").HasFillFactor(90);

            entity.HasIndex(e => e.Quantity, "IX_Quantity").HasFillFactor(90);

            entity.HasIndex(e => new { e.StoreId, e.TransactionNumber }, "IX_TransactionEntry").HasFillFactor(90);

            entity.HasIndex(e => new { e.StoreId, e.ItemId }, "IX_TransactionEntry11").HasFillFactor(90);

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .HasDefaultValue("");
            entity.Property(e => e.Commission).HasColumnType("money");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.DbtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("DBTimeStamp");
            entity.Property(e => e.DetailId).HasColumnName("DetailID");
            entity.Property(e => e.DiscountReasonCodeId).HasColumnName("DiscountReasonCodeID");
            entity.Property(e => e.FullPrice).HasColumnType("money");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.QuantityDiscountId).HasColumnName("QuantityDiscountID");
            entity.Property(e => e.ReturnReasonCodeId).HasColumnName("ReturnReasonCodeID");
            entity.Property(e => e.SalesRepId).HasColumnName("SalesRepID");
            entity.Property(e => e.SalesTax).HasColumnType("money");
            entity.Property(e => e.TaxChangeReasonCodeId).HasColumnName("TaxChangeReasonCodeID");
            entity.Property(e => e.TransactionTime).HasColumnType("datetime");
            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
