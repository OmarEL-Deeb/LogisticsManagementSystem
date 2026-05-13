using AutoMapper;
using FluentValidation; 
using Logistics.Application.DTOs.CountriesDTOs;
using Logistics.Application.Interfaces.IServices;
using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;

namespace Logistics.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<RequireCountryDto> _validator; 

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RequireCountryDto> validator)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            var countries = await _uow.Countries.GetAllAsync(disableTracking: true);
            return _mapper.Map<IEnumerable<CountryDto>>(countries);
        }

        public async Task<CountryDto> CreateAsync(RequireCountryDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var existing = await _uow.Countries.FindAsync(c => c.Name == dto.Name);
            if (existing.Any())
                throw new Exception("Country name already exists.");

            var country = _mapper.Map<Country>(dto);
            await _uow.Countries.AddAsync(country);
            await _uow.CompleteAsync();

            return _mapper.Map<CountryDto>(country);
        }

        public async Task DeleteAsync(int id)
        {
            var country = await _uow.Countries.GetByIdAsync(id) ?? throw new Exception("Country not found");

            _uow.Countries.Delete(country);
            await _uow.CompleteAsync();
        }
        public async Task<CountryDto?> GetByIdAsync(int id)
        {
            var country = await _uow.Countries.GetAsync(c => c.CountryId == id, disableTracking: true);

            if (country == null)
                throw new Exception("Country not found");

            return _mapper.Map<CountryDto>(country);
        }

        public async Task UpdateAsync(int id, RequireCountryDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var country = await _uow.Countries.GetByIdAsync(id) ?? throw new Exception("Country not found");

            var existing = await _uow.Countries.FindAsync(c => c.Name == dto.Name && c.CountryId != id);
            if (existing.Any())
                throw new Exception("Another country with this name already exists.");

            _mapper.Map(dto, country);
            _uow.Countries.Update(country);
            await _uow.CompleteAsync();
        }
    }
}