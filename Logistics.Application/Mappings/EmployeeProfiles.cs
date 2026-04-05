using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Logistics.Application.DTOs.EmployeeDTOs;
using Logistics.Domain.Entities;
using Logistics.Domain.Entities;

namespace Logistics.Application.Mappings
{
    public class EmployeeProfiles : Profile
    {
        public EmployeeProfiles()
        {
            CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.WarehouseName,
                       opt => opt.MapFrom(src => src.Warehouse.Name))
            .ForMember(dest => dest.RoleName,
                       opt => opt.MapFrom(src => src.RoleName.RoleName)); 
        }
    }
}
