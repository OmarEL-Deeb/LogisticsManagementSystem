using Logistics.Application.DTOs.EmployeeDTOs;
using Logistics.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public EmployeesController(IEmployeeService service) => _service = service;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
        [HttpPost] public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto) { var res = await _service.CreateAsync(dto); return CreatedAtAction(nameof(GetById), new { id = res.EmployeeId }, res); }
        [HttpPut("{id}")] public async Task<IActionResult> Update(int id, [FromBody] CreateEmployeeDto dto) { await _service.UpdateAsync(id, dto); return NoContent(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return NoContent(); }
    }
}
