using MediatR;

namespace Desafio.Application;

public class GetByShortIdProductRequest(string shortId) : IRequest<UnitResponse>
{
    public string ShortId { get; set; } = shortId;
}
