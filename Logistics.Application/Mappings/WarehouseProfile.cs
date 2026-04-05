using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Domain.Entities;
using Logistics.Application.DTOs.WarehouseDTOs;
using AutoMapper;

namespace Logistics.Application.Mappings
{
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
            CreateMap<Warehouse, WarehouseDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
            CreateMap<CreateWarehouseDto, Warehouse>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        }
    }
}
