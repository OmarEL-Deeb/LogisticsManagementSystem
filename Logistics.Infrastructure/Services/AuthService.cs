using Logistics.Application.DTOs; 
using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;

namespace Logistics.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            // 1. جلب بيانات الموظف باستخدام الدالة المتوافقة مع الكود بتاعك
            var employee = await _unitOfWork.Employees.GetAsync(
                e => e.Email == loginDto.Email, // الـ Predicate
                e => e.Role                     // الـ Include
            );

            // 2. التحقق من صحة البيانات
            if (employee == null || !VerifyPassword(loginDto.Password, employee.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // 3. توليد التوكن 
            var tokenString = GenerateJwtToken(employee);

            // 4. إرجاع النتيجة
            return new AuthResponseDto
            {
                Token = tokenString,
                FullName = employee.FullName,
                Role = employee.Role.RoleName
            };
        }

        // ==========================================
        // الدوال المساعدة (Helper Methods)
        // ==========================================

        private string GenerateJwtToken(Employee employee)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
        new Claim(ClaimTypes.Name, employee.FullName),
        new Claim(ClaimTypes.Role, employee.Role.RoleName),
        new Claim("WarehouseId", employee.WarehouseId.ToString())
    };

            // 1. تعديل هنا: استخدمنا "Key" بدلاً من "Secret"
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 2. قراءة وقت الانتهاء من الإعدادات
            var durationInMinutes = Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"]);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(durationInMinutes), // 3. استخدام الوقت الديناميكي
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }
    }
}