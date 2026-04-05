using Logistics.Application.DTOs.CustomersDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Interfaces.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<CustomerDto> CreateAsync(CreateCustomerDto dto);
        Task UpdateAsync(int id, CreateCustomerDto dto);
        Task DeleteAsync(int id);
        Task DeactivateAsync(int id);
    }
}
