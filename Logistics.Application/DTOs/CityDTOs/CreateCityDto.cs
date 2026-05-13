using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.DTOs.CityDTOs
{
    public class RequireCityDto
    {
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
    }
}
