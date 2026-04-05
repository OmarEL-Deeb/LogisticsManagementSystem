using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Logistics.Application.DTOs.CustomersDTOs;
using Logistics.Domain.Entities;

namespace Logistics.Application.Mappings
{
    public class CustomerProfiles : Profile
    {
        public CustomerProfiles() { 
            CreateMap<Customer, CustomerDto>();
             CreateMap<CreateCustomerDto, Customer>();
        }
        
    }
}
