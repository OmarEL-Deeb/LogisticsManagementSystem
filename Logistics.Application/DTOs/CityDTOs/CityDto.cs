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
<<<<<<< HEAD
=======
        public int CountryId { get; set; }
>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6
        public string CountryName { get; set; } = string.Empty; 
    }
}
