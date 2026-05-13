using Logistics.Application.DTOs; 
using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
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

            var employee = await _unitOfWork.Employees.GetAsync(
                e => e.Email == loginDto.Email,
                disableTracking: true,
                e => e.Role
            );


            if (employee == null || !VerifyPassword(loginDto.Password, employee.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

           
            var tokenString = GenerateJwtToken(employee);

           
            return new AuthResponseDto
            {
                Token = tokenString,
                FullName = employee.FullName,
                Role = employee.Role.RoleName
            };
        }

        
        private string GenerateJwtToken(Employee employee)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
            new Claim(ClaimTypes.Name, employee.FullName),
            new Claim(ClaimTypes.Role, employee.Role.RoleName),
            new Claim("WarehouseId", employee.WarehouseId.ToString())
            };

         
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      
            var durationInMinutes = Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"]);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationInMinutes),
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
