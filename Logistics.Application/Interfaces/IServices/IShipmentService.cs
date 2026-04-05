using Logistics.Application.DTOs.ShipmentDTOs;
using Logistics.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Interfaces.IServices
{
     public interface IShipmentService
        {
            Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync();
            Task<ShipmentDto?> GetShipmentByIdAsync(int id);
            Task<ShipmentDto> CreateShipmentAsync(CreateShipmentDto dto);
            Task UpdateShipmentStatusAsync(int id, string newStatus);
            Task DeleteShipmentAsync(int id);
           Task UpdateShipmentAsync(int id, CreateShipmentDto dto);
    }
    
}

