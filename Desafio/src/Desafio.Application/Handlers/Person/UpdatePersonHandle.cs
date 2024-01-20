using MediatR;

namespace Desafio.Application;

public class UpdatePersonHandle : IRequestHandler<UpdatePersonRequest, bool>
{
    private readonly IPersonService _personService;

    public UpdatePersonHandle(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<bool> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        var result = await _personService.UpdateAsync(request);

        return result;
    }
}
