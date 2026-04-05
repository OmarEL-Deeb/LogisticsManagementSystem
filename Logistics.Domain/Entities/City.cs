namespace Logistics.Domain.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Foreign Key
        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;
    }
}
