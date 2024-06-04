﻿// <auto-generated />
using System;
using Assessment_1.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assessment_1.Migrations
{
    [DbContext(typeof(TaxiServiceDbContext))]
    [Migration("20240604084415_InitialCreate2")]
    partial class InitialCreate2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Assessment_1.Entity.Driver", b =>
                {
                    b.Property<int>("DriverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("DriverId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("Assessment_1.Entity.Ratings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.Property<int>("RideId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Assessment_1.Entity.Ride", b =>
                {
                    b.Property<int>("RideId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<string>("Drop")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeSpan?>("EndTime")
                        .HasColumnType("time(6)");

                    b.Property<string>("Pickup")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RiderId")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("StartTime")
                        .HasColumnType("time(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RideId");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("Assessment_1.Entity.Rider", b =>
                {
                    b.Property<int>("RiderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RiderId");

                    b.ToTable("Riders");
                });
#pragma warning restore 612, 618
        }
    }
}
