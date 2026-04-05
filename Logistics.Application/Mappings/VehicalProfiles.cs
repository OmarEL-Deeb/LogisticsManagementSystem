using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Logistics.Application.DTOs.VehicleDTOs;
using Logistics.Domain.Entities;


namespace Logistics.Application.Mappings
{
    public class VehicalProfiles : Profile
    {
        public VehicalProfiles()
        {
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<CreateVehicleDto, Vehicle>();
        }
    }
}
