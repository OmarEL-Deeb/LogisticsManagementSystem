using AutoMapper;
using Logistics.Application.DTOs.WarehouseDTOs;
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _service;
        public WarehousesController(IWarehouseService service) => _service = service;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllWarehousesAsync());
        [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetWarehouseByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarehouseDto dto)
        {
            var result = await _service.CreateWarehouseAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.WarehouseId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateWarehouseDto dto) { await _service.UpdateWarehouseAsync(id, dto); return NoContent(); }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) { await _service.DeleteWarehouseAsync(id); return NoContent(); }
       
    }
}
