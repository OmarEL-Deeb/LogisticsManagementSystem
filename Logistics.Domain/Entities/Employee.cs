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
        public EmployeeRole Role { get; set; } = null!;
    }
}