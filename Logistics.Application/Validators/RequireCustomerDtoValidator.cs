using FluentValidation;
using Logistics.Application.DTOs.CustomersDTOs;

namespace Logistics.Application.Validators.CustomerValidators
{
    public class RequireCustomerDtoValidator : AbstractValidator<RequireCustomerDto>
    {
        public RequireCustomerDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(3).WithMessage("Full name must be at least 3 characters long.")
                .MaximumLength(200).WithMessage("Full name cannot exceed 200 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address format is required.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .WithMessage("Phone number must contain between 10 and 15 digits.");
        }
    }
}