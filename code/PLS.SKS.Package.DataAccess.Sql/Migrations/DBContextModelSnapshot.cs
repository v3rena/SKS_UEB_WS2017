﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using PLS.SKS.Package.DataAccess.Entities;
using PLS.SKS.Package.DataAccess.Sql;
using System;

namespace PLS.SKS.Package.DataAccess.Sql.Migrations
{
    [DbContext(typeof(DbContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PLS.SKS.Package.DataAccess.Entities.HopArrival", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Status");

                    b.Property<int>("TrackingInformationId");

                    b.HasKey("Id");

                    b.HasIndex("TrackingInformationId");

                    b.ToTable("HopArrivals");
                });

            modelBuilder.Entity("PLS.SKS.Package.DataAccess.Entities.Parcel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RecipientId");

                    b.Property<int>("TrackingInformationId");

                    b.Property<string>("TrackingNumber");

                    b.Property<float>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("TrackingInformationId");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("PLS.SKS.Package.DataAccess.Entities.Recipient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.ToTable("Recipients");
                });

            modelBuilder.Entity("PLS.SKS.Package.DataAccess.Entities.TrackingInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("State");

                    b.HasKey("Id");

                    b.ToTable("TrackingInformations");
                });

            modelBuilder.Entity("PLS.SKS.Package.DataAccess.Entities.Truck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<decimal>("Duration");

                    b.Property<decimal>("Latitude");

                    b.Property<decimal>("Longitude");

                    b.Property<string>("NumberPlate");

                    b.Property<decimal>("Radius");

                    b.Property<int?>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Trucks");
                });

            modelBuilder.Entity("PLS.SKS.Package.DataAccess.Entities.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<decimal>("Duration");

                    b.Property<int?>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("PLS.SKS.Package.DataAccess.Entities.HopArrival", b =>
                {
                    b.HasOne("PLS.SKS.Package.DataAccess.Entities.TrackingInformation", "TrackingInformation")
                        .WithMany()
                        .HasForeignKey("TrackingInformationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PLS.SKS.Package.DataAccess.Entities.Parcel", b =>
                {
                    b.HasOne("PLS.SKS.Package.DataAccess.Entities.Recipient", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PLS.SKS.Package.DataAccess.Entities.TrackingInformation", "TrackingInformation")
                        .WithMany()
                        .HasForeignKey("TrackingInformationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PLS.SKS.Package.DataAccess.Entities.Truck", b =>
                {
                    b.HasOne("PLS.SKS.Package.DataAccess.Entities.Warehouse")
                        .WithMany("Trucks")
                        .HasForeignKey("WarehouseId");
                });

            modelBuilder.Entity("PLS.SKS.Package.DataAccess.Entities.Warehouse", b =>
                {
                    b.HasOne("PLS.SKS.Package.DataAccess.Entities.Warehouse")
                        .WithMany("NextHops")
                        .HasForeignKey("WarehouseId");
                });
#pragma warning restore 612, 618
        }
    }
}
