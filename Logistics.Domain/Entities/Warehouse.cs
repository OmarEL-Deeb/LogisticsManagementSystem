namespace Logistics.Domain.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign Key
        public int CityId { get; set; }
        public City City { get; set; } = null!;
    }
}