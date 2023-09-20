﻿// <auto-generated />
using System;
using CrudMVC7.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CrudMVC7.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230827160727_Add ImmuizationPatient and relations")]
    partial class AddImmuizationPatientandrelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContactImmunization", b =>
                {
                    b.Property<int>("ContactsContactId")
                        .HasColumnType("int");

                    b.Property<int>("ImmunizationsImmunizationId")
                        .HasColumnType("int");

                    b.HasKey("ContactsContactId", "ImmunizationsImmunizationId");

                    b.HasIndex("ImmunizationsImmunizationId");

                    b.ToTable("ContactImmunization");
                });

            modelBuilder.Entity("CrudMVC7.Models.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactId"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("MotherLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("CrudMVC7.Models.Immunization", b =>
                {
                    b.Property<int>("ImmunizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImmunizationId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Period")
                        .HasColumnType("int");

                    b.HasKey("ImmunizationId");

                    b.ToTable("Immunizations");
                });

            modelBuilder.Entity("ContactImmunization", b =>
                {
                    b.HasOne("CrudMVC7.Models.Contact", null)
                        .WithMany()
                        .HasForeignKey("ContactsContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CrudMVC7.Models.Immunization", null)
                        .WithMany()
                        .HasForeignKey("ImmunizationsImmunizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
