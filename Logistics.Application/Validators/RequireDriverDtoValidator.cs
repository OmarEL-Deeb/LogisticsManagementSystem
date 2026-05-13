using FluentValidation;
using Logistics.Application.DTOs.DriversDTOs;

namespace Logistics.Application.Validators.DriverValidators
{
    public class RequireDriverDtoValidator : AbstractValidator<RequireDriverDto>
    {
        public RequireDriverDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Driver full name is required.")
                .MaximumLength(200).WithMessage("Full name cannot exceed 200 characters.");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("License number is required.")
                .MaximumLength(50).WithMessage("License number cannot exceed 50 characters.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Driver salary must be greater than zero.");

            RuleFor(x => x.HireDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Hire date cannot be in the future.");
        }
    }
}