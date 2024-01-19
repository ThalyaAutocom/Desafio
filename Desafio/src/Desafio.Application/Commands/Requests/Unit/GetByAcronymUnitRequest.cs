using MediatR;

namespace Desafio.Application;

public class GetByAcronymUnitRequest(string acronym) : IRequest<UnitResponse>
{
    public string Acronym { get; set; } = acronym;
}
