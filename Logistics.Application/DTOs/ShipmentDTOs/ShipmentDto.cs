using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.DTOs.ShipmentDTOs
{
    public class ShipmentDto
    {
        public int ShipmentId { get; set; }
        public int CustomerId { get; set; }
        public int OriginWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public int VehicleId { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public double? DeliveryDurationHours { get; set; }
    }
}
