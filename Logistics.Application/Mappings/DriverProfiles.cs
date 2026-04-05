using AutoMapper;
using Logistics.Application.DTOs.DriversDTOs;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Mappings
{
    public class DriverProfiles : Profile
    {
        public DriverProfiles()
        {
            CreateMap<Driver, DriverDto>();
            CreateMap<CreateDriverDto, Driver>();
        }
    }
}
