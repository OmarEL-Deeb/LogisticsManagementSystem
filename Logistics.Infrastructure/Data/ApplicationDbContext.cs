using Microsoft.EntityFrameworkCore;
using Logistics.Domain.Entities;

namespace Logistics.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
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
                .ToTable(t => t.HasCheckConstraint("CK_Warehouse_Capacity", "Capacity > 0"));

            
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Phone)
                .IsUnique();

            modelBuilder.Entity<Customer>().HasQueryFilter(c => c.IsActive);

            modelBuilder.Entity<Driver>()
                .HasIndex(d => d.LicenseNumber)
                .IsUnique();

            modelBuilder.Entity<Driver>()
                .Property(d => d.Salary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Driver>().HasQueryFilter(d => d.IsActive);

            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.PlateNumber)
                .IsUnique();

            modelBuilder.Entity<Vehicle>()
                .ToTable(t => t.HasCheckConstraint("CK_Vehicle_Capacity", "Capacity > 0"));

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.AssignedDriver)
                .WithOne(d => d.Vehicle)
                .HasForeignKey<Vehicle>(v => v.AssignedDriverId)
                .IsRequired(false);

            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.AssignedDriverId)
                .IsUnique()
                .HasFilter("[AssignedDriverId] IS NOT NULL"); 

            modelBuilder.Entity<Vehicle>().HasQueryFilter(v => v.IsActive);

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

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Customer)
                .WithMany() 
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Shipment>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18,2)");

          
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<EmployeeRole>()
                .HasIndex(r => r.RoleName)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Warehouse)
                .WithMany()
                .HasForeignKey(e => e.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}