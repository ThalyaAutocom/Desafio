using MediatR;

namespace Desafio.Application;
public class GetPersontHandler : IRequestHandler<GetPersonRequest, GetPersonResponse>
{
    private readonly IPersonService _personService;

    public GetPersontHandler(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<GetPersonResponse> Handle(GetPersonRequest request, CancellationToken cancellationToken)
    {
        var result = await _personService.GetAllAsync();

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

