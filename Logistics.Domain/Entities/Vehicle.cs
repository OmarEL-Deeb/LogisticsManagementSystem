namespace Logistics.Domain.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public double Capacity { get; set; }
        public bool IsActive { get; set; } = true;

        public int? AssignedDriverId { get; set; }
        public Driver? AssignedDriver { get; set; }
    }
}