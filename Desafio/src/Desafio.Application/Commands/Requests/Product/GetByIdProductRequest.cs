using MediatR;

namespace Desafio.Application;

public class GetByIdProductRequest(string acronym) : IRequest<UnitResponse>
{
    public string Acronym { get; set; } = acronym;
}
