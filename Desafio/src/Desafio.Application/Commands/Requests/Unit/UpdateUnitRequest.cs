using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class UpdateUnitRequest : IRequest<bool>
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
