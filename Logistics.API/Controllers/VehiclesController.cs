using Logistics.Application.DTOs.VehicleDTOs;
using Logistics.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _service;
        public VehiclesController(IVehicleService service) => _service = service;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
        [HttpPost] public async Task<IActionResult> Create([FromBody] RequireVehicleDto dto) { var res = await _service.CreateAsync(dto); return CreatedAtAction(nameof(GetById), new { id = res.VehicleId }, res); }
        [HttpPut("{id}")] public async Task<IActionResult> Update(int id, [FromBody] RequireVehicleDto dto) { await _service.UpdateAsync(id, dto); return NoContent(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _service.DeactivateAsync(id); return NoContent(); }


        [HttpPost("{id}/assign-driver")]
        public async Task<IActionResult> AssignDriver(int id, [FromBody] AssignDriverDto request)
        {
            await _service.AssignDriverAsync(id, request.DriverId);
            return NoContent();
        }
    }
}
