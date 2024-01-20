using MediatR;

namespace Desafio.Application;
public class GetUnitHandler : IRequestHandler<GetUnitRequest, GetUnitResponse>
{
    private readonly IUnitService _unitService;

    public GetUnitHandler(IUnitService unitService)
    {
        _unitService = unitService;
    }

    public async Task<GetUnitResponse> Handle(GetUnitRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitService.GetAllAsync();

        return new GetUnitResponse
        {
            UnitResponses = result
        };
    }
}

