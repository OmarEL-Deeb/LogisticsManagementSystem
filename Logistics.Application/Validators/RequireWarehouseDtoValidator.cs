using FluentValidation;
using Logistics.Application.DTOs.WarehouseDTOs;

namespace Logistics.Application.Validators.WarehouseValidators
{
    public class RequireWarehouseDtoValidator : AbstractValidator<RequireWarehouseDto>
    {
        public RequireWarehouseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Warehouse name is required.")
                .MaximumLength(150).WithMessage("Warehouse name cannot exceed 150 characters.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Warehouse capacity must be greater than zero.");

            RuleFor(x => x.CityId)
                .GreaterThan(0).WithMessage("A valid City ID is required.");
        }
    }
}