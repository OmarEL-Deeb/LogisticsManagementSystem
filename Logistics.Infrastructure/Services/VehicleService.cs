using AutoMapper;
using FluentValidation; // 1. Added Validator
using Logistics.Application.DTOs.VehicleDTOs;
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Logistics.Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleService> _logger;
        private readonly IValidator<RequireVehicleDto> _validator; 

        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<VehicleService> logger, IValidator<RequireVehicleDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task AssignDriverAsync(int vehicleId, int driverId)
        {
            var driver = await _unitOfWork.Drivers.GetAsync(d => d.DriverId == driverId, disableTracking: true)
                         ?? throw new Exception("Driver not found.");

            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(vehicleId)
                          ?? throw new Exception("Vehicle not found.");

            var checkDriverAssigned = await _unitOfWork.Vehicles.FindAsync(v => v.AssignedDriverId == driverId && v.VehicleId != vehicleId);
            if (checkDriverAssigned.Any())
                throw new Exception("Driver is already assigned to another vehicle.");

            vehicle.AssignedDriverId = driverId;
            _unitOfWork.Vehicles.Update(vehicle);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Driver {DriverId} was successfully assigned to Vehicle {VehicleId}.", driverId, vehicleId);
        }

        public async Task<VehicleDto> CreateAsync(RequireVehicleDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var existing = await _unitOfWork.Vehicles.FindAsync(v => v.PlateNumber == dto.PlateNumber);
            if (existing.Any())
                throw new Exception("Plate number already exists.");

            var vehicle = _mapper.Map<Vehicle>(dto);
            vehicle.IsActive = true;

            await _unitOfWork.Vehicles.AddAsync(vehicle);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task DeactivateAsync(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id) ?? throw new Exception("Vehicle not found.");

            vehicle.IsActive = false;
            _unitOfWork.Vehicles.Update(vehicle);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync(disableTracking: true);
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<VehicleDto?> GetByIdAsync(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetAsync(v => v.VehicleId == id, disableTracking: true)
                          ?? throw new Exception("Vehicle not found.");

            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task UpdateAsync(int id, RequireVehicleDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id) ?? throw new Exception("Vehicle not found.");

            var existing = await _unitOfWork.Vehicles.FindAsync(v => v.PlateNumber == dto.PlateNumber && v.VehicleId != id);
            if (existing.Any())
                throw new Exception("Plate number already assigned to another vehicle.");

            _mapper.Map(dto, vehicle);

            _unitOfWork.Vehicles.Update(vehicle);
            await _unitOfWork.CompleteAsync(); 
        }
    }
}