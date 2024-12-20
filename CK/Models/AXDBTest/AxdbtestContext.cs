using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CK.Models.AXDBTest;

public partial class AxdbtestContext : DbContext
{
    public AxdbtestContext()
    {
    }

    public AxdbtestContext(DbContextOptions<AxdbtestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustInv> CustInvs { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Itax> Itaxes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.1.210;database=AXDBTEst;user id=sa;password=P@ssw0rd;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustInv>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CustInv");

            entity.Property(e => e.Code).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.No).HasMaxLength(50);
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasMaxLength(20);
            entity.Property(e => e.CustId).HasMaxLength(10);
            entity.Property(e => e.Depa).HasMaxLength(50);
            entity.Property(e => e.Descript).HasMaxLength(50);
            entity.Property(e => e.ItemCode).HasMaxLength(40);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.SalesId).HasMaxLength(30);
            entity.Property(e => e.TransDate).HasMaxLength(70);
        });

        modelBuilder.Entity<Itax>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ITax__3214EC07C904A8CB");

            entity.ToTable("ITax");

            entity.Property(e => e.ItemCode).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
