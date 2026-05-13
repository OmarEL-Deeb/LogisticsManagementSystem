using AutoMapper;
using FluentValidation;
using Logistics.Application.DTOs.ShipmentDTOs;
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Entities;
using Logistics.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Logistics.Infrastructure.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ShipmentService> _logger;
        private readonly IValidator<RequireShipmentDto> _validator;

        public ShipmentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ShipmentService> logger, IValidator<RequireShipmentDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ShipmentDto> CreateShipmentAsync(RequireShipmentDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var vehicle = await _unitOfWork.Vehicles.GetAsync(v => v.VehicleId == dto.VehicleId, disableTracking: true)
                          ?? throw new Exception("Vehicle not found.");

            if (dto.Weight > vehicle.Capacity)
                throw new Exception("Shipment weight exceeds vehicle capacity.");

            var shipment = _mapper.Map<Shipment>(dto);
            shipment.Status = ShipmentStatus.Pending;
            shipment.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Shipments.AddAsync(shipment);

            await _unitOfWork.ShipmentStatusHistories.AddAsync(new ShipmentStatusHistory
            {
                Shipment = shipment,
                Status = ShipmentStatus.Pending,
                StatusDate = DateTime.UtcNow
            });

            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Shipment {ShipmentId} was created successfully for Customer {CustomerId}.", shipment.ShipmentId, shipment.CustomerId);

            return _mapper.Map<ShipmentDto>(shipment);
        }


        public async Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync()
        {
            var shipments = await _unitOfWork.Shipments.GetAllAsync(disableTracking: true);
            return _mapper.Map<IEnumerable<ShipmentDto>>(shipments);
        }

        public async Task<ShipmentDto?> GetShipmentByIdAsync(int id)
        {
            var shipment = await _unitOfWork.Shipments.GetAsync(s => s.ShipmentId == id, disableTracking: true)
                           ?? throw new Exception("Shipment not found.");

            return _mapper.Map<ShipmentDto>(shipment);
        }

        public async Task UpdateShipmentAsync(int id, RequireShipmentDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var shipment = await _unitOfWork.Shipments.GetByIdAsync(id) ?? throw new Exception("Shipment not found.");

            var vehicle = await _unitOfWork.Vehicles.GetAsync(v => v.VehicleId == dto.VehicleId, disableTracking: true)
                          ?? throw new Exception("Vehicle not found.");

            if (dto.Weight > vehicle.Capacity)
                throw new Exception("Updated shipment weight exceeds vehicle capacity.");

            _mapper.Map(dto, shipment);
            _unitOfWork.Shipments.Update(shipment);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Shipment {ShipmentId} was updated.", id);
        }

        public async Task UpdateShipmentStatusAsync(int id, string newStatus)
        {
            var shipment = await _unitOfWork.Shipments.GetByIdAsync(id) ?? throw new Exception("Shipment not found.");

            if (!Enum.TryParse<ShipmentStatus>(newStatus, true, out var status))
                throw new Exception("Invalid shipment status.");

            if (!IsValidStatusTransition(shipment.Status, status))
                throw new Exception($"Invalid shipment status transition from {shipment.Status} to {status}.");

            shipment.Status = status;

            await _unitOfWork.ShipmentStatusHistories.AddAsync(new ShipmentStatusHistory
            {
                ShipmentId = shipment.ShipmentId,
                Status = status,
                StatusDate = DateTime.UtcNow
            });

            _unitOfWork.Shipments.Update(shipment);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Shipment {ShipmentId} status updated to '{NewStatus}'.", id, newStatus);
        }

        private bool IsValidStatusTransition(ShipmentStatus currentStatus, ShipmentStatus newStatus)
        {
            return (currentStatus, newStatus) switch
            {
                (ShipmentStatus.Pending, ShipmentStatus.InTransit) => true,
                (ShipmentStatus.Pending, ShipmentStatus.Cancelled) => true,
                (ShipmentStatus.InTransit, ShipmentStatus.Delivered) => true,
                (ShipmentStatus.InTransit, ShipmentStatus.Cancelled) => true,
                _ => false 
            };
        }
    }
}