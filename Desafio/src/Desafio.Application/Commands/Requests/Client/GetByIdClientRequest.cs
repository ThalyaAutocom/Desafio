using MediatR;

namespace Desafio.Application;

public class GetByIdClientRequest(Guid id) : IRequest<PersonResponse>
{
    public Guid Id { get; set; } = id;
}
