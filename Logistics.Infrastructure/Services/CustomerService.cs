using AutoMapper;
using Logistics.Application.DTOs.CustomersDTOs;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Domain.Entities;

namespace Logistics.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper   _mapper;
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
        {var customer = _mapper.Map<Customer>(dto);
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

        public async Task DeleteAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id) ?? throw new Exception("Customer not found");
            _unitOfWork.Customers.Delete(customer);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var Exists = await _unitOfWork.Customers.GetByIdAsync(id) ?? throw new Exception("Customer not found");

            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateAsync(int id, CreateCustomerDto dto)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id) ?? throw new Exception("Customer not found");
            _mapper.Map(dto, customer);
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.CompleteAsync();
        }
    }
}
