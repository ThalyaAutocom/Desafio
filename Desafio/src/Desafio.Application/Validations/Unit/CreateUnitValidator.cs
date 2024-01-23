using FluentValidation;

namespace Desafio.Application.Validations.Unit;

public class CreateUnitValidator : AbstractValidator<CreateUnitRequest>
{
    private readonly IUnitService _unitService;

    public CreateUnitValidator(IUnitService unitService)
    {
        _unitService = unitService;

        RuleFor(x => x.Acronym)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .MustAsync(async(acronym, _) => !await _unitService.AcronymAlreadyUsedAsync(acronym)).WithMessage("The Acronym must be unique.")
            .Length(2, 4).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .Length(2, 50)
            .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters.");
    }
}
