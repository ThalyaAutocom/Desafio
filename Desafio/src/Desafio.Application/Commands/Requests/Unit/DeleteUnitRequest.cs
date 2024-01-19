using MediatR;

namespace Desafio.Application;

public class DeleteUnitRequest : IRequest<bool>
{
    private string _acronym;
    public string Acronym
    {
        get => _acronym;
        set => _acronym = value?.ToUpper();
    }

}
