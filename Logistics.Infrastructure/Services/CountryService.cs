using AutoMapper;
using Logistics.Application.DTOs.CountriesDTOs;
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
    public class CountryService : ICountryService
    { 
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CountryDto> CreateAsync(CreateCountryDto dto)
        {
            var existing = await _uow.Countries.FindAsync(c => c.Name == dto.Name);
            if (existing.Any()) throw new Exception("Country name already exists.");

            var country = _mapper.Map<Country>(dto);
            await _uow.Countries.AddAsync(country);
            await _uow.CompleteAsync();
            return _mapper.Map<CountryDto>(country);
        }

        public  async Task DeleteAsync(int id)
        {
            var country = await _uow.Countries.GetByIdAsync(id)?? throw new Exception("Not found");
            _uow.Countries.Delete(country);
           await _uow.CompleteAsync();
         }

        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {

           return _mapper.Map<IEnumerable<CountryDto>>(await _uow.Countries.GetAllAsync());
        }

        public async Task<CountryDto?> GetByIdAsync(int id)
        {
            var country = await _uow.Countries.GetByIdAsync(id) ?? throw new Exception("Not found");

            return _mapper.Map<CountryDto>(await _uow.Countries.GetByIdAsync(id));
        }

        public async Task UpdateAsync(int id, CreateCountryDto dto)
        {
            var country = await _uow.Countries.GetByIdAsync(id) ?? throw new Exception("Not found");
            _mapper.Map(dto, country);
            _uow.Countries.Update(country);
            await _uow.CompleteAsync();
        }
    }
}
