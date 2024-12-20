using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CK.Models.TopSoft;

public partial class TopSoftContext : DbContext
{
    public TopSoftContext()
    {
    }

    public TopSoftContext(DbContextOptions<TopSoftContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AreaCode> AreaCodes { get; set; }

    public virtual DbSet<BranchDatum> BranchData { get; set; }

    public virtual DbSet<CloseDay> CloseDays { get; set; }

    public virtual DbSet<CustomerCode> CustomerCodes { get; set; }

    public virtual DbSet<DsalesCode> DsalesCodes { get; set; }

    public virtual DbSet<HsalesCode> HsalesCodes { get; set; }

    public virtual DbSet<StreetCode> StreetCodes { get; set; }

    public virtual DbSet<ZoneCode> ZoneCodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.1.208\\New;User ID=sa;Password=P@ssw0rd;Database=TopSoft;Connect Timeout=150;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AreaCode>(entity =>
        {
            entity.HasKey(e => e.Serial).HasName("PK__AreaCode__1A00E0921A3F1947");

            entity.ToTable("AreaCode");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.ZoneSerialNavigation).WithMany(p => p.AreaCodes)
                .HasForeignKey(d => d.ZoneSerial)
                .HasConstraintName("FK_AreaCode_ZoneCode");
        });

        modelBuilder.Entity<BranchDatum>(entity =>
        {
            entity.HasKey(e => e.Serial).HasName("PK__BranchDa__1A00E092FEBD7D83");

            entity.Property(e => e.BranchIdD).HasMaxLength(20);
            entity.Property(e => e.BranchIdR).HasMaxLength(20);
            entity.Property(e => e.BranchName).HasMaxLength(255);
        });

        modelBuilder.Entity<CloseDay>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CloseDay");

            entity.Property(e => e.Alert).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CustomerCode>(entity =>
        {
            entity.HasKey(e => e.CustomerCode1).HasName("PK__Customer__0667852000A90BC8");

            entity.ToTable("CustomerCode");

            entity.Property(e => e.CustomerCode1).HasColumnName("CustomerCode");
            entity.Property(e => e.Address1).HasMaxLength(255);
            entity.Property(e => e.Address2).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Phone1).HasMaxLength(20);
            entity.Property(e => e.Phone2).HasMaxLength(20);
            entity.Property(e => e.Phone3).HasMaxLength(20);
        });

        modelBuilder.Entity<DsalesCode>(entity =>
        {
            entity.HasKey(e => e.Serial).HasName("PK__DSalesCo__1A00E092097C51A5");

            entity.ToTable("DSalesCode");

            entity.Property(e => e.CategoryName).HasMaxLength(255);
            entity.Property(e => e.ItemCode).HasMaxLength(100);

            entity.HasOne(d => d.SalesCodeNavigation).WithMany(p => p.DsalesCodes)
                .HasForeignKey(d => d.SalesCode)
                .HasConstraintName("FK_DSalesCode_SalesCode");
        });

        modelBuilder.Entity<HsalesCode>(entity =>
        {
            entity.HasKey(e => e.Serial).HasName("PK__HSalesCo__1A00E092667BB3BD");

            entity.ToTable("HSalesCode");

            entity.Property(e => e.BranchCode).HasMaxLength(100);
            entity.Property(e => e.CancelBy).HasMaxLength(100);
            entity.Property(e => e.Createdby).HasMaxLength(255);
            entity.Property(e => e.Createddatetime).HasColumnType("datetime");
            entity.Property(e => e.CustomerCode).HasMaxLength(100);
            entity.Property(e => e.Deliverytime).HasMaxLength(10);
            entity.Property(e => e.Message).HasMaxLength(300);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.SalesOrderDate).HasColumnType("datetime");
            entity.Property(e => e.Updateddatetime).HasColumnType("datetime");
        });

        modelBuilder.Entity<StreetCode>(entity =>
        {
            entity.HasKey(e => e.Serial).HasName("PK__StreetCo__1A00E092EB5C39B7");

            entity.ToTable("StreetCode");

            entity.Property(e => e.DeliveryTime).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.AreaSerialNavigation).WithMany(p => p.StreetCodes)
                .HasForeignKey(d => d.AreaSerial)
                .HasConstraintName("FK_StreetCode_AreaCode");

            entity.HasOne(d => d.BranchSerialNavigation).WithMany(p => p.StreetCodes)
                .HasForeignKey(d => d.BranchSerial)
                .HasConstraintName("FK_StreetCode_BranchSerial");

            entity.HasOne(d => d.ZoneSerialNavigation).WithMany(p => p.StreetCodes)
                .HasForeignKey(d => d.ZoneSerial)
                .HasConstraintName("FK_StreetCode_ZoneCode");
        });

        modelBuilder.Entity<ZoneCode>(entity =>
        {
            entity.HasKey(e => e.Serial).HasName("PK__ZoneCode__1A00E092E99C6070");

            entity.ToTable("ZoneCode");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
