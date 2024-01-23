using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application.Validations.Unit;

public class RemoveUnitValidator : AbstractValidator<DeleteUnitRequest>
{
    private readonly IUnitService _unitService;

    public RemoveUnitValidator(IUnitService unitService)
    {
        _unitService = unitService;

        RuleFor(x => x.Acronym)
                .NotEmpty().WithMessage("The field {PropertyName} is required.")
                .MustAsync(async(acronym, _) => !await _unitService.HasBeenUsedBeforeAsync(acronym)).WithMessage("It's not possible to remove a unit that is being used in a product.")
                .Length(2, 4)
                .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters.");

        return;
    }

}
