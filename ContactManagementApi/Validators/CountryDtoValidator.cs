using ContactManagementApi.DTOs;
using FluentValidation;

namespace ContactManagementApi.Validators
{
    public class CountryDtoValidator : AbstractValidator<CountryDto>
    {
        public CountryDtoValidator()
        {
            RuleFor(x => x.CountryName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
