﻿// <auto-generated />
using System;
using ADO_hw.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ADO_hw.Migrations
{
    [DbContext(typeof(Data.Data))]
    [Migration("20241214204621_InverseNav")]
    partial class InverseNav
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ADO_hw.Data.Entity.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeleteDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ADO_hw.Data.Entity.Manager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("IdChief")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdMainDep")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdSecDep")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassDk")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SecDepId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Secname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdChief");

                    b.HasIndex("IdMainDep");

                    b.HasIndex("IdSecDep");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("ManagerId");

                    b.HasIndex("SecDepId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("ADO_hw.Data.Entity.Manager", b =>
                {
                    b.HasOne("ADO_hw.Data.Entity.Manager", "Chef")
                        .WithMany()
                        .HasForeignKey("IdChief");

                    b.HasOne("ADO_hw.Data.Entity.Department", "MainDep")
                        .WithMany()
                        .HasForeignKey("IdMainDep")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ADO_hw.Data.Entity.Department", null)
                        .WithMany("SecManagers")
                        .HasForeignKey("IdSecDep");

                    b.HasOne("ADO_hw.Data.Entity.Manager", null)
                        .WithMany("Workers")
                        .HasForeignKey("ManagerId");

                    b.HasOne("ADO_hw.Data.Entity.Department", "SecDep")
                        .WithMany("MainManagers")
                        .HasForeignKey("SecDepId");

                    b.Navigation("Chef");

                    b.Navigation("MainDep");

                    b.Navigation("SecDep");
                });

            modelBuilder.Entity("ADO_hw.Data.Entity.Department", b =>
                {
                    b.Navigation("MainManagers");

                    b.Navigation("SecManagers");
                });

            modelBuilder.Entity("ADO_hw.Data.Entity.Manager", b =>
                {
                    b.Navigation("Workers");
                });
#pragma warning restore 612, 618
        }
    }
}
