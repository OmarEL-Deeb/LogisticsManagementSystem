using AutoMapper;
using Logistics.Application.DTOs.EmployeeDTOs;
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {

            var employee = _mapper.Map<Domain.Entities.Employee>(dto);
            employee.PasswordHash = _authService.HashPassword(dto.Password);
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
            var employees = await _unitOfWork.Employees.GetAllAsync(c => c.Role, e => e.Warehouse);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetAsync(c=>c.EmployeeId==id,c=>c.Role,e=>e.Warehouse) ?? throw new Exception("Employee not found.");

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
