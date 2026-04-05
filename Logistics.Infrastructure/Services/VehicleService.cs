using AutoMapper;
using Logistics.Application.DTOs.VehicleDTOs;
using Logistics.Application.Interfaces;
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
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;  
        }

       public async Task AssignDriverAsync(int vehicleId, int driverId)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(driverId) ?? throw new Exception("Driver not found.");
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(vehicleId) ?? throw new Exception("Vehicle not found.");
            var checkDriverAssigned = await _unitOfWork.Vehicles.FindAsync(v => v.AssignedDriverId == driverId && v.VehicleId != vehicleId);
            if (checkDriverAssigned.Any()) throw new Exception("Driver is already assigned to another vehicle.");

            vehicle.AssignedDriverId = driverId;
            _unitOfWork.Vehicles.Update(vehicle);
            await _unitOfWork.CompleteAsync();
        }

     public async Task<VehicleDto> CreateAsync(CreateVehicleDto dto)
        {
            var existing = await _unitOfWork.Vehicles.FindAsync(v => v.PlateNumber == dto.PlateNumber);
            if (existing.Any()) throw new Exception("Plate number already exists.");

            var vehicle = _mapper.Map<Vehicle>(dto);
            vehicle.IsActive = true;
            await _unitOfWork.Vehicles.AddAsync(vehicle);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<VehicleDto>(vehicle);
        }

      public async Task DeleteAsync(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id) ?? throw new Exception("Vehicle not found.");
            _unitOfWork.Vehicles.Delete(vehicle);
            await _unitOfWork.CompleteAsync();
        }

      public async Task<IEnumerable<VehicleDto>> GetAllAsync()
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<VehicleDto?> GetByIdAsync(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            return vehicle == null ? null : _mapper.Map<VehicleDto>(vehicle);
        }

        public Task UpdateAsync(int id, CreateVehicleDto dto)
        {
            var vehicle = _unitOfWork.Vehicles.GetByIdAsync(id).Result ?? throw new Exception("Vehicle not found.");
            _unitOfWork.Vehicles.Update(vehicle);
            return _unitOfWork.CompleteAsync();
        }
    }
}
