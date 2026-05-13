using AutoMapper;
using Logistics.Application.DTOs.CustomersDTOs;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Entities;
using Logistics.Application.Interfaces;
using FluentValidation;

namespace Logistics.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RequireCustomerDto> _validator;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RequireCustomerDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CustomerDto> CreateAsync(RequireCustomerDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var existingEmail = await _unitOfWork.Customers.FindAsync(c => c.Email == dto.Email);
            if (existingEmail.Any())
                throw new Exception("A customer with this email already exists.");

            var customer = _mapper.Map<Customer>(dto);
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task DeactivateAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id) ?? throw new Exception("Customer not found");

            customer.IsActive = false;
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.CompleteAsync();
        }

        

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync(disableTracking: true);
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetAsync(c => c.CustomerId == id, disableTracking: true)
                           ?? throw new Exception("Customer not found");

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateAsync(int id, RequireCustomerDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.Errors.First().ErrorMessage);

            var customer = await _unitOfWork.Customers.GetByIdAsync(id) ?? throw new Exception("Customer not found");

            var existingEmail = await _unitOfWork.Customers.FindAsync(c => c.Email == dto.Email && c.CustomerId != id);
            if (existingEmail.Any())
                throw new Exception("This email is already in use by another customer.");

            _mapper.Map(dto, customer);
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.CompleteAsync();
        }
    }
}