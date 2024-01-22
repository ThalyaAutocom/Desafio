using MediatR;

namespace Desafio.Application;
public class GetClientHandler : IRequestHandler<GetClientRequest, GetPersonResponse>
{
    private readonly IPersonService _personService;

    public GetClientHandler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<GetPersonResponse> Handle(GetClientRequest request, CancellationToken cancellationToken)
    {
        var result = await _personService.GetAllClientAsync();

        if(request.Enable is not null)
        {
            result = result.Where(x => x.Enable).ToList();
        }

        return new GetPersonResponse
        {
            PersonResponses = result
        };
    }
}

