using AutoMapper;
using FluentValidation;
using Logistics.Application.DTOs.PaymentDTOs;
using Logistics.Application.Interfaces.IServices;
using Logistics.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Logistics.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RequirePaymentDto> _validator; 
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PaymentService> logger, IValidator<RequirePaymentDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<PaymentDto> CreatePaymentAsync(RequirePaymentDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Payment creation failed validation for Shipment {ShipmentId}.", dto.ShipmentId);
                throw new Exception(validationResult.Errors.First().ErrorMessage);
            }

            var existingPayment = await _unitOfWork.Payments.FindAsync(p => p.ShipmentId == dto.ShipmentId);
            if (existingPayment.Any())
            {
                _logger.LogWarning("Attempted to create a duplicate payment for Shipment {ShipmentId}.", dto.ShipmentId);
                throw new Exception("A payment record already exists for this shipment.");
            }

            var payment = _mapper.Map<Domain.Entities.Payment>(dto);

            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Payment of {Amount} was successfully completed for Shipment {ShipmentId} using {Method}.",
                payment.Amount, payment.ShipmentId, payment.PaymentMethod);

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _unitOfWork.Payments.GetAllAsync(disableTracking: true);
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto?> GetPaymentByIdAsync(int id)
        {
            var payment = await _unitOfWork.Payments.GetAsync(p => p.PaymentId == id, disableTracking: true)
                          ?? throw new Exception("Payment not found");

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto> UpdatePaymentStatusAsync(int id, bool isPaid)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id) ?? throw new Exception("Payment not found");

            payment.IsPaid = isPaid;
            payment.PaidDate = isPaid ? DateTime.UtcNow : null; 

            _unitOfWork.Payments.Update(payment);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Payment {PaymentId} status updated to Paid: {IsPaid}.", id, isPaid);

            return _mapper.Map<PaymentDto>(payment);
        }
    }
}