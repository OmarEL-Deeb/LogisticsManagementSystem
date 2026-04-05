using AutoMapper;
using Logistics.Application.DTOs.PaymentDTOs;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }

        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto dto)
        {
            var payment = _mapper.Map<Domain.Entities.Payment>(dto);
            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<PaymentDto>(payment);

        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _unitOfWork.Payments.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public Task<PaymentDto?> GetPaymentByIdAsync(int id)
        {
            var payment = _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null)
                throw new Exception("Payment not found");
            return _mapper.Map<Task<PaymentDto?>>(payment);
        }

        public Task<PaymentDto> UpdatePaymentStatusAsync(int id, bool isPaid)
        {
            var payment = _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null)
                throw new Exception("Payment not found");
            payment.Result.IsPaid = isPaid;
            payment.Result.PaidDate = isPaid ? DateTime.UtcNow : (DateTime?)null;
            _unitOfWork.Payments.Update(payment.Result);
            _unitOfWork.CompleteAsync();
            return _mapper.Map<Task<PaymentDto>>(payment);
        }
    }
}
