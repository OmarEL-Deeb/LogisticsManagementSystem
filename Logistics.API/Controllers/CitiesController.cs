using AutoMapper;
using Logistics.Application.DTOs.CityDTOs;
using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
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


    }
}
