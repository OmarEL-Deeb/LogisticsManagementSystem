using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Application.DTOs.EmployeeRoleDTO;
using Logistics.Application.DTOs.EmployeeRoleDTOs;

namespace Logistics.Application.Interfaces.IServices
{
    public interface IEmployeeRoleService
    {
        Task<IEnumerable<EmployeeRoleDto>> GetAllAsync();
        Task<EmployeeRoleDto> CreateAsync(CreateEmployeeRoleDto dto);
    }
}
