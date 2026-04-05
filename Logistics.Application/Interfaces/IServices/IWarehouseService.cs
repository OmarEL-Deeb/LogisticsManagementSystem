using Logistics.Application.DTOs.WarehouseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Interfaces.IServices
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseDto>> GetAllWarehousesAsync();
        Task<WarehouseDto?> GetWarehouseByIdAsync(int id);
        Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto dto);
        Task UpdateWarehouseAsync(int id, CreateWarehouseDto dto);
        Task DeleteWarehouseAsync(int id);
    }
}
