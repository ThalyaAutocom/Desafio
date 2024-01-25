using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application.Validations.Unit;

public class DeleteUnitValidator : AbstractValidator<DeleteUnitRequest>
{
    private readonly IUnitService _unitService;

    public DeleteUnitValidator(IUnitService unitService)
    {
        _unitService = unitService;

        RuleFor(x => x.ShortId)
                .NotEmpty().WithMessage("The field {PropertyName} is required.")
                .MustAsync(async (shortId, _) => !await _unitService.HasBeenUsedBeforeAsync(shortId)).WithMessage("It's not possible to remove a unit that is being used in a product.");

        return;
    }

}
