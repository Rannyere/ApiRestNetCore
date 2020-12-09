using System;
using FluentValidation;
using IO.Business.Models;
using IO.Business.Validations.Documents;

namespace IO.Business.Validations
{
    public class ProviderValidation : AbstractValidator<Provider>
    {
        public ProviderValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be informed")
                .Length(2, 100).WithMessage("The field {PropertyName} must be between {MinLength} e {MaxLength} characteres");

            When(p => p.TypeProvider == TypeProvider.NaturalPerson, () =>
            {
                RuleFor(p => p.Document.Length).Equal(CpfValidacao.TamanhoCpf)
                .WithMessage("The Document field must be {ComparisonValue} characters and {PropertyValue} has been provided.");
                RuleFor(p => CpfValidacao.Validar(p.Document)).Equal(true)
                    .WithMessage("Document Invalid.");
            });

            When(p => p.TypeProvider == TypeProvider.LegalPerson, () =>
            {
                RuleFor(p => p.Document.Length).Equal(CnpjValidacao.TamanhoCnpj)
                .WithMessage("The Document field must be {ComparisonValue} characters and {PropertyValue} has been provided.");
                RuleFor(p => CpfValidacao.Validar(p.Document)).Equal(true)
                    .WithMessage("Document Invalid.");
            });
        }
    }
}
