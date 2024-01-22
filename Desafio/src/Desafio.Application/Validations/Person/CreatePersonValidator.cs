using FluentValidation;

namespace Desafio.Application.Validations.Person;

public class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
{
    private readonly IPersonService _personService;

    public CreatePersonValidator(IPersonService personService)
    {
        _personService = personService;
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.City).NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.AlternativeCode)
            .MustAsync(async (alternativeCode, _) => await _personService.AlternativeCodeAlreadyExistsAsync(alternativeCode)).WithMessage("The field {PropertyName} must be unique.");
        RuleFor(x => x.Document)
            .Must(document => (!string.IsNullOrWhiteSpace(document) && (document.Length == 11 || document.Length == 14)) || string.IsNullOrWhiteSpace(document)).WithMessage("The field {PropertyName} is invalid.")
            .MustAsync(async (document, _) => await _personService.DocumentAlreadyExistsAsync(document)).WithMessage("The field {PropertyName} must be unique.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Document).IsValidCNPJ().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 14).Unless(x => string.IsNullOrWhiteSpace(x.Document));
                RuleFor(x => x.Document).IsValidCPF().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 11).Unless(x => string.IsNullOrWhiteSpace(x.Document));
            });
    }
}
