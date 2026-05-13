using AutoMapper;
using FluentValidation;
using Logistics.Application.DTOs.EmployeeDTOs;
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices;

namespace Logistics.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RequireEmployeeDto> _validator;
        private readonly IAuthService _authService;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RequireEmployeeDto> validator, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _authService = authService;
        }

        public async Task<EmployeeDto> CreateAsync(RequireEmployeeDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var existingEmail = await _unitOfWork.Employees.FindAsync(e => e.Email == dto.Email);
            if (existingEmail.Any())
                throw new Exception("This email is already registered to another employee.");

            var employee = _mapper.Map<Domain.Entities.Employee>(dto);

            employee.PasswordHash = _authService.HashPassword(dto.Password);

            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task DeactivateAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id) ?? throw new Exception("Employee not found.");

            employee.IsActive = false;
            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync(disableTracking: true, c => c.Role, e => e.Warehouse);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetAsync(c => c.EmployeeId == id, disableTracking: true, c => c.Role, e => e.Warehouse)
                           ?? throw new Exception("Employee not found.");

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task UpdateAsync(int id, RequireEmployeeDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var employee = await _unitOfWork.Employees.GetByIdAsync(id) ?? throw new Exception("Employee not found.");

            var existingEmail = await _unitOfWork.Employees.FindAsync(e => e.Email == dto.Email && e.EmployeeId != id);
            if (existingEmail.Any())
                throw new Exception("This email is already in use by another employee.");

            _mapper.Map(dto, employee);

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                employee.PasswordHash = _authService.HashPassword(dto.Password);
            }

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.CompleteAsync();
        }
    }
}