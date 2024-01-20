using MediatR;

namespace Desafio.Application;
public class GetByIdPersonHandler : IRequestHandler<GetByIdPersonRequest, PersonResponse>
{
    private readonly IPersonService _personService;

    public GetByIdPersonHandler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<PersonResponse> Handle(GetByIdPersonRequest request, CancellationToken cancellationToken)
    {
        return await _personService.GetByIdAsync(request.Id);       
    }
}

