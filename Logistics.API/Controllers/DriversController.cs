using Logistics.Application.DTOs.DriversDTOs;
using Logistics.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _service;
        public DriversController(IDriverService service) => _service = service;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
        [HttpPost] public async Task<IActionResult> Create([FromBody] CreateDriverDto dto) { var res = await _service.CreateAsync(dto); return CreatedAtAction(nameof(GetById), new { id = res.DriverId }, res); }
        [HttpPut("{id}")] public async Task<IActionResult> Update(int id, [FromBody] CreateDriverDto dto) { await _service.UpdateAsync(id, dto); return NoContent(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return NoContent(); }
    }
}

