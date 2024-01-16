using Desafio.Domain;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class RegisterUserRequest
{
    private string _document;
    private string _userName;

    
    public string Email { get; set; }
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
    private string UserName 
    {
        get => _userName;
        set => _userName = Email;
    }
}
