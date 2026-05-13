using FluentValidation;
using Logistics.Application.DTOs.VehicleDTOs;

namespace Logistics.Application.Validators.VehicleValidators
{
    public class RequireVehicleDtoValidator : AbstractValidator<RequireVehicleDto>
    {
        public RequireVehicleDtoValidator()
        {
            RuleFor(x => x.PlateNumber)
                .NotEmpty().WithMessage("Vehicle plate number is required.")
                .MaximumLength(20).WithMessage("Plate number cannot exceed 20 characters.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Vehicle capacity must be greater than zero.");

            RuleFor(x => x.AssignedDriverId)
                .GreaterThan(0).When(x => x.AssignedDriverId.HasValue)
                .WithMessage("If a driver is assigned, the Driver ID must be valid.");
        }
    }
}