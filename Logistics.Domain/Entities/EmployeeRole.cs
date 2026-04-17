using System.ComponentModel.DataAnnotations;

namespace Logistics.Domain.Entities
{
<<<<<<< HEAD
    public class EmployeeRole {
        [Key]
        public int RoleId { get; set; }
        public string RoleName 
        { get; set; } = string.Empty;
=======
    public class EmployeeRole
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}