using Logistics.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.DTOs.ShipmentStatusHistoryDTO
{
    public class RequireShipmentStatusHistoryDto
    {
        public ShipmentStatus Status { get; set; } 
        public int ShipmentId { get; set; }
    }
}
