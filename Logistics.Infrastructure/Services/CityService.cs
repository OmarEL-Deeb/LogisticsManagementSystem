using AutoMapper;
using FluentValidation;
using Logistics.Application.DTOs.CityDTOs;
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices; 
using Logistics.Domain.Entities;

namespace Logistics.Infrastructure.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RequireCityDto> _validator;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RequireCityDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CityDto> CreateAsync(RequireCityDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.Errors.First().ErrorMessage);
            }
            var countryExists = await _unitOfWork.Countries.FindAsync(c => c.CountryId == dto.CountryId);
            if (!countryExists.Any())
                throw new Exception("Country not found.");

            var existingCity = await _unitOfWork.Cities.FindAsync(c => c.Name == dto.Name && c.CountryId == dto.CountryId);
            if (existingCity.Any())
                throw new Exception("City already exists.");

            var city = _mapper.Map<City>(dto);
            await _unitOfWork.Cities.AddAsync(city);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CityDto>(city);
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _unitOfWork.Cities.GetAsync(c => c.CityId == id)
                       ?? throw new Exception("City not found.");

            _unitOfWork.Cities.Delete(city);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            var cities = await _unitOfWork.Cities.GetAllAsync(disableTracking: true, c => c.Country);
            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public async Task<CityDto?> GetByIdAsync(int id)
        {
            var city = await _unitOfWork.Cities.GetAsync(c => c.CityId == id, disableTracking: true, c => c.Country);

            if (city == null)
            {
                throw new Exception("City not found.");
            }

            return _mapper.Map<CityDto>(city);
        }

        public async Task UpdateAsync(int id, RequireCityDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.Errors.First().ErrorMessage);
            }

            var city = await _unitOfWork.Cities.GetByIdAsync(id)
                       ?? throw new Exception("City not found.");
            _mapper.Map(dto, city);
            _unitOfWork.Cities.Update(city);
            await _unitOfWork.CompleteAsync();
        }
    }
}