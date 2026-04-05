using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.DTOs.EmployeeDTOs
{
    public class EmployeeDto
    {
         public int EmployeeId { get; set; }

        public string FullName { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public string RoleName { get; set; } = string.Empty;
         public string WarehouseName { get; set; } = string.Empty;
    }
}
