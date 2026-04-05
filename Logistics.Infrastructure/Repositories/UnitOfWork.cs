using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces;
using Logistics.Infrastructure.Data;

namespace Logistics.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Warehouse> Warehouses { get; private set; }
        public IGenericRepository<Customer> Customers { get; private set; }
        public IGenericRepository<Driver> Drivers { get; private set; }
        public IGenericRepository<Vehicle> Vehicles { get; private set; }
        public IGenericRepository<Shipment> Shipments { get; private set; }
        public IGenericRepository<ShipmentStatusHistory> ShipmentStatusHistories { get; private set; }
        public IGenericRepository<Payment> Payments { get; private set; }
        public IGenericRepository<Employee> Employees { get; private set; }
        public IGenericRepository<EmployeeRole> EmployeeRoles { get; private set; }
        public IGenericRepository<City> Cities { get; private set; }
        public IGenericRepository<Country> Countries { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            
            Warehouses = new GenericRepository<Warehouse>(_context);
            Customers = new GenericRepository<Customer>(_context);
            Drivers = new GenericRepository<Driver>(_context);
            Vehicles = new GenericRepository<Vehicle>(_context);
            Shipments = new GenericRepository<Shipment>(_context);
            ShipmentStatusHistories = new GenericRepository<ShipmentStatusHistory>(_context);
            Payments = new GenericRepository<Payment>(_context);
            Employees = new GenericRepository<Employee>(_context);
            EmployeeRoles = new GenericRepository<EmployeeRole>(_context);
            Cities = new GenericRepository<City>(_context);
            Countries = new GenericRepository<Country>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}