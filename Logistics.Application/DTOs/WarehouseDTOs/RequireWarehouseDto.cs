namespace Logistics.Application.DTOs.WarehouseDTOs
{
    public class RequireWarehouseDto
    {
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int CityId { get; set; }
    }
}