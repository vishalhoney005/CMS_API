using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Speridian.CMS.PL.Models;

public partial class CmsDbContext : DbContext
{
    public CmsDbContext()
    {
    }

    public CmsDbContext(DbContextOptions<CmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerMaster> CustomerMasters { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<GetBestSellingItem> GetBestSellingItems { get; set; }

    public virtual DbSet<GetHappyCustomer> GetHappyCustomers { get; set; }

    public virtual DbSet<GetOverallRating> GetOverallRatings { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Monthbestsell> Monthbestsells { get; set; }

    public virtual DbSet<OrderInvoice> OrderInvoices { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SPCOKLAP-5559\\SQLEXPRESS;Database=CMS_DB;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerMaster>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.ToTable("Customer_Master");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Customer_Id");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .HasColumnName("Customer_Name");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .HasColumnName("Phone_No");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.Text).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedbacks_Customer_Master");
        });

        modelBuilder.Entity<GetBestSellingItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetBestSellingItem");

            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemNo).HasColumnName("Item_no");
        });

        modelBuilder.Entity<GetHappyCustomer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetHappyCustomers");

            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .HasColumnName("Customer_Name");
        });

        modelBuilder.Entity<GetOverallRating>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetOverallRating");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.ItemNo);

            entity.ToTable("Menu");

            entity.Property(e => e.ItemNo).HasColumnName("Item_No");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasColumnName("Image_Url");
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .HasColumnName("Item_Name");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Monthbestsell>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Monthbestsell");

            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemNo).HasColumnName("Item_no");
            entity.Property(e => e.Month).HasMaxLength(30);
        });

        modelBuilder.Entity<OrderInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceNo);

            entity.Property(e => e.InvoiceNo).HasColumnName("Invoice_no");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.PayId).HasColumnName("Pay_Id");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderInvoices)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_OrderInvoices_Customer_Master");

            entity.HasOne(d => d.Pay).WithMany(p => p.OrderInvoices)
                .HasForeignKey(d => d.PayId)
                .HasConstraintName("FK_OrderInvoices_Payments");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceNo).HasColumnName("Invoice_No");
            entity.Property(e => e.ItemNo).HasColumnName("Item_No");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.InvoiceNoNavigation).WithMany()
                .HasForeignKey(d => d.InvoiceNo)
                .HasConstraintName("FK_OrderItems_OrderInvoices");

            entity.HasOne(d => d.ItemNoNavigation).WithMany()
                .HasForeignKey(d => d.ItemNo)
                .HasConstraintName("FK_OrderItems_Menu");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK_Payment");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("Payment_Id");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<UserRefreshToken>(entity =>
        {
            entity.ToTable("User_RefreshToken");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
