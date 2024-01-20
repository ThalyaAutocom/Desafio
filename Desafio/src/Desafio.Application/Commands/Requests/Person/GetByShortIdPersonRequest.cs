using MediatR;

namespace Desafio.Application;

public class GetByShortIdPersonRequest(string shortId) : IRequest<PersonResponse>
{
    public string ShortId { get; set; } = shortId;
}
