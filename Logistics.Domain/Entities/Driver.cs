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

        // Navigation Property (One-to-One with Vehicle)
        public Vehicle? Vehicle { get; set; }
    }
}