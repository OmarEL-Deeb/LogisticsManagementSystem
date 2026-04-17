namespace Logistics.Domain.Entities
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; } = true;

<<<<<<< HEAD
=======
        // Navigation Property (One-to-One with Vehicle)
>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6
        public Vehicle? Vehicle { get; set; }
    }
}