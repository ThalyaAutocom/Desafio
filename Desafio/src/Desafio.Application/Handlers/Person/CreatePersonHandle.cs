using MediatR;

namespace Desafio.Application;

public class CreatePersonHandle : IRequestHandler<CreatePersonRequest, CreatePersonResponse>
{
    private readonly IPersonService _personService;

    public CreatePersonHandle(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<CreatePersonResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
    {
        var result = await _personService.InsertAsync(request);

        return result;
    }
}
