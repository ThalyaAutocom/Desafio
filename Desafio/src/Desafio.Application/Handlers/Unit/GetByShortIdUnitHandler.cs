using MediatR;

namespace Desafio.Application;
public class GetByShortIdUnitHandler : IRequestHandler<GetByShortIdUnitRequest, UnitResponse>
{
    private readonly IUnitService _unitService;

    public GetByShortIdUnitHandler(IUnitService unitService)
    {
        _unitService = unitService;
    }

    public async Task<UnitResponse> Handle(GetByShortIdUnitRequest request, CancellationToken cancellationToken)
    {
        return await _unitService.GetByShortIdAsync(request.ShortId);       
    }
}

