using AutoMapper;
using Logistics.Application.DTOs.CityDTOs;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Services
{
    public class CityService: ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CityService(IUnitOfWork unitOfWork , IMapper imapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = imapper;

            
        }

       public async Task<CityDto> CreateAsync(CreateCityDto dto)
        {
            var countryExists = await _unitOfWork.Countries
             .FindAsync(c => c.CountryId == dto.CountryId);

            if (!countryExists.Any())
                throw new Exception("Country not found.");

            var existingCity =await _unitOfWork.Cities.FindAsync(c => c.Name == dto.Name && c.CountryId == dto.CountryId);
            if (existingCity.Any()) throw new Exception("City already exists.");

            var city = _mapper.Map<City>(dto);
            _unitOfWork.Cities.AddAsync(city);
            _unitOfWork.CompleteAsync();
            return _mapper.Map<CityDto>(city);
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _unitOfWork.Cities.GetAsync(c => c.CityId == id,c=>c.Country) ?? throw new Exception("Not found");
            _unitOfWork.Cities.Delete(city);
            await _unitOfWork.CompleteAsync();
        }

       public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            var cities = await _unitOfWork.Cities.GetAllAsync(c => c.Country);
            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public async Task<CityDto?> GetByIdAsync(int id)
        {
            var Exists = await _unitOfWork.Cities.FindAsync(c => c.CityId == id);
            if ( !Exists.Any())
            {
                throw new Exception("City NOT exists.");
            }
            var city = await _unitOfWork.Cities.GetAsync(c => c.CityId == id, c => c.Country);
            return city == null ? null : _mapper.Map<CityDto>(city);
        }

        public async Task UpdateAsync(int id, CreateCityDto dto)
        {
            var city = await _unitOfWork.Cities.GetByIdAsync(id) ?? throw new Exception("Not found");
            _mapper.Map(dto, city);
            _unitOfWork.Cities.Update(city);
            await _unitOfWork.CompleteAsync();
        }
    }
}
