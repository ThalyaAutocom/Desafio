using MediatR;

namespace Desafio.Application;

public class GetByShortIdUnitRequest(string shortId) : IRequest<UnitResponse>
{
    public string ShortId { get; set; } = shortId;
}
