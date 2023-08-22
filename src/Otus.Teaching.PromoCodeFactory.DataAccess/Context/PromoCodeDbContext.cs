using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Context
{
    public class PromoCodeDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Preference> Preferences { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerPreference> CustomerPreferences { get; set; }

        public string DbPath { get; }

        public PromoCodeDbContext(DbContextOptions<PromoCodeDbContext>
options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "promocode.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}").EnableSensitiveDataLogging();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Role
            builder.Entity<Role>()
                .ToTable("roles");

            builder.Entity<Role>()
               .HasKey(k => k.Id);
            builder.Entity<Role>()
                .Property(k => k.Id)
                .IsRequired();

            builder.Entity<Role>()
                .Property(k => k.Name)
                .HasMaxLength(15)
                .IsRequired();

            builder.Entity<Role>()
                .HasMany(k => k.Employees)
                .WithOne(k => k.Role);

            builder.Entity<Role>().HasData(FakeDataFactory.Roles);
            #endregion

            #region Employee
            builder.Entity<Employee>()
                .ToTable("employees");

            builder.Entity<Employee>()
               .HasKey(k => k.Id);
            builder.Entity<Employee>()
                .Property(k => k.Id)
                .IsRequired();

            builder.Entity<Employee>()
                .Property(k => k.FirstName)
                .IsRequired();

            builder.Entity<Employee>()
                .Property(k => k.LastName)
                .IsRequired();

            builder.Entity<Employee>()
                .HasOne(k => k.Role)
                .WithMany(k => k.Employees)
                .HasForeignKey(k=>k.RoleId);

            builder.Entity<Employee>()
                .HasMany(k => k.PromoCodes)
                .WithOne(k => k.PartnerManager)
                .HasForeignKey(k=>k.PartnerManagerId);

            builder.Entity<Employee>().HasData(FakeDataFactory.Employees.Select(x=> new Employee
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                AppliedPromocodesCount = x.AppliedPromocodesCount,
                RoleId = x.Role.Id
            }).ToList());
            #endregion

            #region Customer
            builder.Entity<Customer>()
                .ToTable("customers");

            builder.Entity<Customer>()
               .HasKey(k => k.Id);

            builder.Entity<Customer>()
                .Property(k => k.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Entity<Customer>()
                .Property(k => k.FirstName)
                .IsRequired();

            builder.Entity<Customer>()
                .Property(k => k.LastName)
                .IsRequired();

            builder.Entity<Customer>()
                .HasMany(k => k.PromoCodes)
                .WithOne(k => k.Customer)
                .HasForeignKey(k=>k.CustomerId);

            builder.Entity<Customer>()
                .HasMany(k => k.CustomerPreferences)
                .WithOne(k => k.Customer);

            builder.Entity<Customer>().HasData(FakeDataFactory.Customers);
            #endregion

            #region Preference
            builder.Entity<Preference>()
                .ToTable("preference");

            builder.Entity<Preference>()
               .HasKey(k => k.Id);
            builder.Entity<Preference>()
                .Property(k => k.Id)
                .IsRequired();

            builder.Entity<Preference>()
                .Property(k => k.Name)
                .HasMaxLength(15)
                .IsRequired();

            builder.Entity<Preference>()
                .HasMany(k => k.CustomerPreferences)
                .WithOne(k => k.Preference)
                .HasForeignKey(k=>k.PreferenceId);

            builder.Entity<Preference>()
                .HasMany(k => k.PromoCodes)
                .WithOne(k => k.Preference);

            builder.Entity<Preference>().HasData(FakeDataFactory.Preferences);
            #endregion

            #region PromoCode
            builder.Entity<PromoCode>()
                .ToTable("promoCode");

            builder.Entity<PromoCode>()
               .HasKey(k => k.Id);
            builder.Entity<PromoCode>()
                .Property(k => k.Id)
                .IsRequired();

            builder.Entity<PromoCode>()
                .Property(k => k.Code)
                .IsRequired();

            builder.Entity<PromoCode>()
                .Property(k => k.ServiceInfo)
                .IsRequired();

            builder.Entity<PromoCode>()
                .HasOne(k => k.PartnerManager)
                .WithMany(k => k.PromoCodes)
                .HasForeignKey(k=>k.PartnerManagerId);

            builder.Entity<PromoCode>()
                .HasOne(k => k.Preference)
                .WithMany(k => k.PromoCodes)
                .HasForeignKey(k=>k.PreferenceId);

            builder.Entity<PromoCode>()
                .HasOne(k => k.Customer)
                .WithMany(k => k.PromoCodes);

            builder.Entity<PromoCode>().HasData(FakeDataFactory.PromoCodes);
            #endregion

            #region CustomerPreference
            builder.Entity<CustomerPreference>()
                .ToTable("customer_preference");

            builder.Entity<CustomerPreference>()
               .HasKey(k => new { k.CustomerId, k.PreferenceId });
            builder.Entity<CustomerPreference>()
                .Property(k => k.CustomerId)
                .IsRequired();

            builder.Entity<CustomerPreference>()
                .Property(k => k.PreferenceId)
                .IsRequired();

            builder.Entity<CustomerPreference>()
                .HasOne(k => k.Customer)
                .WithMany(k => k.CustomerPreferences)
                .HasForeignKey(k=>k.CustomerId);

            builder.Entity<CustomerPreference>()
                .HasOne(k => k.Preference)
                .WithMany(k => k.CustomerPreferences)
                .HasForeignKey(k => k.PreferenceId);
            #endregion
        }
    }
}
