using MediatR;

namespace Desafio.Application;

public class CreateUnitHandle : IRequestHandler<CreateUnitRequest, CreateUnitResponse>
{
    private readonly IUnitService _unitService;

    public CreateUnitHandle(IUnitService unitService)
    {
        _unitService = unitService;
    }

    public async Task<CreateUnitResponse> Handle(CreateUnitRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitService.InsertAsync(request);

        return result;
    }
}
