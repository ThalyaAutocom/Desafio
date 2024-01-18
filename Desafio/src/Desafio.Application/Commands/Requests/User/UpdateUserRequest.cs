using Desafio.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class UpdateUserRequest : IRequest<bool>
{
    private string _document;

    public string Email { get; set; }
    public string UserName { get; set; }
    public EUserLevel UserLevel { get; set; } = EUserLevel.Administrator;
    public string Name { get; set; }
    public string NickName { get; set; }
    public string Document
    {
        get => _document;
        set => _document = value.GetOnlyDocumentNumber();
    }
}
