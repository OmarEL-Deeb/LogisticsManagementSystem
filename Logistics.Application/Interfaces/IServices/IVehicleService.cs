using Logistics.Application.DTOs.VehicleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Interfaces.IServices
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> GetAllAsync();
        Task<VehicleDto?> GetByIdAsync(int id);
        Task<VehicleDto> CreateAsync(CreateVehicleDto dto);
        Task UpdateAsync(int id, CreateVehicleDto dto);
        Task DeleteAsync(int id);
        Task AssignDriverAsync(int vehicleId, int driverId);
    }
}
