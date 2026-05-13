using AutoMapper;
using FluentValidation; 
using Logistics.Application.DTOs.WarehouseDTOs;
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Entities;

namespace Logistics.Infrastructure.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RequireWarehouseDto> _validator;

        public WarehouseService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RequireWarehouseDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<WarehouseDto> CreateWarehouseAsync(RequireWarehouseDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var existingWarehouse = await _unitOfWork.Warehouses.FindAsync(w => w.Name == dto.Name && w.CityId == dto.CityId);
            if (existingWarehouse.Any())
                throw new Exception("A warehouse with this name already exists in the selected city.");

            var warehouse = _mapper.Map<Warehouse>(dto);
            warehouse.CreatedAt = DateTime.UtcNow;
            warehouse.IsActive = true; 

            await _unitOfWork.Warehouses.AddAsync(warehouse);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<WarehouseDto>(warehouse);
        }

        public async Task DeactivateWarehouseAsync(int id)
        {
            var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(id) ?? throw new Exception("Warehouse not found");

            warehouse.IsActive = false; 
            _unitOfWork.Warehouses.Update(warehouse);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<WarehouseDto>> GetAllWarehousesAsync()
        {
            var warehouses = await _unitOfWork.Warehouses.GetAllAsync(disableTracking: true, w => w.City);
            return _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
        }

        public async Task<WarehouseDto?> GetWarehouseByIdAsync(int id)
        {
            var warehouse = await _unitOfWork.Warehouses.GetAsync(c => c.WarehouseId == id, disableTracking: true, c => c.City)
                            ?? throw new Exception("Warehouse not found");

            return _mapper.Map<WarehouseDto>(warehouse);
        }

        public async Task UpdateWarehouseAsync(int id, RequireWarehouseDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(id) ?? throw new Exception("Warehouse not found");

            var existingWarehouse = await _unitOfWork.Warehouses.FindAsync(w => w.Name == dto.Name && w.CityId == dto.CityId && w.WarehouseId != id);
            if (existingWarehouse.Any())
                throw new Exception("Another warehouse with this name already exists in the selected city.");

            _mapper.Map(dto, warehouse);

            _unitOfWork.Warehouses.Update(warehouse);
            await _unitOfWork.CompleteAsync(); 
        }
    }
}