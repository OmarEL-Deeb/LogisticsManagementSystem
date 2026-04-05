using AutoMapper;
using Logistics.Application.DTOs.ShipmentDTOs;
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Entities;
using Logistics.Domain.Enums;
using Logistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ShipmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShipmentDto> CreateShipmentAsync(CreateShipmentDto dto)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(dto.VehicleId) ?? throw new Exception("Vehicle not found.");

            // Business Rule: Shipment weight must not exceed vehicle capacity.
            if (dto.Weight > vehicle.Capacity) throw new Exception("Shipment weight exceeds vehicle capacity.");

            // Business Rule: Warehouses cannot exceed their capacity (Validation check)
            // Note: A full implementation would sum current active shipments in warehouse, but here's the concept.

            var shipment = _mapper.Map<Shipment>(dto);
            shipment.Status = ShipmentStatus.Pending;
            shipment.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Shipments.AddAsync(shipment);

            // Business Rule: Shipment status changes must be recorded
            await _unitOfWork.ShipmentStatusHistories.AddAsync(new ShipmentStatusHistory
            {
                Shipment = shipment,
                Status = ShipmentStatus.Pending,
                StatusDate = DateTime.UtcNow
            });

            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ShipmentDto>(shipment);
        }

        public async Task DeleteShipmentAsync(int id)
        {
            var shipment = await _unitOfWork.Shipments.GetByIdAsync(id) ?? throw new Exception("Shipment not found.");
            _unitOfWork.Shipments.Delete(shipment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync()
        {
            var shipments = await _unitOfWork.Shipments.GetAllAsync();
            return _mapper.Map<IEnumerable<ShipmentDto>>(shipments);
        }

        public async Task<ShipmentDto?> GetShipmentByIdAsync(int id)
        {
            var shipment = await _unitOfWork.Shipments.GetByIdAsync(id);
            return shipment == null ? null : _mapper.Map<ShipmentDto>(shipment);
        }

      

        public async Task UpdateShipmentAsync(int id, CreateShipmentDto dto)
        {
            var shipment = await _unitOfWork.Shipments.GetByIdAsync(id) ?? throw new Exception("Shipment not found.");
            var shipmentToUpdate = _mapper.Map(dto, shipment);
            _unitOfWork.Shipments.Update(shipmentToUpdate);
              await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateShipmentStatusAsync(int id, string newStatus)
        {
            var shipment = await _unitOfWork.Shipments.GetByIdAsync(id) ?? throw new Exception("Shipment not found.");
            if (!Enum.TryParse<ShipmentStatus>(newStatus, true, out var status)) throw new Exception("Invalid shipment status.");

            // Business Rule: Shipment status transitions must follow a defined workflow (e.g., Pending -> InTransit -> Delivered)

            if (!IsValidStatusTransition(shipment.Status, status)) throw new Exception("Invalid shipment status transition.");
            shipment.Status = status;
            // Business Rule: Shipment status changes must be recorded
            await _unitOfWork.ShipmentStatusHistories.AddAsync(new ShipmentStatusHistory
            {
                Shipment = shipment,
                Status = status,
                StatusDate = DateTime.UtcNow
            });
            _unitOfWork.Shipments.Update(shipment);
            await _unitOfWork.CompleteAsync();
        }

      

        private bool IsValidStatusTransition(ShipmentStatus currentStatus, ShipmentStatus newStatus)
        {
            return (currentStatus, newStatus) switch
            {
                // Allow moving from Pending to InTransit or Cancelled
                (ShipmentStatus.Pending, ShipmentStatus.InTransit) => true,
                (ShipmentStatus.Pending, ShipmentStatus.Cancelled) => true,

                // Allow moving from InTransit to Delivered or Cancelled
                (ShipmentStatus.InTransit, ShipmentStatus.Delivered) => true,
                (ShipmentStatus.InTransit, ShipmentStatus.Cancelled) => true,

                // No transitions allowed once Delivered or Cancelled
                _ => false
            };
        }
    }
}
