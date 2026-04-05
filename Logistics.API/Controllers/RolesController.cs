using Logistics.Application.DTOs.EmployeeRoleDTO;
using Logistics.Application.DTOs.EmployeeRoleDTOs;
using Logistics.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRoleController : ControllerBase
    {
        private readonly IEmployeeRoleService _service;
        public EmployeeRoleController(IEmployeeRoleService service) => _service = service;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        [HttpPost] public async Task<IActionResult> Create([FromBody] CreateEmployeeRoleDto dto) => Ok(await _service.CreateAsync(dto));
    }
}