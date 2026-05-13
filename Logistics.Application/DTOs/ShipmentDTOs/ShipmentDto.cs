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
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public string StatusName { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public double? DeliveryDurationHours { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public int OriginWarehouseId { get; set; }
        public string OriginWarehouseName { get; set; } = string.Empty;

        public int DestinationWarehouseId { get; set; }
        public string DestinationWarehouseName { get; set; } = string.Empty;

        public int VehicleId { get; set; }
        public string VehiclePlateNumber { get; set; } = string.Empty;
       
      
    }
}
