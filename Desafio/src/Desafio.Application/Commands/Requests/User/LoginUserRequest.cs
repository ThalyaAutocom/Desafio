using MediatR;

namespace Desafio.Application;

public class LoginUserRequest : IRequest<LoginUserResponse>
{
    private string _nickName;
    public string NickName
    {
        get => _nickName;
        set => _nickName = value?.ToUpper();
    }
    public string Password { get; set; }
}
