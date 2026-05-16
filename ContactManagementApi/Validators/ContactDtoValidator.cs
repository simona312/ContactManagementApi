using ContactManagementApi.DTOs;
using FluentValidation;

namespace ContactManagementApi.Validators
{
    public class ContactDtoValidator : AbstractValidator<ContactDto>
    {
        public ContactDtoValidator()
        {
            RuleFor(x => x.ContactName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.CompanyId)
                .GreaterThan(0);

            RuleFor(x => x.CountryId)
                .GreaterThan(0);
        }
    }
}