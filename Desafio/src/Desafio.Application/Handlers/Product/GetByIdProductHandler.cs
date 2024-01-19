using MediatR;

namespace Desafio.Application;
public class GetByIdProductHandler : IRequestHandler<GetByAcronymUnitRequest, UnitResponse>
{
    private readonly IUnitService _unitService;

    public GetByIdProductHandler(IUnitService unitService)
    {
        _unitService = unitService;
    }

    public async Task<UnitResponse> Handle(GetByAcronymUnitRequest request, CancellationToken cancellationToken)
    {
        return await _unitService.GetByAcronymAsync(request.Acronym);       
    }
}

