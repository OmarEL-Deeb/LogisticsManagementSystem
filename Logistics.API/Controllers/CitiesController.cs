using AutoMapper;
using Logistics.Application.DTOs.CityDTOs;
using Logistics.Application.Interfaces;
<<<<<<< HEAD
using Logistics.Application.Interfaces.IServices;
=======
>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6
using Logistics.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
<<<<<<< HEAD
        private readonly ICityService _service;
        public CitiesController(ICityService service) => _service = service;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCityDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.CityId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCityDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
=======
        private readonly IGenericRepository<City> _repository;
        private readonly IMapper _mapper;
        public CitiesController(IGenericRepository<City> repository, IMapper mapper)
        {

            _repository = repository;
            _mapper = mapper;

        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
            var cities = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCity(int id)
        {
            var city = await _repository.GetByIdAsync(id);
            if (city == null)
            {
                return NotFound(new { Message = "City not found" });
            }
            var result = _mapper.Map<CityDto>(city);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CityDto>> CreateCity([FromBody]CreateCityDto cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            await _repository.AddAsync(city);
            var result = _mapper.Map<CityDto>(city);
            return CreatedAtAction(nameof(GetCity), new { id = city.CityId}, result);
        }

        [HttpPut("{id}")]
       public async Task<IActionResult> UpdateCity(int id,[FromBody] CityDto cityDto)
        {             var city = await _repository.GetByIdAsync(id);
            if (city == null)
            {
                return NotFound(new { Message = "City not found" });
            }
            _mapper.Map(cityDto, city);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

>>>>>>> 369c4203daa3a057b22b26e20c6fcdfb71a585d6

    }
}
