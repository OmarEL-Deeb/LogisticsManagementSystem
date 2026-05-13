using FluentValidation;
using Logistics.Application.DTOs.EmployeeRoleDTO;

namespace Logistics.Application.Validators.EmployeeRoleValidators
{
    public class RequireRoleDtoValidator : AbstractValidator<RequireRoleDto>
    {
        public RequireRoleDtoValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Role name is required.")
                .MinimumLength(2).WithMessage("Role name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters.");
        }
    }
}