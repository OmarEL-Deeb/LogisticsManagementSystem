using AutoMapper;
using Logistics.Application.DTOs.CountriesDTOs;
using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IGenericRepository<Country> _repository;
        private readonly IMapper _mapper;
        public CountriesController(IGenericRepository<Country> repository,IMapper mapper)
        {
            
            _repository = repository;
            _mapper = mapper;

        }
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            var countries = await _repository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<CountryDto>>(countries);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _repository.GetByIdAsync(id);

            if (country == null)
                return NotFound(new {message="Country not found"});

            var result = _mapper.Map<CountryDto>(country);

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> CreateCountry([FromBody]CreateCountryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var country = _mapper.Map<Country>(dto);

            await _repository.AddAsync(country);
            await _repository.SaveChangesAsync();

            var result = _mapper.Map<CountryDto>(country);

            return CreatedAtAction(nameof(GetCountry),
                new { id = country.CountryId },
                result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody]CreateCountryDto dto)
        {
            var country = await _repository.GetByIdAsync(id);

            if (country == null)
                return NotFound(new { message = "Country not found" });

            _mapper.Map(dto, country);

            _repository.Update(country);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _repository.GetByIdAsync(id);

            if (country == null)
                return NotFound(new { message = "Country not found" });

            _repository.Delete(country);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
