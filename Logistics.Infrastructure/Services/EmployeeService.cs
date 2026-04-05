using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Logistics.Application.DTOs.EmployeeDTOs;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Interfaces;

namespace Logistics.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = _mapper.Map<Domain.Entities.Employee>(dto);
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id) ?? throw new Exception("Employee not found.");
            _unitOfWork.Employees.Delete(employee);
            await _unitOfWork.CompleteAsync();  
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync(c => c.RoleName, e => e.Warehouse);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetAsync(c=>c.EmployeeId==id,c=>c.RoleName,e=>e.Warehouse) ?? throw new Exception("Employee not found.");

            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }

        public async Task UpdateAsync(int id, CreateEmployeeDto dto)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id) ?? throw new Exception("Employee not found.");
            _mapper.Map(dto, employee);
            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.CompleteAsync();
        }
    }
}
