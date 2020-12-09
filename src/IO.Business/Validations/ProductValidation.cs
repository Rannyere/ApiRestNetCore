using System;
using FluentValidation;
using IO.Business.Models;

namespace IO.Business.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
                .Length(2, 200).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
                .Length(2, 1000).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Value)
                .GreaterThan(0).WithMessage("The {PropertyName} field must be greater than {ComparisonValue}");
        }
    }
}
