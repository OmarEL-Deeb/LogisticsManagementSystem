using System.ComponentModel.DataAnnotations;

namespace Logistics.Domain.Entities
{

    public class EmployeeRole {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}