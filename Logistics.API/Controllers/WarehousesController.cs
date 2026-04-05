using AutoMapper;
using Logistics.Application.DTOs.WarehouseDTOs;
using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Warehouse> _repository;
        public WarehousesController(IGenericRepository<Warehouse> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseDto>>> GetWarehouses()
        {
            var warehouses = await _repository.GetAllAsync();
            var warehouseDto = _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
            return Ok(warehouseDto);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<IEnumerable<WarehouseDto>>> GetWarehouse(int id)
        {
            var warehouse = await _repository.GetByIdAsync(id);
            if (warehouse == null) {
                return NotFound(new { message = "Warehouse Not Found " });
            }
            var warehouseDto = _mapper.Map<WarehouseDto>( warehouse);
            return Ok(warehouseDto);
        }

        [HttpPost]

        public async Task <ActionResult> CreateWarehouse([FromBody] CreateWarehouseDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Warehouse =_mapper.Map<Warehouse>(createDto);
            await _repository.AddAsync(Warehouse);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetWarehouse), new { id = Warehouse.WarehouseId }, createDto);
        }

        [HttpPut("{id}")]

        public async Task <ActionResult>UpdateWarehouse (int id, [FromBody] CreateWarehouseDto updateDto)
        {
            var existingWarehouse = await _repository.GetByIdAsync(id);
            if (existingWarehouse == null)
                return NotFound(new { message = "Warehouse Not Exist" });
            _mapper.Map(updateDto ,existingWarehouse);
            _repository.Update(existingWarehouse);
            await _repository.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task <ActionResult>DeleteWarehouse (int id)
        {
            var warehouse =  await _repository.GetByIdAsync (id);
            if (warehouse == null)
                return NotFound(new { message = "Warehouse Not Found" });
            _repository.Delete(warehouse);
           await _repository.SaveChangesAsync();

            return NoContent();


        }

    }
}
