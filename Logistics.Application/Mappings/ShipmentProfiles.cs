using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Logistics.Application.DTOs.ShipmentDTOs;
using Logistics.Domain.Entities;

namespace Logistics.Application.Mappings
{
    public class ShipmentProfiles : Profile
    {
        public ShipmentProfiles()
        {
            CreateMap<Shipment, ShipmentDto>();
            CreateMap<CreateShipmentDto, Shipment>();
        }
    }
}
