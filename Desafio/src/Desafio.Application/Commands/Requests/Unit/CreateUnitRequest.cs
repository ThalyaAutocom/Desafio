using MediatR;

namespace Desafio.Application;

public class CreateUnitRequest: IRequest<CreateUnitResponse>
{
    private string _acronym;
    private string _description;
    public string Acronym 
    {
        get => _acronym;
        set => _acronym = value?.ToUpper();
    }
    public string Description
    {
        get => _description;
        set => _description = value?.ToUpper();
    }
}
