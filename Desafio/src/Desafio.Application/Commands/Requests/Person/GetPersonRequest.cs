using MediatR;

namespace Desafio.Application;

public class GetPersonRequest(bool? enable) : IRequest<GetPersonResponse>
{
    public bool? Enable { get; set; } = enable;
}
