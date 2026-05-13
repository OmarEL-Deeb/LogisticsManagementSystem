using AutoMapper;
using FluentValidation; 
using Logistics.Application.DTOs.EmployeeRoleDTO;
using Logistics.Application.DTOs.EmployeeRoleDTOs;
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Entities;

namespace Logistics.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RequireRoleDto> _validator; 

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RequireRoleDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<RoleDto> CreateAsync(RequireRoleDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var existingRole = await _unitOfWork.EmployeeRoles.FindAsync(r => r.RoleName == dto.RoleName);
            if (existingRole.Any())
                throw new Exception("This role already exists.");

            var role = _mapper.Map<EmployeeRole>(dto);

            await _unitOfWork.EmployeeRoles.AddAsync(role);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles = await _unitOfWork.EmployeeRoles.GetAllAsync(disableTracking: true);
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetByIdAsync(int id)
        {
            var role = await _unitOfWork.EmployeeRoles.GetAsync(r => r.RoleId == id, disableTracking: true)
                       ?? throw new Exception("Role not found");

            return _mapper.Map<RoleDto>(role);
        }
    }
}