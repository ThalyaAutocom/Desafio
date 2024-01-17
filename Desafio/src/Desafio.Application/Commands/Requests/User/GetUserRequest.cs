using Desafio.Domain;
using MediatR;

namespace Desafio.Application;

public class GetUserRequest(bool enable, EUserLevel? userLevel) : IRequest<GetUserResponse>
{
    public bool Enable { get; set; } = enable;
    public EUserLevel? UserLevel { get; set; } = userLevel;
}
