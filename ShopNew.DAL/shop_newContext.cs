using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ShopNew.DAL.Models;

namespace ShopNew.DAL
{
    public partial class shop_newContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Supplier> Suppliers { get; set; } 
        public DbSet<GoodGroup> GoodGroups { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<BarCode> BarCodes { get; set; }

        public shop_newContext()
        {
        }

        public shop_newContext(DbContextOptions<shop_newContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=172.172.172.214;Port=5432;Database=shop_new;User Id=postgres;Password=312301001;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Organization)
                .WithMany(o => o.Users)
                .HasForeignKey(u => u.OrganizationId);
            modelBuilder.Entity<Shop>()
                .HasOne(u => u.Organization)
                .WithMany(o => o.Shops)
                .HasForeignKey(u => u.OrganizationId);

            modelBuilder.Entity<Supplier>()
                .HasOne(s => s.Shop)
                .WithMany(s => s.Suppliers)
                .HasForeignKey(s => s.ShopId);
            modelBuilder.Entity<GoodGroup>()
                .HasOne(g => g.Shop)
                .WithMany(s => s.GoodGroups)
                .HasForeignKey(g => g.ShopId);
            modelBuilder.Entity<Good>()
                .HasOne(g => g.GoodGroup)
                .WithMany(gr => gr.Goods)
                .HasForeignKey(g => g.GoodGroupId);
            modelBuilder.Entity<Good>()
                .HasOne(g => g.Supplier)
                .WithMany(s => s.Goods)
                .HasForeignKey(g => g.SupplierId);
            modelBuilder.Entity<BarCode>()
                .HasOne(b => b.Good)
                .WithMany(g => g.BarCodes)
                .HasForeignKey(b => b.GoodId);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
