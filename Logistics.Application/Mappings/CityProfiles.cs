using AutoMapper;
using Logistics.Application.DTOs.CityDTOs;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Application.Mappings
{
    public class CityProfiles : Profile
    {
         public CityProfiles()
        {
            CreateMap<City, CityDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
            CreateMap<CreateCityDto, City>();


        }
    }
}
