namespace Logistics.Domain.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public double Capacity { get; set; }
        public bool IsActive { get; set; } = true;

<<<<<<< HEAD
=======
        // Foreign Key
>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6
        public int? AssignedDriverId { get; set; }
        public Driver? AssignedDriver { get; set; }
    }
}