using Logistics.Domain.Entities;
using Logistics.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.DTOs.ShipmentStatusHistoryDTOs
{
    public class ShipmentStatusHistoryDto
    {
        public ShipmentStatus Status { get; set; }
        public DateTime StatusDate { get; set; }

        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; } = null!;
    }
}
