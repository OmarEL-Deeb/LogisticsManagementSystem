using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Application.DTOs.CountriesDTOs;

namespace Logistics.Application.Interfaces.IServices
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetAllAsync();
        Task<CountryDto?> GetByIdAsync(int id);
        Task<CountryDto> CreateAsync(CreateCountryDto dto);
        Task UpdateAsync(int id, CreateCountryDto dto);
        Task DeleteAsync(int id);
    }
}
