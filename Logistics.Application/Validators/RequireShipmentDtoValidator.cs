using FluentValidation;
using Logistics.Application.DTOs.ShipmentDTOs;

namespace Logistics.Application.Validators.ShipmentValidators
{
    public class RequireShipmentDtoValidator : AbstractValidator<RequireShipmentDto>
    {
        public RequireShipmentDtoValidator()
        {
            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Shipment weight must be greater than zero.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Shipment price cannot be negative.");

            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("A valid Customer ID is required.");

            RuleFor(x => x.OriginWarehouseId)
                .GreaterThan(0).WithMessage("A valid Origin Warehouse ID is required.");

            RuleFor(x => x.DestinationWarehouseId)
                .GreaterThan(0).WithMessage("A valid Destination Warehouse ID is required.")
                .NotEqual(x => x.OriginWarehouseId)
                .WithMessage("Destination warehouse cannot be the same as the origin warehouse."); 

            RuleFor(x => x.VehicleId)
                .GreaterThan(0).WithMessage("A valid Vehicle ID is required.");
        }
    }
}