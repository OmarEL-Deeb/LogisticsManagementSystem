using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.DTOs.EmployeeDTOs
{
    public class CreateEmployeeDto
    {
        public string FullName { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public int WarehouseId { get; set; }
        public int RoleId { get; set; }
    }
}
