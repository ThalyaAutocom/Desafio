using MediatR;

namespace Desafio.Application;

public class GetByIdUserRequest(string id) : IRequest<UserResponse>
{
    public string Id { get; set; } = id;
}
