using FluentValidation;
using Logistics.Application.DTOs.ShipmentStatusHistoryDTO;
using Logistics.Application.DTOs.ShipmentStatusHistoryDTOs;

namespace Logistics.Application.Validators.ShipmentStatusHistoryValidators
{
    public class RequireShipmentStatusHistoryDtoValidator : AbstractValidator<RequireShipmentStatusHistoryDto>
    {
        public RequireShipmentStatusHistoryDtoValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid shipment status provided."); 

            RuleFor(x => x.ShipmentId)
                .GreaterThan(0).WithMessage("A valid Shipment ID is required.");
        }
    }
}