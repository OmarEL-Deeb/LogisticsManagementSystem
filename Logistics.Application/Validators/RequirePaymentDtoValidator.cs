using FluentValidation;
using Logistics.Application.DTOs.PaymentDTOs;

namespace Logistics.Application.Validators.PaymentValidators
{
    public class RequirePaymentDtoValidator : AbstractValidator<RequirePaymentDto>
    {
        public RequirePaymentDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Payment amount must be greater than zero.");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum().WithMessage("Invalid payment method provided.");

            RuleFor(x => x.ShipmentId)
                .GreaterThan(0).WithMessage("A valid Shipment ID is required to process the payment.");
        }
    }
}