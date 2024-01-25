using FluentValidation;

namespace Desafio.Application.Validations.Person;

public class RemovePersonValidator : AbstractValidator<DeletePersonRequest>
{
    private readonly IPersonService _personService;

    public RemovePersonValidator(IPersonService personService)
    {
        _personService = personService;

        RuleFor(x => x.ShortId)
                .MustAsync(async (shortId, _) => !await _personService.PersonCanBuyAsync(shortId)).WithMessage("Cannot remove a client.");
    }
}
