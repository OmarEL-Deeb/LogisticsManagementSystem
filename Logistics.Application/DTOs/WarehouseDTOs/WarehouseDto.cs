namespace Logistics.Application.DTOs.WarehouseDTOs
{
    public class WarehouseDto
    {
        public int WarehouseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string CityName { get; set; } = string.Empty; 
    }
}