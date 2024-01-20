using MediatR;

namespace Desafio.Application;

public class GetByIdPersonRequest(Guid id) : IRequest<PersonResponse>
{
    public Guid Id { get; set; } = id;
}
