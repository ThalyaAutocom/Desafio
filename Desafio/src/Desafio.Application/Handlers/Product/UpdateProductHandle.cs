using MediatR;

namespace Desafio.Application;

public class UpdateProductHandle : IRequestHandler<UpdateUnitRequest, bool>
{
    private readonly IUnitService _unitService;

    public UpdateProductHandle(IUnitService unitService)
    {
        _unitService = unitService;
    }

    public async Task<bool> Handle(UpdateUnitRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitService.UpdateAsync(request);

        return result;
    }
}
