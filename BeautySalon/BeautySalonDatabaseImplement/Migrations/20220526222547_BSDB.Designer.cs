﻿// <auto-generated />
using System;
using BeautySalonDatabaseImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeautySalonDatabaseImplement.Migrations
{
    [DbContext(typeof(BeautySalonDatabase))]
    [Migration("20220526222547_BSDB")]
    partial class BSDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClientFIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Cosmetic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CosmeticName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Cosmetics");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.CosmeticService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CosmeticId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CosmeticId");

                    b.HasIndex("ServiceId");

                    b.ToTable("CosmeticServices");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManagerFIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Emplyees");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Estimate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateEstimate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EstimateRate")
                        .HasColumnType("int");

                    b.Property<int>("ProcedureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProcedureId");

                    b.ToTable("Estimates");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.LaborCost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CosmeticId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndLaborCost")
                        .HasColumnType("datetime2");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartLaborCost")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CosmeticId");

                    b.HasIndex("ManagerId");

                    b.ToTable("LaborCosts");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.OrderCosmetic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CosmeticId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CosmeticId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderCosmetics");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.OrderProcedure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProcedureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProcedureId");

                    b.ToTable("OrderProcedures");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Procedure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ProcedureName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProcedurePrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Procedures");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.ProcedureService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ProcedureId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProcedureId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ProcedureServices");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateAdding")
                        .HasColumnType("datetime2");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ServicePrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Cosmetic", b =>
                {
                    b.HasOne("BeautySalonDatabaseImplement.Models.Employee", "Manager")
                        .WithMany("Cosmetics")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.CosmeticService", b =>
                {
                    b.HasOne("BeautySalonDatabaseImplement.Models.Cosmetic", "Cosmetic")
                        .WithMany("CosmeticServices")
                        .HasForeignKey("CosmeticId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeautySalonDatabaseImplement.Models.Service", "Service")
                        .WithMany("CosmeticServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cosmetic");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Estimate", b =>
                {
                    b.HasOne("BeautySalonDatabaseImplement.Models.Client", "Client")
                        .WithMany("Estimates")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeautySalonDatabaseImplement.Models.Procedure", "Procedure")
                        .WithMany("Estimates")
                        .HasForeignKey("ProcedureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Procedure");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.LaborCost", b =>
                {
                    b.HasOne("BeautySalonDatabaseImplement.Models.Cosmetic", "Cosmetic")
                        .WithMany("LaborCosts")
                        .HasForeignKey("CosmeticId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeautySalonDatabaseImplement.Models.Employee", "Manager")
                        .WithMany("LaborCosts")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cosmetic");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Order", b =>
                {
                    b.HasOne("BeautySalonDatabaseImplement.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.OrderCosmetic", b =>
                {
                    b.HasOne("BeautySalonDatabaseImplement.Models.Cosmetic", "Cosmetic")
                        .WithMany("OrderCosmetics")
                        .HasForeignKey("CosmeticId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeautySalonDatabaseImplement.Models.Order", "Order")
                        .WithMany("OrderCosmetics")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cosmetic");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.OrderProcedure", b =>
                {
                    b.HasOne("BeautySalonDatabaseImplement.Models.Order", "Order")
                        .WithMany("OrderProcedures")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeautySalonDatabaseImplement.Models.Procedure", "Procedure")
                        .WithMany("ProcedureOrders")
                        .HasForeignKey("ProcedureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Procedure");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Procedure", b =>
                {
                    b.HasOne("BeautySalonDatabaseImplement.Models.Client", "Client")
                        .WithMany("Procedures")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.ProcedureService", b =>
                {
                    b.HasOne("BeautySalonDatabaseImplement.Models.Procedure", "Procedure")
                        .WithMany("ProcedureServices")
                        .HasForeignKey("ProcedureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeautySalonDatabaseImplement.Models.Service", "Service")
                        .WithMany("ProcedureServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Procedure");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Service", b =>
                {
                    b.HasOne("BeautySalonDatabaseImplement.Models.Employee", "Manager")
                        .WithMany("Services")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Client", b =>
                {
                    b.Navigation("Estimates");

                    b.Navigation("Orders");

                    b.Navigation("Procedures");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Cosmetic", b =>
                {
                    b.Navigation("CosmeticServices");

                    b.Navigation("LaborCosts");

                    b.Navigation("OrderCosmetics");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Employee", b =>
                {
                    b.Navigation("Cosmetics");

                    b.Navigation("LaborCosts");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Order", b =>
                {
                    b.Navigation("OrderCosmetics");

                    b.Navigation("OrderProcedures");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Procedure", b =>
                {
                    b.Navigation("Estimates");

                    b.Navigation("ProcedureOrders");

                    b.Navigation("ProcedureServices");
                });

            modelBuilder.Entity("BeautySalonDatabaseImplement.Models.Service", b =>
                {
                    b.Navigation("CosmeticServices");

                    b.Navigation("ProcedureServices");
                });
#pragma warning restore 612, 618
        }
    }
}
