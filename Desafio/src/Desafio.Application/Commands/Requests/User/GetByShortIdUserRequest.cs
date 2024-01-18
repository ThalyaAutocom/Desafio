using Desafio.Domain;
using MediatR;

namespace Desafio.Application;

public class GetByShortIdUserRequest(string shortId) : IRequest<GetUserResponse>
{
    public string Id { get; set; } = shortId;
}
