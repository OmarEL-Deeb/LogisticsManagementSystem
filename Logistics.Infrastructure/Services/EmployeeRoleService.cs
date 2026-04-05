using AutoMapper;
using Logistics.Application.DTOs.EmployeeRoleDTO;
using Logistics.Application.DTOs.EmployeeRoleDTOs;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Interfaces;
using System;
using Logistics.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Services
{
    public class EmployeeRoleService : IEmployeeRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeRoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EmployeeRoleDto> CreateAsync(CreateEmployeeRoleDto dto)
        {
            var role = _mapper.Map<EmployeeRole>(dto);
            await _unitOfWork.EmployeeRoles.AddAsync(role);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<EmployeeRoleDto>(role);
        }

       

        public async Task<IEnumerable<EmployeeRoleDto>> GetAllAsync()
        {
            var roles = await _unitOfWork.EmployeeRoles.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeRoleDto>>(roles);
        }
    }
}
