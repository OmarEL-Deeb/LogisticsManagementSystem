using FluentValidation;
using Logistics.Application.DTOs.EmployeeDTOs;

namespace Logistics.Application.Validators.EmployeeValidators
{
    public class RequireEmployeeDtoValidator : AbstractValidator<RequireEmployeeDto>
    {
        public RequireEmployeeDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Employee full name is required.")
                .MaximumLength(200).WithMessage("Full name cannot exceed 200 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address format is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
                //.MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                //.Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                //.Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                //.Matches(@"[0-9]").WithMessage("Password must contain at least one number.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Employee salary must be greater than zero.");

            RuleFor(x => x.HireDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Hire date cannot be in the future.");

            RuleFor(x => x.WarehouseId)
                .GreaterThan(0).WithMessage("A valid Warehouse ID is required.");

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("A valid Role ID is required.");
        }
    }
}