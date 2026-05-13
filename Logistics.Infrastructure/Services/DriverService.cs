using AutoMapper;
using Logistics.Application.DTOs.DriversDTOs;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Entities;
using Logistics.Application.Interfaces;
using FluentValidation;

namespace Logistics.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RequireDriverDto> _validator;

        public DriverService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RequireDriverDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<DriverDto> CreateAsync(RequireDriverDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var existingLicense = await _unitOfWork.Drivers.FindAsync(d => d.LicenseNumber == dto.LicenseNumber);
            if (existingLicense.Any())
                throw new Exception("A driver with this license number already exists.");

            var driverEntity = _mapper.Map<Driver>(dto);
            await _unitOfWork.Drivers.AddAsync(driverEntity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DriverDto>(driverEntity);
        }

        public async Task DeactivateAsync(int id)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(id) ?? throw new Exception("Driver not found");

            driver.IsActive = false; 
            _unitOfWork.Drivers.Update(driver);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<DriverDto>> GetAllAsync()
        {
            var drivers = await _unitOfWork.Drivers.GetAllAsync(disableTracking: true);
            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        }

        public async Task<DriverDto?> GetByIdAsync(int id)
        {
            var driver = await _unitOfWork.Drivers.GetAsync(d => d.DriverId == id, disableTracking: true)
                         ?? throw new Exception("Driver not found");

            return _mapper.Map<DriverDto>(driver);
        }

        public async Task UpdateAsync(int id, RequireDriverDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var driver = await _unitOfWork.Drivers.GetByIdAsync(id) ?? throw new Exception("Driver not found");

            var existingLicense = await _unitOfWork.Drivers.FindAsync(d => d.LicenseNumber == dto.LicenseNumber && d.DriverId != id);
            if (existingLicense.Any())
                throw new Exception("This license number is already assigned to another driver.");

            _mapper.Map(dto, driver);
            _unitOfWork.Drivers.Update(driver);
            await _unitOfWork.CompleteAsync();
        }
    }
}