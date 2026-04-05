using AutoMapper;
using Logistics.Application.DTOs.CountriesDTOs;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Mappings
{
    public class CountriesProfiles : Profile
    {
        public CountriesProfiles()
        {

            CreateMap<Country, CountryDto>();
            CreateMap<CreateCountryDto, Country>();
        }
    }
}
