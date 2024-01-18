using Desafio.Domain;
using MediatR;

namespace Desafio.Application;

public class GetByIdUserRequest(Guid id) : IRequest<GetUserResponse>
{
    public Guid Id { get; set; } = id;
}
