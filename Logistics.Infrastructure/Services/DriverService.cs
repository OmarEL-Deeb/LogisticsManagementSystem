using AutoMapper;
using Logistics.Application.DTOs.DriversDTOs;
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
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DriverService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       public async Task<DriverDto> CreateAsync(CreateDriverDto dto)
        {
         var driverEntity = _mapper.Map<Driver>(dto);
            await _unitOfWork.Drivers.AddAsync(driverEntity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<DriverDto>(driverEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(id) ?? throw new Exception("Driver not found");
            _unitOfWork.Drivers.Delete(driver);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<DriverDto>> GetAllAsync()
        {
            var drivers = await _unitOfWork.Drivers.GetAllAsync();
            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        }

        public async Task<DriverDto?> GetByIdAsync(int id)
        {
            var Exists = await _unitOfWork.Drivers.GetByIdAsync(id) ?? throw new Exception("Driver not found");

            var driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            return driver == null ? null : _mapper.Map<DriverDto>(driver);
        }

        public async Task UpdateAsync(int id, CreateDriverDto dto)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(id) ?? throw new Exception("Driver not found");
            _mapper.Map(dto, driver);
            _unitOfWork.Drivers.Update(driver);
            await _unitOfWork.CompleteAsync();
        }
    }
}

