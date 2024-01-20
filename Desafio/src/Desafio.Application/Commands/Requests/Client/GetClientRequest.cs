using MediatR;

namespace Desafio.Application;

public class GetClientRequest(bool? enable) : IRequest<GetPersonResponse>
{
    public bool? Enable { get; set; } = enable;
}
