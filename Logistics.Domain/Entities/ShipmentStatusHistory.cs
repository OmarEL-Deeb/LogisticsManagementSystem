using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities
{
    public class ShipmentStatusHistory
    {
        public int Id { get; set; }
        public ShipmentStatus Status { get; set; }
        public DateTime StatusDate { get; set; }

        // Foreign Key
        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; } = null!;
    }
}