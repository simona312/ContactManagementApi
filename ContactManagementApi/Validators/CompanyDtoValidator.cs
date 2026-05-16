using ContactManagementApi.DTOs;
using FluentValidation;

namespace ContactManagementApi.Validators
{
    public class CompanyDtoValidator : AbstractValidator<CompanyDto>
    {
        public CompanyDtoValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}