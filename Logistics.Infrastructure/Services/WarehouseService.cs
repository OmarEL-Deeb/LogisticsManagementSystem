using AutoMapper;
using Logistics.Application.DTOs.WarehouseDTOs;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Domain.Entities;

namespace Logistics.Infrastructure.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WarehouseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
       public async Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto dto)
        {
            var warehouse = _mapper.Map<Warehouse>(dto);
            warehouse.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Warehouses.AddAsync(warehouse);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<WarehouseDto>(warehouse);

        }

       public async Task DeleteWarehouseAsync(int id)
        {
            var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(id) ?? throw new Exception("Warehouse not found");
            _unitOfWork.Warehouses.Delete(warehouse);
            await _unitOfWork.CompleteAsync();

        }

        public async Task<IEnumerable<WarehouseDto>> GetAllWarehousesAsync()
        {
            var warehouses = await _unitOfWork.Warehouses.GetAllAsync(w => w.City);
            return _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
        }

        public async Task<WarehouseDto?> GetWarehouseByIdAsync(int id)
        {
            var warehouse = await _unitOfWork.Warehouses.GetAsync(c=>c.WarehouseId==id, c => c.City) ?? throw new Exception("Warehouse not found");
            return warehouse == null ? null : _mapper.Map<WarehouseDto>(warehouse);
        }

        public Task UpdateWarehouseAsync(int id, CreateWarehouseDto dto)
        {
            var warehouse = _unitOfWork.Warehouses.GetByIdAsync(id).Result ?? throw new Exception("Warehouse not found");
            _mapper.Map(dto, warehouse);
            _unitOfWork.Warehouses.Update(warehouse);
            return _unitOfWork.CompleteAsync();
        }
    }
}
