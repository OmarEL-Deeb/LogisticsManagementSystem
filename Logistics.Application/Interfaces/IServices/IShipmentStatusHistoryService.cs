using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Application.DTOs.ShipmentStatusHistoryDTOs;

namespace Logistics.Application.Interfaces.IServices
{
    public interface IShipmentStatusHistoryService
    {
        Task<IEnumerable<ShipmentStatusHistoryDto>> GetHistoryByShipmentIdAsync(int shipmentId);
    }
}
