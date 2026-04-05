using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Logistics.Application.DTOs.ShipmentStatusHistoryDTOs;
using Logistics.Domain.Entities;

namespace Logistics.Application.Mappings
{
    public class ShipmentStatusHistoryProfiles : Profile
    {
        public ShipmentStatusHistoryProfiles()
        {
            CreateMap<ShipmentStatusHistory, ShipmentStatusHistoryDto>().ReverseMap() ;
        }
    }
}
