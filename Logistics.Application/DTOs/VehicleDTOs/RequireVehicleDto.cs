using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.DTOs.VehicleDTOs
{
    public class RequireVehicleDto
    {
        public string PlateNumber { get; set; } = string.Empty;
        public double Capacity { get; set; }
        public int? AssignedDriverId { get; set; }


    }
}
