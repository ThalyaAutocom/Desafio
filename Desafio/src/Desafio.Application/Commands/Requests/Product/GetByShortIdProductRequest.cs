using MediatR;

namespace Desafio.Application;

public class GetByShortIdProductRequest(string shortId) : IRequest<ProductResponse>
{
    public string ShortId { get; set; } = shortId;
}
