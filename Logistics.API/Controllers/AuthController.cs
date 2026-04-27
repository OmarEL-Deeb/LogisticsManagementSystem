using Logistics.Application.DTOs;
using Logistics.Application.DTOs.EmployeeDTOs; 
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices; 
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

       
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                 if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

            
                var authResponse = await _authService.LoginAsync(loginDto);

               
                return Ok(authResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
        
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login.", error = ex.Message });
            }
        }
    }
}
