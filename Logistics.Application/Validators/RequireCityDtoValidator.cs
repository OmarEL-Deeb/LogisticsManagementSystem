using FluentValidation;
using Logistics.Application.DTOs.CityDTOs;

namespace Logistics.Application.Validators
{
    public class RequireCityDtoValidator : AbstractValidator<RequireCityDto>
    {
        public RequireCityDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("City name is required.")
                .MaximumLength(100).WithMessage("City name cannot exceed 100 characters.");

            RuleFor(x => x.CountryId)
                .GreaterThan(0).WithMessage("A valid Country ID must be provided.");
        }
    }
}