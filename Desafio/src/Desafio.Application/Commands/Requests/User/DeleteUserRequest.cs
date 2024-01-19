using Desafio.Domain;
using MediatR;

namespace Desafio.Application;

public class DeleteUserRequest(string email) : IRequest<bool>
{
    public string Email { get; set; } = email;
}
