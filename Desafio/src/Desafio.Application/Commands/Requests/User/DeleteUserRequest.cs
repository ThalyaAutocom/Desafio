using Desafio.Domain;
using MediatR;

namespace Desafio.Application;

public class DeleteUserRequest(Guid id) : IRequest<bool>
{
    public Guid Id { get; set; } = id;
}
