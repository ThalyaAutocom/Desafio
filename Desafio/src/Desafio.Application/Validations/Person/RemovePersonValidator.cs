using FluentValidation;

namespace Desafio.Application.Validations.Person;

public class RemovePersonValidator : AbstractValidator<DeletePersonRequest>
{
    private readonly IPersonService _personService;

    public RemovePersonValidator(IPersonService personService)
    {
        _personService = personService;

        RuleFor(x => x.Id)
                .MustAsync(async (id, _) => !await _personService.PersonCanBuyAsync(id)).WithMessage("Cannot remove a client.");
    }
}
