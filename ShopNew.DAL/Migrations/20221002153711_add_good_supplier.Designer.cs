﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShopNew.DAL;

#nullable disable

namespace ShopNew.DAL.Migrations
{
    [DbContext(typeof(shop_newContext))]
    [Migration("20221002153711_add_good_supplier")]
    partial class add_good_supplier
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShopNew.DAL.Models.BarCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GoodId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GoodId");

                    b.ToTable("BarCodes");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.Good", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Article")
                        .HasColumnType("text");

                    b.Property<int>("GoodGroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("integer");

                    b.Property<int>("Unit")
                        .HasColumnType("integer");

                    b.Property<Guid>("Uuid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GoodGroupId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.GoodGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ShopId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("GoodGroups");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Kpp")
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Inn")
                        .HasColumnType("text");

                    b.Property<string>("Kpp")
                        .HasColumnType("text");

                    b.Property<string>("OrgName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ShopId")
                        .HasColumnType("integer");

                    b.Property<Guid>("Uuid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.BarCode", b =>
                {
                    b.HasOne("ShopNew.DAL.Models.Good", "Good")
                        .WithMany("BarCodes")
                        .HasForeignKey("GoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Good");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.Good", b =>
                {
                    b.HasOne("ShopNew.DAL.Models.GoodGroup", "GoodGroup")
                        .WithMany("Goods")
                        .HasForeignKey("GoodGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopNew.DAL.Models.Supplier", "Supplier")
                        .WithMany("Goods")
                        .HasForeignKey("SupplierId");

                    b.Navigation("GoodGroup");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.GoodGroup", b =>
                {
                    b.HasOne("ShopNew.DAL.Models.Shop", "Shop")
                        .WithMany("GoodGroups")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.Shop", b =>
                {
                    b.HasOne("ShopNew.DAL.Models.Organization", "Organization")
                        .WithMany("Shops")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.Supplier", b =>
                {
                    b.HasOne("ShopNew.DAL.Models.Shop", "Shop")
                        .WithMany("Suppliers")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.User", b =>
                {
                    b.HasOne("ShopNew.DAL.Models.Organization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.Good", b =>
                {
                    b.Navigation("BarCodes");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.GoodGroup", b =>
                {
                    b.Navigation("Goods");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.Organization", b =>
                {
                    b.Navigation("Shops");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.Shop", b =>
                {
                    b.Navigation("GoodGroups");

                    b.Navigation("Suppliers");
                });

            modelBuilder.Entity("ShopNew.DAL.Models.Supplier", b =>
                {
                    b.Navigation("Goods");
                });
#pragma warning restore 612, 618
        }
    }
}
