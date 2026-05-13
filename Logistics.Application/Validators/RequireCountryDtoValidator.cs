using FluentValidation;
using Logistics.Application.DTOs.CountriesDTOs;

namespace Logistics.Application.Validators.CountryValidators
{
    public class RequireCountryDtoValidator : AbstractValidator<RequireCountryDto>
    {
        public RequireCountryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Country name is required.")
                .MaximumLength(100).WithMessage("Country name cannot exceed 100 characters.");
        }
    }
}