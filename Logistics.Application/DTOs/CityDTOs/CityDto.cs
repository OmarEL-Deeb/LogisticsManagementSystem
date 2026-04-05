using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.DTOs.CityDTOs
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty; 
    }
}
