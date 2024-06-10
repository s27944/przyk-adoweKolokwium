﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Contexts;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240610184458_Z przykładowymi danymi")]
    partial class Zprzykładowymidanymi
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.4.24267.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi.Models.Client", b =>
                {
                    b.Property<int>("clientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("clientID"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("LastName");

                    b.HasKey("clientID");

                    b.ToTable("Client");

                    b.HasData(
                        new
                        {
                            clientID = 1,
                            FirstName = "Jacek",
                            LastName = "Sasin"
                        });
                });

            modelBuilder.Entity("WebApi.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ClientID")
                        .HasColumnType("int")
                        .HasColumnName("ClientID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime?>("FulfilledAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("FulfilledAt");

                    b.Property<int>("StatusID")
                        .HasColumnType("int")
                        .HasColumnName("StatusID");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.HasIndex("StatusID");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            ClientID = 1,
                            CreatedAt = new DateTime(2024, 6, 10, 20, 44, 57, 731, DateTimeKind.Local).AddTicks(7431),
                            StatusID = 1
                        });
                });

            modelBuilder.Entity("WebApi.Models.Product", b =>
                {
                    b.Property<int>("productID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("productID"));

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.Property<decimal>("productPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.HasKey("productID");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            productID = 1,
                            productName = "Czekolada",
                            productPrice = 5m
                        });
                });

            modelBuilder.Entity("WebApi.Models.Product_Order", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int>("amount")
                        .HasColumnType("int")
                        .HasColumnName("Amount");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("Product_Order");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            OrderId = 1,
                            amount = 5
                        });
                });

            modelBuilder.Entity("WebApi.Models.Status", b =>
                {
                    b.Property<int>("statusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("statusID"));

                    b.Property<string>("statusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("statusID");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            statusID = 1,
                            statusName = "Utworzone"
                        },
                        new
                        {
                            statusID = 2,
                            statusName = "Zrealizowane"
                        });
                });

            modelBuilder.Entity("WebApi.Models.Order", b =>
                {
                    b.HasOne("WebApi.Models.Client", "Client")
                        .WithMany("Order")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Status", "Status")
                        .WithMany("Order")
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("WebApi.Models.Product_Order", b =>
                {
                    b.HasOne("WebApi.Models.Order", "Order")
                        .WithMany("Product_Order")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Product", "Product")
                        .WithMany("Product_Order")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApi.Models.Client", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebApi.Models.Order", b =>
                {
                    b.Navigation("Product_Order");
                });

            modelBuilder.Entity("WebApi.Models.Product", b =>
                {
                    b.Navigation("Product_Order");
                });

            modelBuilder.Entity("WebApi.Models.Status", b =>
                {
                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
