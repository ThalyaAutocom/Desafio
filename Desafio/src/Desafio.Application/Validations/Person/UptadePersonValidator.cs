using FluentValidation;

namespace Desafio.Application.Validations.Person;

public class UptadePersonValidator : AbstractValidator<UpdatePersonRequest>
{
    private readonly IPersonService _personService;

    public UptadePersonValidator(IPersonService personService)
    {
        _personService = personService;
        RuleFor(x => x.Id).NotEmpty().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.City).NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.AlternativeCode).NotNull().NotEmpty().WithMessage("The field {PropertyName} is required.")
            .MustAsync(async (person, alternativeCode, _) => !await _personService.AlternativeCodeAlreadyExistsAsync(person)).WithMessage("The field {PropertyName} must be unique.");
        RuleFor(x => x.Document)
            .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .Must(document => !string.IsNullOrWhiteSpace(document) && (document.Length == 11 || document.Length == 14)).WithMessage("The field {PropertyName} is invalid.")
            .MustAsync(async (person, document, _) => !await _personService.DocumentAlreadyExistsAsync(person)).WithMessage("The field {PropertyName} must be unique.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Document).IsValidCNPJ().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 14);
                RuleFor(x => x.Document).IsValidCPF().WithMessage("{PropertyName} is invalid.").When(x => x.Document.Length == 11);
            });
    }
}
