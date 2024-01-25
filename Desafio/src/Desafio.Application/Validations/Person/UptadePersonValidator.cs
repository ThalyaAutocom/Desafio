using FluentValidation;

namespace Desafio.Application.Validations.Person;

public class UptadePersonValidator : AbstractValidator<UpdatePersonRequest>
{
    private readonly IPersonService _personService;

    public UptadePersonValidator(IPersonService personService)
    {
        _personService = personService;
        RuleFor(x => x.ShortId).NotEmpty().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.City).NotEmpty().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.AlternativeCode)
            .MustAsync(async (person, alternativeCode, _) => !await _personService.AlternativeCodeAlreadyExistsAsync(person)).WithMessage("The field {PropertyName} must be unique.");
        RuleFor(x => x.Document)
            .Must(document => (!string.IsNullOrWhiteSpace(document) && (document.Length == 11 || document.Length == 14)) || string.IsNullOrWhiteSpace(document)).WithMessage("The field {PropertyName} is invalid.")
            .MustAsync(async (person, document, _) => !await _personService.DocumentAlreadyExistsAsync(person)).WithMessage("The field {PropertyName} must be unique.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Document).IsValidCNPJ().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 14).Unless(x => string.IsNullOrWhiteSpace(x.Document));
                RuleFor(x => x.Document).IsValidCPF().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 11).Unless(x => string.IsNullOrWhiteSpace(x.Document));
            });
    }
}
