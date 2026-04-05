using Logistics.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.DTOs.ShipmentDTOs
{
    public class CreateShipmentDto
    {
        public int CustomerId { get; set; }
        public int OriginWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public int VehicleId { get; set; }
        public double Weight { get; set; }
        public ShipmentStatus Status { get; set; }
        public decimal Price { get; set; }
    }
}
