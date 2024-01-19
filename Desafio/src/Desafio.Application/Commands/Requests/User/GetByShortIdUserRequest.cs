using MediatR;

namespace Desafio.Application;

public class GetByShortIdUserRequest(string shortId) : IRequest<UserResponse>
{
    public string ShortId { get; set; } = shortId;
}
