using Logistics.Domain.Enums;
namespace Logistics.Domain.Entities
{
  

    public class Payment
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime? PaidDate { get; set; }

        // Foreign Key
        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; } = null!;
    }
}