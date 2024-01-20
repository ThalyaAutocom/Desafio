using MediatR;

namespace Desafio.Application;
public class GetByIdClientHandler : IRequestHandler<GetByIdClientRequest, PersonResponse>
{
    private readonly IPersonService _clientService;

    public GetByIdClientHandler(IPersonService clientService)
    {
        _clientService = clientService;
    }

    public async Task<PersonResponse> Handle(GetByIdClientRequest request, CancellationToken cancellationToken)
    {
        return await _clientService.GetByIdAsync(request.Id);       
    }
}

