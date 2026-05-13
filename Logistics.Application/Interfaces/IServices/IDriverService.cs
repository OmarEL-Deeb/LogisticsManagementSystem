using Logistics.Application.DTOs.DriversDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Interfaces.IServices
{
    public interface IDriverService
    {
        Task<IEnumerable<DriverDto>> GetAllAsync();
        Task<DriverDto?> GetByIdAsync(int id);
        Task<DriverDto> CreateAsync(RequireDriverDto dto);
        Task UpdateAsync(int id, RequireDriverDto dto);
        Task DeactivateAsync(int id);
    }
}
