﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankApp.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250507175501_mig5")]
    partial class mig5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankApp.Server.Models.BaseTransfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("BaseAccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PayeeId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BaseAccountId");

                    b.HasIndex("PayeeId");

                    b.HasIndex("SenderId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("BankApp.Server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<int>("DefaultAccountId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BaseAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Iban")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1")
                        .IsUnique()
                        .HasFilter("[UserId1] IS NOT NULL");

                    b.ToTable("Accounts", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("CompanyAccount", b =>
                {
                    b.HasBaseType("BaseAccount");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("REGON")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CompanyAccounts", (string)null);
                });

            modelBuilder.Entity("BankApp.Server.Models.BaseTransfer", b =>
                {
                    b.HasOne("BaseAccount", null)
                        .WithMany("Transfers")
                        .HasForeignKey("BaseAccountId");

                    b.HasOne("BaseAccount", "Payee")
                        .WithMany()
                        .HasForeignKey("PayeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BaseAccount", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Payee");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("BaseAccount", b =>
                {
                    b.HasOne("BankApp.Server.Models.User", "User")
                        .WithMany("BaseAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankApp.Server.Models.User", null)
                        .WithOne("DefaulAccount")
                        .HasForeignKey("BaseAccount", "UserId1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CompanyAccount", b =>
                {
                    b.HasOne("BaseAccount", null)
                        .WithOne()
                        .HasForeignKey("CompanyAccount", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BankApp.Server.Models.User", b =>
                {
                    b.Navigation("BaseAccounts");

                    b.Navigation("DefaulAccount")
                        .IsRequired();
                });

            modelBuilder.Entity("BaseAccount", b =>
                {
                    b.Navigation("Transfers");
                });
#pragma warning restore 612, 618
        }
    }
}
