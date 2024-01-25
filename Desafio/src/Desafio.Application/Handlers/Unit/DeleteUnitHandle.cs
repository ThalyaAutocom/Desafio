using MediatR;

namespace Desafio.Application;

public class DeleteUnitHandle : IRequestHandler<DeleteUnitRequest, bool>
{
    private readonly IUnitService _unitService;

    public DeleteUnitHandle(IUnitService unitService)
    {
        _unitService = unitService;
    }

    public async Task<bool> Handle(DeleteUnitRequest request, CancellationToken cancellationToken)
    {
        return await _unitService.RemoveAsync(request.ShortId);
    }
}
