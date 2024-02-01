﻿// <auto-generated />
using System;
using AppDataWorker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppDataWorker.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppDataWorker.Models.Analog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("productId");

                    b.ToTable("Analogs");
                });

            modelBuilder.Entity("AppDataWorker.Models.Apteka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("hub")
                        .HasColumnType("bit");

                    b.Property<string>("id_apt")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<bool>("is_point_issue")
                        .HasColumnType("bit");

                    b.Property<bool>("is_shipment")
                        .HasColumnType("bit");

                    b.Property<double?>("latitude")
                        .HasColumnType("float");

                    b.Property<double?>("longitude")
                        .HasColumnType("float");

                    b.Property<string>("metro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("schedule")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Apteks");
                });

            modelBuilder.Entity("AppDataWorker.Models.Operating_mode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AptekaId")
                        .HasColumnType("int");

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AptekaId");

                    b.ToTable("operating_Modes");
                });

            modelBuilder.Entity("AppDataWorker.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("barcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("group_id")
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<string>("mnn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("recept")
                        .HasColumnType("int");

                    b.Property<string>("release_form")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AppDataWorker.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AppDataWorker.Models.Analog", b =>
                {
                    b.HasOne("AppDataWorker.Models.Product", null)
                        .WithMany("analog")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppDataWorker.Models.Operating_mode", b =>
                {
                    b.HasOne("AppDataWorker.Models.Apteka", null)
                        .WithMany("operating_mode")
                        .HasForeignKey("AptekaId");
                });

            modelBuilder.Entity("AppDataWorker.Models.Apteka", b =>
                {
                    b.Navigation("operating_mode");
                });

            modelBuilder.Entity("AppDataWorker.Models.Product", b =>
                {
                    b.Navigation("analog");
                });
#pragma warning restore 612, 618
        }
    }
}
