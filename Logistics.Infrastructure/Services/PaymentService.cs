using AutoMapper;
using Logistics.Application.DTOs.PaymentDTOs;
using Logistics.Application.Interfaces.IServices;
using Logistics.Domain.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PaymentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto dto)
        {
            var payment = _mapper.Map<Domain.Entities.Payment>(dto);
            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("Payment of {Amount} was successfully completed for Shipment {ShipmentId} using {Method}.",
            payment.Amount, payment.ShipmentId, payment.PaymentMethod);
            return _mapper.Map<PaymentDto>(payment);

        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _unitOfWork.Payments.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public  async Task<PaymentDto?> GetPaymentByIdAsync(int id)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null)
                throw new Exception("Payment not found");
            return _mapper.Map<PaymentDto?>(payment);
        }

        public async Task<PaymentDto> UpdatePaymentStatusAsync(int id, bool isPaid)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);
            if (payment == null)
                throw new Exception("Payment not found");
            payment.IsPaid = isPaid;
            payment.PaidDate = isPaid ? DateTime.UtcNow : (DateTime?)null;
            _unitOfWork.Payments.Update(payment);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<PaymentDto>(payment);
        }
    }
}
