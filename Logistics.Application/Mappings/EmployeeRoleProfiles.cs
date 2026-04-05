using AutoMapper;
using Logistics.Application.DTOs.EmployeeRoleDTO;
using Logistics.Application.DTOs.EmployeeRoleDTOs;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logistics.Application.Mappings
{
    public class EmployeeRoleProfiles : Profile
     {
        public EmployeeRoleProfiles()
        {
            CreateMap<EmployeeRole, EmployeeRoleDto>().ReverseMap();
           CreateMap<EmployeeRole, CreateEmployeeRoleDto>().ReverseMap();
        }
    
    }
}
