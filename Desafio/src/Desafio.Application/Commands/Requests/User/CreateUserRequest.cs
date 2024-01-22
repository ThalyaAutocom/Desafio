using Desafio.Domain;
using MediatR;

namespace Desafio.Application;

public class CreateUserRequest : IRequest<CreateUserResponse>
{
    private string _document;
    private string _nickName;
    
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
    public string NickName
    {
        get => _nickName;
        set => _nickName = value?.ToUpper();
    }
    public string Document
    {
        get => _document;
        set => _document = value.GetOnlyDocumentNumber();
    }
    public string UserName { get; private set; }
    public bool Enable { get; set; } = true;
}
