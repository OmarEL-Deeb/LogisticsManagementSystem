using System;
using System.Threading.Tasks;
using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Logistics.Infrastructure.Data;

namespace Logistics.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IGenericRepository<Warehouse>? _warehouses;
        private IGenericRepository<Customer>? _customers;
        private IGenericRepository<Driver>? _drivers;
        private IGenericRepository<Vehicle>? _vehicles;
        private IGenericRepository<Shipment>? _shipments;
        private IGenericRepository<ShipmentStatusHistory>? _shipmentStatusHistories;
        private IGenericRepository<Payment>? _payments;
        private IGenericRepository<Employee>? _employees;
        private IGenericRepository<EmployeeRole>? _employeeRoles;
        private IGenericRepository<City>? _cities;
        private IGenericRepository<Country>? _countries;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // 2. Public properties using Lazy Initialization (??=)
        // The repository is only instantiated when it is explicitly called by a service.
        public IGenericRepository<Warehouse> Warehouses =>
            _warehouses ??= new GenericRepository<Warehouse>(_context);

        public IGenericRepository<Customer> Customers =>
            _customers ??= new GenericRepository<Customer>(_context);

        public IGenericRepository<Driver> Drivers =>
            _drivers ??= new GenericRepository<Driver>(_context);

        public IGenericRepository<Vehicle> Vehicles =>
            _vehicles ??= new GenericRepository<Vehicle>(_context);

        public IGenericRepository<Shipment> Shipments =>
            _shipments ??= new GenericRepository<Shipment>(_context);

        public IGenericRepository<ShipmentStatusHistory> ShipmentStatusHistories =>
            _shipmentStatusHistories ??= new GenericRepository<ShipmentStatusHistory>(_context);

        public IGenericRepository<Payment> Payments =>
            _payments ??= new GenericRepository<Payment>(_context);

        public IGenericRepository<Employee> Employees =>
            _employees ??= new GenericRepository<Employee>(_context);

        public IGenericRepository<EmployeeRole> EmployeeRoles =>
            _employeeRoles ??= new GenericRepository<EmployeeRole>(_context);

        public IGenericRepository<City> Cities =>
            _cities ??= new GenericRepository<City>(_context);

        public IGenericRepository<Country> Countries =>
            _countries ??= new GenericRepository<Country>(_context);

        // 3. Commit the transaction to the database
        public async Task<int> CompleteAsync()
        {
            // Here, EF Core applies all changes in a single transaction
            return await _context.SaveChangesAsync();
        }

        // 4. Free up unmanaged resources
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this); // Good practice to prevent finalizer queue overhead
        }
    }
}