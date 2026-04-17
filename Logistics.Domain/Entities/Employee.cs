namespace Logistics.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }

        // Foreign Keys
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;

        public int RoleId { get; set; }
<<<<<<< HEAD
        public EmployeeRole RoleName { get; set; } = null!;
=======
        public EmployeeRole Role { get; set; } = null!;
>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6
    }
}