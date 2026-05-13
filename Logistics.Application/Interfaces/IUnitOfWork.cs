using System;
using System.Threading.Tasks;
using Logistics.Domain.Entities;
namespace Logistics.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Warehouse> Warehouses { get; }
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Driver> Drivers { get; }
        IGenericRepository<Vehicle> Vehicles { get; }
        IGenericRepository<Shipment> Shipments { get; }

        IGenericRepository<ShipmentStatusHistory> ShipmentStatusHistories { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Employee> Employees { get; }
        IGenericRepository<EmployeeRole> EmployeeRoles { get; }
        IGenericRepository<City> Cities { get; }
        IGenericRepository<Country> Countries { get; }

        Task<int> CompleteAsync();
    }
}