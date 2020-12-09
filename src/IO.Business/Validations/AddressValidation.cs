using System;
using FluentValidation;
using IO.Business.Models;

namespace IO.Business.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(c => c.PublicPlace)
                .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
                .Length(2, 200).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Neighborhoodty)
                .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
                .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
                .Length(8).WithMessage("The {PropertyName} field must be {MaxLength} characters");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
                .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
                .Length(2, 50).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
                .Length(1, 50).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");
        }
    }
}
