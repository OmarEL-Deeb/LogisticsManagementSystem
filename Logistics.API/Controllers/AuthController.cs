using Logistics.Application.DTOs;
using Logistics.Application.DTOs.EmployeeDTOs; // مسار الـ LoginDto
using Logistics.Application.Interfaces;
using Logistics.Application.Interfaces.IServices; // مسار الـ IAuthService
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

        // حقن خدمة الـ Auth
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                // التأكد من أن البيانات المرسلة ليست فارغة ومطابقة للشروط
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // استدعاء دالة تسجيل الدخول من الـ Service
                var authResponse = await _authService.LoginAsync(loginDto);

                // إرجاع النتيجة (التوكن + بيانات الموظف) بكود 200 OK
                return Ok(authResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                // التقاط الخطأ الخاص بالباسورد أو الإيميل غير الصحيح وإرجاع كود 401
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // التقاط أي خطأ آخر غير متوقع وإرجاع كود 500
                return StatusCode(500, new { message = "An error occurred during login.", error = ex.Message });
            }
        }
    }
}