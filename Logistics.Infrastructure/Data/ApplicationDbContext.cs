using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Domain.Entities;

namespace Logistics.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentStatusHistory> ShipmentStatusHistories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<City>()
                .HasIndex(c => new { c.Name, c.CountryId })
                .IsUnique();
            
            modelBuilder.Entity<Warehouse>()
                .ToTable(t => t
                .HasCheckConstraint("CK_Warehouse_Capacity", "Capacity >0"));

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Phone)
                .IsUnique();

            modelBuilder.Entity<Vehicle>().
                HasIndex(v => v.PlateNumber)
                .IsUnique();

            modelBuilder.Entity<Vehicle>() 
                .ToTable(t => t
                .HasCheckConstraint("CK_Vehicle_Capacity", "Capacity > 0"));
           
            modelBuilder.Entity<Vehicle>()
               .HasOne(v => v.AssignedDriver)
               .WithOne(d => d.Vehicle)
               .HasForeignKey<Vehicle>(v => v.AssignedDriverId)
               .IsRequired(false);

            modelBuilder.Entity<Shipment>()
              .HasOne(s => s.OriginWarehouse)
              .WithMany()
              .HasForeignKey(s => s.OriginWarehouseId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.DestinationWarehouse)
                .WithMany()
                .HasForeignKey(s => s.DestinationWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

        }


    }
}
