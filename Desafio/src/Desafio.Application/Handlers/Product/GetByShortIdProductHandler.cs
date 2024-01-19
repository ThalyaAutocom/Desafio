using MediatR;

namespace Desafio.Application;
public class GetByShortIdProductHandler : IRequestHandler<GetByShortIdUnitRequest, UnitResponse>
{
    private readonly IUnitService _unitService;

    public GetByShortIdProductHandler(IUnitService unitService)
    {
        _unitService = unitService;
    }

    public async Task<UnitResponse> Handle(GetByShortIdUnitRequest request, CancellationToken cancellationToken)
    {
        return await _unitService.GetByShortIdAsync(request.ShortId);       
    }
}

