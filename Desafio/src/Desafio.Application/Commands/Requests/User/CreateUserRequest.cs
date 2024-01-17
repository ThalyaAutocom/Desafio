using Desafio.Domain;
using MediatR;

namespace Desafio.Application;

public class CreateUserRequest : IRequest<CreateUserResponse>
{
    private string _document;
    
    public string Email 
    { 
        get => UserName;
        set 
        {
            UserName = value;
        } 
    }
    public string Password { get; set; } 
    public string ConfirmPassword { get; set; } 
    public EUserLevel UserLevel { get; set; } = EUserLevel.Administrator;
    public string Name { get; set; }
    public string NickName { get; set; }
    public string Document
    {
        get => _document;
        set => _document = value.GetOnlyDocumentNumber();
    }
    public string UserName { get; private set; }
}
