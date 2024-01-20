using MediatR;

namespace Desafio.Application;

public class GetProductRequest(bool? sellable, bool? enable) : IRequest<GetProductResponse>
{
    public bool? Sellable { get; set; } = sellable;
    public bool? Enable { get; set; } = enable;
}
