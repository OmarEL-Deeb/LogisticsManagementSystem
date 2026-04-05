using Logistics.Application.DTOs.CityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Interfaces.IServices
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetAllAsync();
        Task<CityDto?> GetByIdAsync(int id);    
        Task<CityDto> CreateAsync(CreateCityDto dto);
        Task UpdateAsync(int id, CreateCityDto dto);
        Task DeleteAsync(int id);
    }
}
