using MediatR;

namespace Desafio.Application;

public class DeletePersonHandle : IRequestHandler<DeletePersonRequest, bool>
{
    private readonly IPersonService _personService;

    public DeletePersonHandle(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<bool> Handle(DeletePersonRequest request, CancellationToken cancellationToken)
    {
        return await _personService.RemoveAsync(request.Id);
    }
}
