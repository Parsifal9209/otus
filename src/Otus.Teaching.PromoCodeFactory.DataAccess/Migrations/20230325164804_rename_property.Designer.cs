﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Otus.Teaching.PromoCodeFactory.DataAccess.Context;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Migrations
{
    [DbContext(typeof(PromoCodeDbContext))]
    [Migration("20230325164804_rename_property")]
    partial class rename_property
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AppliedPromocodesCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                            AppliedPromocodesCount = 5,
                            Email = "owner@somemail.ru",
                            FirstName = "Иван",
                            LastName = "Сергеев",
                            RoleId = new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02")
                        },
                        new
                        {
                            Id = new Guid("f766e2bf-340a-46ea-bff3-f1700b435895"),
                            AppliedPromocodesCount = 10,
                            Email = "andreev@somemail.ru",
                            FirstName = "Петр",
                            LastName = "Андреев",
                            RoleId = new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665")
                        });
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                            Description = "Администратор",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                            Description = "Партнерский менеджер",
                            Name = "PartnerManager"
                        });
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PromoCodeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PromoCodeId");

                    b.ToTable("customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                            Email = "ivan_sergeev@mail.ru",
                            FirstName = "Иван",
                            LastName = "Петров",
                            PromoCodeId = new Guid("ef7f299f-92d7-459f-896e-078ed53ef11c")
                        });
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PreferenceId")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId", "PreferenceId");

                    b.HasIndex("PreferenceId");

                    b.ToTable("customer_preference");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("preference");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                            Name = "Театр"
                        },
                        new
                        {
                            Id = new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                            Name = "Семья"
                        },
                        new
                        {
                            Id = new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                            Name = "Дети"
                        });
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PartnerManagerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PartnerName")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PreferenceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceInfo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PartnerManagerId")
                        .IsUnique();

                    b.HasIndex("PreferenceId");

                    b.ToTable("promoCode");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ef7f299f-92d7-459f-896e-078ed53ef11c"),
                            BeginDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Code = "Промокод на покупку билета в Театр",
                            EndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PartnerManagerId = new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                            PreferenceId = new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                            ServiceInfo = "test"
                        });
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", b =>
                {
                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", "PromoCode")
                        .WithMany("Customers")
                        .HasForeignKey("PromoCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PromoCode");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", "Customer")
                        .WithMany("CustomerPreferences")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", "Preference")
                        .WithMany("CustomerPreferences")
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Employee", "PartnerManager")
                        .WithOne("PromoCode")
                        .HasForeignKey("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", "PartnerManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", "Preference")
                        .WithMany("PromoCodes")
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PartnerManager");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.Navigation("PromoCode");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Role", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", b =>
                {
                    b.Navigation("CustomerPreferences");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", b =>
                {
                    b.Navigation("CustomerPreferences");

                    b.Navigation("PromoCodes");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
