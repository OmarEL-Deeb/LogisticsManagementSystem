using Logistics.Domain.Enums;
namespace Logistics.Domain.Entities
   
{
   
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

        // Computed Property
        public double? DeliveryDurationHours =>
            DeliveredAt.HasValue ? (DeliveredAt.Value - CreatedAt).TotalHours : null;

        // Foreign Keys
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int OriginWarehouseId { get; set; }
        public Warehouse OriginWarehouse { get; set; } = null!;

        public int DestinationWarehouseId { get; set; }
        public Warehouse DestinationWarehouse { get; set; } = null!;

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
    }
}