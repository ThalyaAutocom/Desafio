using MediatR;

namespace Desafio.Application;
public class GetByShortIdPersonHandler : IRequestHandler<GetByShortIdPersonRequest, PersonResponse>
{
    private readonly IPersonService _personService;

    public GetByShortIdPersonHandler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<PersonResponse> Handle(GetByShortIdPersonRequest request, CancellationToken cancellationToken)
    {
        return await _personService.GetByShortIdAsync(request.ShortId);       
    }
}

