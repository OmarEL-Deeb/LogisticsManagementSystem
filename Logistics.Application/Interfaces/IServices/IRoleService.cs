using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Application.DTOs.EmployeeRoleDTO;
using Logistics.Application.DTOs.EmployeeRoleDTOs;

namespace Logistics.Application.Interfaces.IServices
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();
        Task<RoleDto> GetByIdAsync(int id);
        Task<RoleDto> CreateAsync(RequireRoleDto dto);
    }
}
