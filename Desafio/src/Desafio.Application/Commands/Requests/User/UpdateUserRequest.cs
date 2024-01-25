using Desafio.Domain;
using MediatR;

namespace Desafio.Application;

public class UpdateUserRequest : IRequest<bool>
{
    private string _document;
    private string _nickName;

    public string Id { get; set; }
    public string Email
    {
        get => UserName;
        set
        {
            UserName = value;
        }
    }
    public string UserName { get; private set; }
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
    public bool Enable {  get; set; }
}
